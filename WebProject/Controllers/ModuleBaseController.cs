using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using WebProject.Areas.DictionaryTables.Controllers;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Areas.HPConsumers.Models;
using WebProject.Data;
using WebProject.Filters;
using listOf = System.Collections.Generic.List<object>;

namespace WebProject.Controllers
{
    //Базовый контроллер для модулей  
    public class ModuleBaseController : Controller
    {
        protected readonly ILogger<TariffZoneController> _logger;
        protected readonly HssDbContext _context;
        protected readonly ApplicationDbContext _context2;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IWebHostEnvironment _hostingEnvironment;
        protected readonly string? _user;
        protected int userId;
        protected bool _is_new_consumer = false;
        protected string? userDisplayName;
        protected readonly HSSController _m_c;

        public ModuleBaseController(ILogger<TariffZoneController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
        {
            _logger = logger;
            _context = context;
            _context2 = context2;
            _httpContextAccessor = httpContextAccessor;
            _user = _httpContextAccessor.HttpContext.User.Identity.Name;
            _hostingEnvironment = hostingEnvironment;
            _m_c = m_c;
            if (_user != null)
            {
                var user = _context2.DictWinUsers.Where(x => x.UserLogin == _user).FirstOrDefault();
                userDisplayName = user.UserName;
                userId = user.Id;
            }
            else
            {
                string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                if (host.Contains("localhost"))
                {
                    userId = 1;
                    userDisplayName = "Ермошин Виктор Анатольевич";
                }
            }
        }

        [ValidateAntiForgeryToken]
        [TypeFilter(typeof(ControllerActionFilterCheckDS))]
        public async Task<IActionResult> Save()
        {
            StringValues type_full_name = StringValues.Empty;
            try
            {
                type_full_name = Request.Form.First(n => n.Key == "FullName").Value; // Задаем переменной type_full_name значение из представления (<input hidden asp-for="@Model.GetType().FullName">).
                Type type = Type.GetType(type_full_name); // Получаем тип.
                var obj = Activator.CreateInstance(type) as IActionController ?? throw new InvalidCastException(); // Создаем объект на основе полученного типа.
                var value_list = Request.Form; // Получаем список значений из формы.

                if (type != null && (type.BaseType.Name == typeof(listOf).Name || type.Name == typeof(listOf).Name))
                    return await obj.SaveAsync(_context, userId, value_list);
                else
                    return await SaveSingleAsync(obj, value_list);
            }
            catch (InvalidCastException){ throw new Exception("Ошибка при сохранении данных\r\n" + type_full_name + " не реализует интерфейс IActionController"); }
            catch (InvalidOperationException){ throw new Exception("InvalidOperationException\r\nВ представлении нет <input hidden asp-for=\"@Model.GetType().FullName\">");}
            catch (Exception ex){ return new JsonResult(new { success = false }); }
        }

        private async Task<IActionResult> SaveSingleAsync(IActionController obj, IFormCollection value_list)
        {
            var prop_list = obj.GetType().GetProperties(); // Получаем список свойств объекта, который мы создали(все значения пустые).
            
            await Task.Run(() =>
            {
                foreach (var prop in prop_list) // Перебираем список свойств созданного нами объекта, находим соответствующие свойства в списке значений из формы и устанавливаем соответствующие значения каждому свойству
                {
                    object? input_type;
                    if (prop.PropertyType != typeof(string))
                        input_type = Activator.CreateInstance(prop.PropertyType) ?? Activator.CreateInstance(Nullable.GetUnderlyingType(prop.PropertyType));
                    else
                        input_type = typeof(string).Name;

                    var input_value = value_list.FirstOrDefault(n => n.Key == prop.Name).Value.ToString();
                    if (input_value == null || input_value == "")
                        continue;
                    var output_type = Convert.ChangeType(input_value, input_type.GetType());
                    prop.SetValue(obj, output_type);
                }
            });

            return await obj.SaveAsync(_context, userId);
        }
    }
}