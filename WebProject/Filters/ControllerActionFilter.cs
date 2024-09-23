using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.RegularExpressions;
using WebProject.Data;

namespace WebProject.Filters
{
    public class ControllerActionFilter : IAsyncActionFilter
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string? _user;
        private int userId;

        public ControllerActionFilter(ApplicationDbContext context_db, IHttpContextAccessor httpContextAccessor)
        {
            _context = context_db;
            _httpContextAccessor = httpContextAccessor;
            _user = _httpContextAccessor.HttpContext.User.Identity.Name;

            if (_user != null)
            {
                var user = _context.DictWinUsers.Where(x => x.UserLogin == _user).FirstOrDefault();
                if (user != null)
                {
					userId = user.Id;
				}
            }
            else
            {
                string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                if (host.Contains("localhost"))
                {
                    userId = 1;
                }
            }
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //дата и время последнего действия пользователя
            var user = _context.DictWinUsers.Where(x => x.Id == userId).FirstOrDefault();
            if (user != null)
            {
				user.Last_visit_dt = DateTime.Now;
				await _context.SaveChangesAsync();
			}
            await next();
        }

    }
}