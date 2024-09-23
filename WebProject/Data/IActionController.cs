using Microsoft.AspNetCore.Mvc;

namespace WebProject.Data
{
    public interface IActionController
    {
        public Task<IActionResult> SaveAsync(HssDbContext _context, int userId, IFormCollection keyValues = null);
    }
}
