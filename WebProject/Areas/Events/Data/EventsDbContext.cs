using DataBase.Models;
using DataBase.Models.Events;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.Events.Models;

namespace WebProject.Areas.Events.Data
{
    public class EventsDbContext : DbContext //IdentityDbContext<IdentityUser>
    {
        public EventsDbContext(DbContextOptions<EventsDbContext> options)
            : base(options)
        {
        }

        public DbSet<DictWinUsers> DictWinUsers { get; set; } = null!;
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);
            modelBuilder
            .Entity<SourceListView>()
            .ToView("SourceListView")
            .HasNoKey();
            
            base.OnModelCreating(modelBuilder);
            modelBuilder
            .Entity<TSOListView>()
            .ToView("TSOListView")
            .HasNoKey();
        }
    }
   
}