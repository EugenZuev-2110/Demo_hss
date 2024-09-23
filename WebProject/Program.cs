using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebProject.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDbContext<HssDbContext>((serviceProvider, dbContextBuilder) =>
{
    var connectionStringPlaceHolder = builder.Configuration.GetConnectionString("HSSConnection");
    var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();

    var dbName = "";

    httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("dbName", out dbName);

    if (dbName == null)
        dbName = "HeatSupplyScheme";
    //if (httpContextAccessor.HttpContext.Request.Headers.Any(h => h.Key.Equals("dbName", StringComparison.InvariantCultureIgnoreCase)))
    //dbName = httpContextAccessor.HttpContext.Request.Headers["dbName"].First();

    var connectionString_hss = connectionStringPlaceHolder.Replace("{dbName}", dbName);
    dbContextBuilder.UseSqlServer(connectionString_hss);
});

builder.Services.Configure<IISServerOptions>(options =>
{
	options.AllowSynchronousIO = true;
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers().AddControllersAsServices();
//builder.Services.AddScoped<HssDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseExceptionHandler(
	options =>
	{
		options.Run(
			async context =>
			{
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				context.Response.ContentType = "text/html";
				var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
				if (null != exceptionObject)
				{
					//var errorMessage = $"<b>Exception Error: {exceptionObject.Error.Message} </b> {exceptionObject.Error.StackTrace}";
					var errorMessage = $"{exceptionObject.Error.Message}";
					await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);
				}
			});
	}
);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "events_area",
    areaName: "Events",
    pattern: "Events/{controller=Home}/{action=Events}/{id?}");

app.MapAreaControllerRoute(
    name: "ret_area",
    areaName: "RET",
    pattern: "RET/{controller=Home}/{action=MainRET}/{id?}");

app.MapAreaControllerRoute(
    name: "tso",
    areaName: "TSO",
    pattern: "TSO/{controller=Main}/{action=TSOList}/{id?}");

app.MapControllerRoute(
    name: "areaRoute",
    pattern: "{area:exists}/{controller}/{action}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
	name: "outputForms",
	areaName: "OutputForms",
	pattern: "OutputForms/{controller=Main}/{action=OutputFormsList}/{id?}");

app.MapAreaControllerRoute(
    name: "dictionaryTables",
    areaName: "DictionaryTables",
    pattern: "DictionaryTables/{controller=TerritorialDivision}/{action=TerritorialDivisionList}/{id?}");

app.MapAreaControllerRoute(
	name: "heatPointsAndConsumers",
	areaName: "HeatPointsAndConsumers",
	pattern: "HeatPointsAndConsumers/{controller=HeatPoint}/{action=HeatPointMainView}/{id?}");

app.MapAreaControllerRoute(
	name: "hPConsumers",
	areaName: "HPConsumers",
	pattern: "HPConsumers/{controller=Consumers}/{action=ConsumersMainView}/{id?}");

app.MapRazorPages();

app.Run();
