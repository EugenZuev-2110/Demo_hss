using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebProject.Data;
using WebProject.Models;

namespace WebProject.Components
{
    public class UnomsListViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public UnomsListViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(string searchText, string filter, int userId)
        {
            //string unom_num = string.Empty;
            // если передан параметр number
            //if (Request.Query.ContainsKey("unom_num"))
            //{
            //    //int.TryParse(Request.Query["unom_num"], out number);
            //    unom_num = Request.Query["unom_num"];
            //}

            //var searchTextParam = new SqlParameter("@search_text", searchText ?? string.Empty);
            //List<UnomsViewModel> unoms = await _context.UnomsViewModel.FromSqlRaw("exec sp_GetUnomsList @search_text", searchTextParam).ToListAsync();

            List<UnomsViewModel> unoms = await _context.UnomsViewModel.FromSqlInterpolated($"exec sp_GetUnomsList {searchText ?? ""}, {filter ?? "all"}, {userId}").ToListAsync();
            await _context.DisposeAsync();
            //var unoms = await _context.sp_GetUnomsList(searchText ?? string.Empty).ToListAsync();
            return View(unoms);
        }
    }
}
