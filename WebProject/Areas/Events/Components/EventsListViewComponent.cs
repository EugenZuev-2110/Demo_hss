using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.Events.Data;
using WebProject.Areas.Events.Models;
using WebProject.Data;

namespace WebProject.Components
{
    public class EventsListViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public EventsListViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int year, int object_type)
        {
            List<EventsViewModel> events = await _context.EventsViewModel.FromSqlInterpolated($"exec events.sp_GetEventsList {year},{object_type}").ToListAsync();
            await _context.DisposeAsync();
            ViewBag.ObjectType = object_type;
            ViewBag.EventYear = year;

            return View("EventsList", events);
        }
    }
}
