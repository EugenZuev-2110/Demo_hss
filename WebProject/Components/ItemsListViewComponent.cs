using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebProject.Data;
using WebProject.Models;

namespace WebProject.Components
{
    public class ItemsListViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ItemsListViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(string searchText)
        {
            //var unomNumParam = new SqlParameter("@unom_num", searchText ?? string.Empty);
            //List<Items> items = await _context.Items.FromSqlRaw("exec sp_GetItemsList @unom_num", unomNumParam).ToListAsync();
            List<ItemsViewModel> items = await _context.ItemsViewModel.FromSqlInterpolated($"exec sp_GetItemsList {searchText ?? ""}").ToListAsync();
            await _context.DisposeAsync();
            return View(items);
        }
    }
}
