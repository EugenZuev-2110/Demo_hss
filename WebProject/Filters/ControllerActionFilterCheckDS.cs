using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Text.RegularExpressions;
using WebProject.Data;

namespace WebProject.Filters
{
    public class ControllerActionFilterCheckDS : IAsyncActionFilter
    {
        private readonly ApplicationDbContext _context;
		private readonly HssDbContext _contextHSS;
		private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string? _user;
        private int userId;

        public ControllerActionFilterCheckDS(ApplicationDbContext context_db, HssDbContext context_db_hss, IHttpContextAccessor httpContextAccessor)
        {
            _context = context_db;
			_contextHSS = context_db_hss;
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
			context.ActionArguments.TryGetValue("model", out object model);
			if (model is not null)
			{
				foreach (var property in model.GetType().GetProperties())
				{
                    if (property.Name.Contains("data_status"))
					{
						var data_status = (int)model.GetType().GetProperty(property.Name).GetValue(model);

						if (await _contextHSS.DataStatuses.AnyAsync(x => x.data_status == data_status && x.is_active == false))
                        {
                            //property.SetValue(model, -1, null); -с помощью этой строки можно переопределить входную переменную (на -1)

							throw new Exception("Редактирование запрещено. Выберите другой базовый год.");
						}
                    }
				}
			}

			foreach (var arg in context.ActionArguments)
			{
				if (arg.Key.Contains("data_status"))
				{
					context.ActionArguments.TryGetValue(arg.Key, out object data_status);

					if (data_status is not null)
					{
						if (await _contextHSS.DataStatuses.AnyAsync(x => x.data_status == (int)data_status && x.is_active == false))
						{
							throw new Exception("Редактирование запрещено. Выберите другой базовый год.");
						}
					}
				}
			}
			await next();
        }

	}
}