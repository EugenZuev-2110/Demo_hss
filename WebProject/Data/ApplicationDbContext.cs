using DataBase.Models;
using DataBase.Models.Events;
using DataBase.Models.RET;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.Events.Models;
using WebProject.Models;

namespace WebProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // Контекст получает строку подключения.
        //    optionsBuilder.UseSqlServer(Constants.SqlConnectionIntegratedSecurity);
        //}
        public DbSet<DictWinUsers> DictWinUsers { get; set; } = null!;
        public DbSet<Unoms> Unoms { get; set; } = null!;
        public DbSet<DictUnomStates> DictUnomStates { get; set; } = null!;
        public DbSet<DictUnomCategories> DictUnomCategories { get; set; } = null!;
        public DbSet<DictDistricts> DictDistricts { get; set; } = null!;
        public DbSet<DictUnomTags> DictUnomTags { get; set; } = null!;
        public DbSet<DictObjectTypes> DictObjectTypes { get; set; } = null!;
        public DbSet<DictOrganisation> DictOrganisation { get; set; }
        public DbSet<SourceListView> SourceListView { get; set; } = null!;
        public DbSet<ExecutorsListView> ExecutorsListView { get; set; } = null!;
        public DbSet<CategoriesListView> CategoriesListView { get; set; } = null!;
        public DbSet<DataStatusesView> DataStatusesView { get; set; } = null!;
        public DbSet<DictLayers> DictLayers { get; set; } = null!;
        public DbSet<DictETSProjects> DictETSProjects { get; set; } = null!;
        public DbSet<ObjectTypesListView> ObjectTypesListView { get; set; }
        public DbSet<Items> Items { get; set; } = null!;
        public DbSet<Items_Logs> Items_Logs { get; set; } = null!;
        public DbSet<UnomTagsMapps> UnomTagsMapps { get; set; } = null!;
        public DbSet<SectionsCategoryMapps> SectionsCategoryMapps { get; set; } = null!;
        public DbSet<ConsumersHeatLoad> ConsumersHeatLoad { get; set; } = null!;
        public DbSet<ObjectDescription> ObjectDescription { get; set; } = null!;
        public DbSet<KSIO> KSIO { get; set; } = null!;
        public DbSet<Decommissioning> Decommissioning { get; set; } = null!;
        public DbSet<SourceDescription> SourceDescription { get; set; } = null!;
        public DbSet<HeatNetworksEvents> HeatNetworksEvents { get; set; } = null!;
        public DbSet<UnomHNE> UnomHNE { get; set; } = null!;
        //Мероприятия
        public DbSet<Sources> Sources { get; set; }
        public DbSet<Networks> Networks { get; set; }
        public DbSet<ClosedScheme> ClosedScheme { get; set; }
        public DbSet<YearsView> YearsView { get; set; } = null!;
        public DbSet<TSOListView> TSOListView { get; set; } = null!;
        public DbSet<DictEventsTypes> DictEventsTypes { get; set; } = null!;
        public DbSet<DictObjectsCodes> DictObjectsCodes { get; set; } = null!;
        public DbSet<DictPurposeCodes> DictPurposeCodes { get; set; } = null!;
        public DbSet<DictSFinanceCodes> DictSFinanceCodes { get; set; } = null!;
        //Мероприятия
        //РЭТ
        public DbSet<DictTemplates> DictTemplates { get; set; } = null!;
        public DbSet<TemplateRET_K> TemplateRET_K { get; set; } = null!;
        //РЭТ

        public virtual DbSet<EventsViewModel> EventsViewModel { get; set; }
        public virtual DbSet<EventViewModel> EventViewModel { get; set; }
        public virtual DbSet<UnomsViewModel> UnomsViewModel { get; set; }
        public virtual DbSet<UnomInfoViewModel> UnomInfoViewModel { get; set; }
        public virtual DbSet<ItemsViewModel> ItemsViewModel { get; set; }
        public virtual DbSet<SourceViewModel> SourceViewModel { get; set; }
        public virtual DbSet<LoadsViewModel> LoadsViewModel { get; set; }
        
        //Отчёты для книги по обращениям
        public virtual DbSet<ReportOther_t15_ViewModel> ReportOther_t15_ViewModel { get; set; }
        public virtual DbSet<ReportTechConViewModel> ReportTechConViewModel { get; set; }
        public virtual DbSet<ReportDecommissioningViewModel> ReportDecommissioningViewModel { get; set; }
        public virtual DbSet<ReportOthers_t9_ViewModel> ReportOthers_t9_ViewModel { get; set; }
        public virtual DbSet<ReportAll_t9_ViewModel> ReportAll_t9_ViewModel { get; set; }
        public virtual DbSet<ReportKSIO_t15_ViewModel> ReportKSIO_t15_ViewModel { get; set; }
        public virtual DbSet<ReportTechCon_t15_ViewModel> ReportTechCon_t15_ViewModel { get; set; }
        public virtual DbSet<ReportDecommissioning_t15_ViewModel> ReportDecommissioning_t15_ViewModel { get; set; }
        public virtual DbSet<ReportAll_t15_ViewModel> ReportAll_t15_ViewModel { get; set; }
        public virtual DbSet<UnomItemsCheckListViewModel> UnomItemsCheckListViewModel { get; set; }
        //public IQueryable<UnomsViewModel> dbo.sp_GetUnomsList(string search_text) => FromExpression(() => sp_GetUnomsList(search_text));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
            .Entity<SourceListView>()
            .ToView("SourceListView")
            .HasNoKey();

            modelBuilder
            .Entity<DataStatusesView>()
            .ToView("DataStatusesView")
            .HasNoKey();

            modelBuilder
            .Entity<ObjectTypesListView>()
            .ToView("ObjectTypesListView")
            .HasNoKey();

            modelBuilder
            .Entity<ExecutorsListView>()
            .ToView("ExecutorsListView")
            .HasNoKey();

            modelBuilder
            .Entity<CategoriesListView>()
            .ToView("CategoriesListView")
            .HasNoKey();

            modelBuilder
            .Entity<YearsView>()
            .ToView("YearsView", "events")
            .HasNoKey();

            modelBuilder.Entity<SectionsCategoryMapps>().HasKey(u => new { u.category_id, u.section_id });

            modelBuilder.Entity<GetScaleStr>(e => e.HasNoKey());

            modelBuilder.HasDbFunction(() => fnt_GetKSIOLocalNames(default));

            //modelBuilder.Entity<Article>().HasData(new Article
            //{
            //    Id = new Guid("716C2E99-6F6C-4472-81A5-43C56E11637C"),
            //    Title = "Новый спутник запущен на орбиту",
            //    Text = "text text"
            //});
        }

        //[DbFunction]
        //public static string fn_GetDirLink(string unom_num)
        //{
        //    throw new NotImplementedException();
        //}

        public IQueryable<KSIO_Names> fnt_GetKSIOLocalNames(string unom_num) => FromExpression(() => fnt_GetKSIOLocalNames(unom_num));
        public virtual DbSet<GetScaleStr> scale_str { get; set; }
        public class GetScaleStr
        {
            public string? scale_str { get; set; }
        }

        public virtual DbSet<KSIO_Names> ksio_names { get; set; }

        [Keyless]
        public class KSIO_Names
        {
            public string? ksio_name { get; set; }
        }

    }


    public class Constants
    {
        // Проверка подлинности Windows
        public static string SqlConnectionIntegratedSecurity
        {
            get
            {
                var sb = new SqlConnectionStringBuilder
                {
                    DataSource = "(localdb)\\mssqllocaldb",
                    // Подключение будет с проверкой подлинности пользователя Windows
                    IntegratedSecurity = true,
                    // Название целевой базы данных.
                    InitialCatalog = "master"
                };

                return sb.ConnectionString;
            }
        }


        // Проверка подлинности SQL сервером
        public static string SqlConnectionSQLServer
        {
            get
            {
                var sb = new SqlConnectionStringBuilder
                {
                    DataSource = "Путь к серверу SQL",
                    IntegratedSecurity = false,
                    InitialCatalog = "DBMSSQL",
                    UserID = "Имя пользователя",
                    Password = "Пароль"
                };

                return sb.ConnectionString;
            }
        }
    }
}