using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Areas.TSO.Models;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Filters;

namespace WebProject.Areas.HeatPointsAndConsumers.Controllers
{
    [TypeFilter(typeof(ControllerActionFilter))]
    [Area("HeatPointsAndConsumers")]
    public class ContractController : Controller
    {
        private readonly ILogger<ContractController> _logger;
        private readonly HssDbContext _context;
        private readonly ApplicationDbContext _context2;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly string? _user;
        private int userId;
        private string? userDisplayName;
        private readonly HSSController _m_c;

        public ContractController(ILogger<ContractController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
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
                    userDisplayName = "Сергеев Андрей Сергеевич";
                }
            }
        }

        public IActionResult ContractList()
        {
            return View();
        }

		#region ViewComponents
		public ActionResult OnGetCallContractList_PartialViewComponent(int data_status, byte only_liquidate)
		{
			return ViewComponent("ContractList_Partial", new { data_status, only_liquidate });
		}
        #endregion

        #region OpenPopups
        public async Task<IActionResult> OpenContract(int data_status, int contract_id, string action_for = "")
        {
			if (data_status == 0)
			{
				data_status = _m_c.GetCurrentDS();
			}
			var _contract = (await _context.ContractOneDataViewModels.FromSqlInterpolated($"exec consumers.sp_GetContractOneData {data_status},{contract_id}")
                                .ToListAsync()).FirstOrDefault() ?? new ContractOneDataViewModel();

            _contract.data_status = data_status;
			ViewBag.Action_for = action_for;
			if (action_for == "copy")
				_contract.contract_id = 0;
			else
				_contract.contract_id = contract_id;
			ViewBag.UnomContractList = _context.fnt_GetContractUnomList(data_status).ToList();
            ViewBag.TSOList = _context.fnt_GetTSOName(data_status).ToList() ;

			return PartialView("OpenContract", _contract);
        }
		#endregion

		// Сохранение договоров
		[ValidateAntiForgeryToken]
		[TypeFilter(typeof(ControllerActionFilterCheckDS))]
		public async Task<IActionResult> Contract_Save(ContractOneDataViewModel model)
		{
            int contr_id = 0;
            bool is_new = false;
			var contr_upd = await _context.Contracts.Where(x => x.contract_id == model.contract_id).FirstOrDefaultAsync();

			try
			{
                
				if (contr_upd != null)
                {
                    contr_upd.contract_num = model.contract_num;
                    contr_upd.tso_id = model.tso_id;
                    contr_upd.contract_date = model.contract_date;
                    contr_upd.contract_valid_date = model.contract_valid_date;
                    contr_upd.org_name = model.org_name;
                    contr_upd.org_inn = model.org_inn;
                    contr_upd.is_deleted = false;
                    contr_upd.edit_date = DateTime.Now;
                    contr_upd.user_id = userId;

                    var contr_h_upd = await _context.Contracts_Histories.Where(x => x.contract_id == model.contract_id && x.data_status == model.data_status).FirstOrDefaultAsync();

                    if (contr_h_upd != null)
                    {
                        contr_h_upd.contract_status_id = model.contract_status_id;
                        contr_h_upd.edit_date = DateTime.Now;
                        contr_h_upd.user_id = userId;
                    }
                    else
                    {
                        contr_h_upd = new Contracts_History();
                        contr_h_upd.contract_id = model.contract_id;
                        contr_h_upd.data_status = model.data_status;
						contr_h_upd.contract_status_id = model.contract_status_id;
						contr_h_upd.create_date = DateTime.Now;
						contr_h_upd.user_id = userId;
                        await _context.AddAsync(contr_h_upd);
					}
                    
                    contr_id = contr_upd.contract_id;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    Contracts contr_new = new Contracts();
					contr_new.contract_num = model.contract_num;
					contr_new.tso_id = model.tso_id;
					contr_new.contract_date = model.contract_date;
					contr_new.contract_valid_date = model.contract_valid_date;
					contr_new.org_name = model.org_name;
					contr_new.org_inn = model.org_inn;
					contr_new.is_deleted = false;
					contr_new.create_date = DateTime.Now;
					contr_new.user_id = userId;

                   await _context.AddAsync(contr_new);
                   await _context.SaveChangesAsync();

					contr_id = contr_new.contract_id;

					var contr_h_new = new Contracts_History();

                    contr_h_new.contract_id = contr_id;
                    contr_h_new.data_status = model.data_status;
					contr_h_new.contract_status_id = model.contract_status_id;
					contr_h_new.create_date = DateTime.Now;
					contr_h_new.user_id = userId;

					await _context.AddAsync(contr_h_new);
					await _context.SaveChangesAsync();
                    is_new = true;
				}

				return Json(new { success = true, is_new, model.data_status, contr_id });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("Contract_Save", $"data_status={model.data_status} contract_id={contr_id}", ex.Message, userId);
				return Json(new { success = false });
			}
		}
	}
}
