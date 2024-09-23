using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Areas.HPConsumers.Models;
using WebProject.Areas.TSO.Models;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Filters;

namespace WebProject.Areas.HeatPointsAndConsumers.Controllers
{
    [TypeFilter(typeof(ControllerActionFilter))]
    [Area("HPConsumers")]
    public class ConsumersController : Controller
    {
        private readonly ILogger<ConsumersController> _logger;
        private readonly HssDbContext _context;
        private readonly ApplicationDbContext _context2;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly string? _user;
        private int userId;
        private bool _is_new_consumer = false;
        private bool _is_new_contract_load = false;
        private bool _is_new_contract_cons = false;
        private string? userDisplayName;
        private readonly HSSController _m_c;
        private TZHeaderTariffDataViewModel _tariffConnectionViewModel;
        //private PrincipalContext _ctx = new PrincipalContext(ContextType.Domain);
        public ConsumersController(ILogger<ConsumersController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
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
                    userDisplayName = "Зуев Евгений Александрович";
                }
            }
        }

        public IActionResult ConsumersMainView()
        {
            return View();
        }

        #region OpenPopups
        public async Task<IActionResult> OpenHeatPointSwitch(int data_status)
        {
            try
            {
                ViewBag.data_status = data_status;
                ViewBag.TZSourcesList = await _context.fnt_GetSourcesByHPList(data_status).ToListAsync();
                ViewBag.PerspectiveYearsList = _m_c.GetPerspectiveYearsList(data_status);
            }
            catch (Exception ex)
            {
                //_m_c.ExLog_Save("OpenTariffConnection", $"id={id},data_status={data_status},perspective_year={perspective_year}", ex.Message, userId);
            }
            return PartialView();
        }

        public async Task<IActionResult> OpenConsumerAddRemove(int data_status, int consumer_id)
        {
            var _hpHeaderAddRemoveDataViewModel = (await _context.ConsumerHeaderAddRemoveDataViewModel.FromSqlInterpolated($"exec consumers.sp_GetConsumersHeaderAddRemoveDataOne {data_status},{consumer_id}")
                            .ToListAsync()).FirstOrDefault() ?? new ConsumerHeaderAddRemoveDataViewModel() { data_status = data_status, consumer_id = consumer_id };

            try
            {
                ViewBag.ConsumerNumberAddressList = await _context.fnt_GetUnomConsumerAddressListByChars("", data_status).ToListAsync();
            }
            catch (Exception ex)
            {
                //_m_c.ExLog_Save("OpenTariffConnection", $"id={id},data_status={data_status},perspective_year={perspective_year}", ex.Message, userId);
            }
            return PartialView(_hpHeaderAddRemoveDataViewModel);
        }

        public async Task<IActionResult> OpenLoadsAndExpensives(int data_status, int heat_point_id)
        {
            var _hpHeaderAddRemoveDataViewModel = (await _context.HPHeaderAddRemoveDataViewModel.FromSqlInterpolated($"exec heat_points.sp_GetHpHeaderAddRemoveDataOne {data_status},{heat_point_id}")
                            .ToListAsync()).FirstOrDefault() ?? new HPHeaderAddRemoveDataViewModel() { data_status = data_status, hp_id = heat_point_id };

            try
            {
                ViewBag.HpNumberAddressList = await _context.fnt_GetUnomTpAddressListByChars("", data_status).ToListAsync();
            }
            catch (Exception ex)
            {
                //_m_c.ExLog_Save("OpenTariffConnection", $"id={id},data_status={data_status},perspective_year={perspective_year}", ex.Message, userId);
            }
            return PartialView(_hpHeaderAddRemoveDataViewModel);
        }

        #endregion

        [HttpPost]
        public async Task<IActionResult> GetCalcPurposeTypes(int main_purpose_type_id)
        {
            var list = await _context.Dict_CalcPurposeTypes.Where(n => n.main_purpose_type_id == main_purpose_type_id).ToListAsync();
            return Json(new { list });
        }

        #region ViewComponents

        public ActionResult OnGetConsumersList_PartialViewComponent(int data_status)
        {
            return ViewComponent("ConsumersList_Partial", new { data_status });
        }

        //Состав оборудования. Теплообменное оборудование
        public ActionResult OnGetConsumers_DevProgPart_ChangeDevMainProg_ViewComponent(int data_status, int consumer_id, int dev_prog_id)
        {
            return ViewComponent("Consumers_PerspectiveDev_SubPartial", new { data_status, consumer_id, dev_prog_id });
        }

        public ActionResult OnGetConsumers_DevProgPart_ChangeDevMainProgBySelect_ViewComponent(int data_status, int consumer_id)
        {
            return ViewComponent("Consumers_PerspectiveDevelopment_Partial", new { data_status, consumer_id });
        }

        //Состав оборудования. Насосное оборудование
        public ActionResult OnGetConsumers_ChangeDevYearParamsCalcLoad_ViewComponent(int data_status, int consumer_id)
        {
            return ViewComponent("Consumers_YearParamsCalcLoad_Partial", new { data_status, consumer_id });
        }

        //Учет ресурсов
        public ActionResult OnGetHP_EquipmentPart_HP_AccResources_ViewComponent(int data_status, int heat_point_id)
        {
            return ViewComponent("HP_EquipmentPart_AccResources_Partial", new { data_status, heat_point_id });
        }

        //Документация и фотоматериалы по ТП
        public ActionResult OnGetHP_HP_DocsFootage_ViewComponent(int data_status, int heat_point_id)
        {
            return ViewComponent("HP_DocsFootage_Partial", new { data_status, heat_point_id });
        }

        //Нагрузки и расходы
        public ActionResult OnGet_LoadExpensive_ViewComponent(int data_status, int[] perspective_years, int load_type = 2, int hp_type_id = -1, int hp_status_id = -1, int source_id = -1, int tso_id = -1)
        {
            return ViewComponent("LoadExpensiveList_Partial", new { data_status, perspective_years, hp_type_id, load_type, hp_status_id, source_id, tso_id });
        }

        //Оборудование
        public ActionResult OnGet_Equipment_ViewComponent(int data_status, int perspective_year, int hp_type_id = -1, int hp_status_id = -1, int source_id = -1, int tso_id = -1)
        {
            return ViewComponent("HeatPointsEquipment_Partial", new { data_status, perspective_year, hp_type_id, hp_status_id, source_id, tso_id });
        }

        //Схема присоединения нагрузок
        public ActionResult OnGet_LoadAttachmentSchemas_ViewComponent(int data_status, int perspective_year, int hp_type_id = -1, int hp_status_id = -1, int source_id = -1, int tso_id = -1)
        {
            return ViewComponent("HeatPointsLoadAttachmentSchemas_Partial", new { data_status, perspective_year, hp_type_id, hp_status_id, source_id, tso_id });
        }

        //Автоматизация
        public ActionResult OnGet_Automatization_ViewComponent(int data_status, int perspective_year, int hp_type_id = -1, int hp_status_id = -1, int source_id = -1, int tso_id = -1)
        {
            return ViewComponent("HeatPointsAutomatization_Partial", new { data_status, perspective_year, hp_type_id, hp_status_id, source_id, tso_id });
        }

        //Учёт энергетических ресурсов
        public ActionResult OnGet_EnergyResourceAccounting_ViewComponent(int data_status, int perspective_year, int hp_type_id = -1, int hp_status_id = -1, int source_id = -1, int tso_id = -1)
        {
            return ViewComponent("HeatPointsEnergyResourceAccounting_Partial", new { data_status, perspective_year, hp_type_id, hp_status_id, source_id, tso_id });
        }

        #endregion

        public async Task<IActionResult> ShowHeatPointSwitch(string heat_point_id_list, int data_status, int perspective_year_before, int perspective_year_after)
        {
            List<HP_SwitchableUnit> list;

            try
            {
                list = await _context.HP_SwitchableUnit.FromSqlInterpolated
                    ($"exec sources.sp_GetListSwitchHPTable {heat_point_id_list},{data_status}, {perspective_year_before},{perspective_year_after}").ToListAsync();
            }
            catch (Exception ex)
            {
                list = new List<HP_SwitchableUnit>();
                //_m_c.ExLog_Save("OpenTariffConnection", $"id={id},data_status={data_status},perspective_year={perspective_year}", ex.Message, userId);
            }

            return Json(new { list });
        }

        #region SAVE_DATA HEAT POINT SWITCHING

        // Сохранение значений при переключении ТП
        [ValidateAntiForgeryToken]
        [TypeFilter(typeof(ControllerActionFilterCheckDS))]
        public async Task<IActionResult> HpSwitching_Save(IFormCollection model)
        {
            var outer_sources_list = (model["hpSwitchParamNumberTso"]).ToString().Split(" ");
            //var selected_source_id = Convert.ToInt32(model["hpSwitchSourcesList"]);
            var selected_output_id = Convert.ToInt32(model["hpSwitchSourceHeadsList"]);
            var date_start = Convert.ToInt32(model["hpSwitchSourcePerspectiveYearsListStart"]);
            var date_end = Convert.ToInt32(model["hpSwitchSourcePerspectiveYearsListFinish"]);
            var data_status = Convert.ToInt32(model["hp_data_status"]);
            var dto = _context.HP_Perspective;

            try
            {
                for (int i = 0; i < outer_sources_list.Length; i++)
                {
                    var sources = await dto.Where(n => n.data_status == data_status && n.heat_point_id == Convert.ToInt32(outer_sources_list[i])
                        && n.perspective_year >= date_start && n.perspective_year <= date_end).ToListAsync();

                    if (sources.Count > 0)
                    {
                        foreach (var s in sources)
                        {
                            s.source_output_id = selected_output_id;
                            s.edit_date = DateTime.Now;
                            s.user_id = userId;
                        }

                        await _context.SaveChangesAsync();
                    }
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                //_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
                return Json(new { success = false });
            }
        }

        #endregion

        #region SAVE_DATA ADD REMOVE DESTINATION AND RELATION'S CATEGORY

        // Сохранение значений при добавлении или удалении назначения и категории надежности
        [ValidateAntiForgeryToken]
        [TypeFilter(typeof(ControllerActionFilterCheckDS))]
        public async Task<IActionResult> ConsumerAddRemove_DestRelCatHSData_Save(Consumers_DestRelCatHSDataViewModel model)
        {
            try
            {
                await SetAllParameters_AddRemove_DestRelCatHSData(model);
                await _context.SaveChangesAsync();

                if (model.is_deleted != null && (bool)model.is_deleted)
                    return Json(new { success = true, is_deleted = true, _is_new_consumer });

                return Json(new { success = true, is_deleted = false, _is_new_consumer });
            }
            catch (Exception ex)
            {
                //_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
                return Json(new { success = false });
            }
        }

        private async Task SetAllParameters_AddRemove_DestRelCatHSData(Consumers_DestRelCatHSDataViewModel model)
        {
            var cons_hist = await _context.ConsumersHistory.Where(x => x.data_status == model.data_status && x.consumer_id == model.consumer_id && x.is_deleted != true).FirstOrDefaultAsync();

            try
            {
                if (cons_hist == null)
                {
                    cons_hist = await Create_DestRelCatHSData_ConsumersHistory(model);
                    _is_new_consumer = true;
                }
                else
                {
                    SetExistModel_DestRelCatHSData_Consumers_History(cons_hist, model);
                    _context.ConsumersHistory.Update(cons_hist);
                }
            }
            catch (Exception ex) { }
        }

        private void SetExistModel_DestRelCatHSData_Consumers_History(ConsumersHistory history, Consumers_DestRelCatHSDataViewModel model, bool isNewData = false)
        {
            history.type_purpose_id = model.type_purpose_id;
            history.category_rel_id = model.category_rel_id;
            history.purpose_name = model.purpose_name;
            history.is_in_soc_obj = model.is_in_soc_obj;
            history.type_prod_supply_id = model.type_prod_supply_id;

            if (!isNewData)
                history.edit_date = DateTime.Now;
            history.user_id = userId;
            history.is_deleted = model.is_deleted ?? false;
        }

        private async Task<ConsumersHistory?> Create_DestRelCatHSData_ConsumersHistory(Consumers_DestRelCatHSDataViewModel model)
        {
            try
            {
                var new_cons_hist = new ConsumersHistory { data_status = model.data_status, consumer_id = model.consumer_id, create_date = DateTime.Now };
                SetExistModel_DestRelCatHSData_Consumers_History(new_cons_hist, model, true);
                await _context.ConsumersHistory.AddAsync(new_cons_hist);
                await _context.SaveChangesAsync();
                return new_cons_hist;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region SAVE_DATA ADD REMOVE HEAT LOADS SUPPLY CONTRACT

        // Сохранение значений при добавлении или удалении тепловых нагрузок по договору на теплоснабжение
        [ValidateAntiForgeryToken]
        [TypeFilter(typeof(ControllerActionFilterCheckDS))]
        public async Task<IActionResult> ConsumerAddRemove_HeatLoadsSupplyContract_Save(Consumers_HeatLoadsSupplyContractDataViewModel model)
        {
            try
            {
                await SetAllParameters_AddRemove_HeatLoadsSupplyContract(model);
                await _context.SaveChangesAsync();

                return Json(new { success = true, is_deleted = false, _is_new_contract_load });
            }
            catch (Exception ex)
            {
                //_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
                return Json(new { success = false });
            }
        }

        private async Task SetAllParameters_AddRemove_HeatLoadsSupplyContract(Consumers_HeatLoadsSupplyContractDataViewModel model)
        {
            var cont_loads = await _context.ContractLoads.Where(x => x.data_status == model.data_status && x.consumer_id == model.consumer_id).FirstOrDefaultAsync();
            var cont_cons = await _context.ContractConsumers.Where(x => x.data_status == model.data_status && x.consumer_id == model.consumer_id).FirstOrDefaultAsync();

            try
            {
                if (cont_loads == null)
                {
                    cont_loads = await Create_HeatLoadsSupplyContract_ContractLoads(model);
                    _is_new_contract_load = true;
                }
                else
                {
                    SetExistModel_HeatLoadsSupplyContract_ContractLoads(cont_loads, model);
                    _context.ContractLoads.Update(cont_loads);
                }

                if (cont_cons == null)
                {
                    cont_cons = await Create_HeatLoadsSupplyContract_ContractConsumers(model);
                    _is_new_contract_cons = true;
                }
                else
                {
                    SetExistModel_HeatLoadsSupplyContract_ContractConsumers(cont_cons, model);
                    _context.ContractConsumers.Update(cont_cons);
                }
            }
            catch (Exception ex) { }
        }

        private async Task<ContractLoads?> Create_HeatLoadsSupplyContract_ContractLoads(Consumers_HeatLoadsSupplyContractDataViewModel model)
        {
            try
            {
                var new_cons_loads = new ContractLoads { data_status = model.data_status, consumer_id = model.consumer_id, create_date = DateTime.Now };
                SetExistModel_HeatLoadsSupplyContract_ContractLoads(new_cons_loads, model, true);
                await _context.ContractLoads.AddAsync(new_cons_loads);
                await _context.SaveChangesAsync();
                return new_cons_loads;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void SetExistModel_HeatLoadsSupplyContract_ContractLoads(ContractLoads contract, Consumers_HeatLoadsSupplyContractDataViewModel model, bool isNewData = false)
        {
            contract.contract_id = model.contract_id;
            contract.ctr_hl_heat_hw = model.ctr_hl_heat_hw;
            contract.ctr_hl_vent_hw = model.ctr_hl_vent_hw;
            contract.ctr_hl_gvsm_hw = model.ctr_hl_gvsm_hw;
            contract.ctr_hl_gvss_hw = model.ctr_hl_gvss_hw;
            contract.ctr_hl_tech_hw = model.ctr_hl_tech_hw;
            contract.ctr_hl_other_hw = model.ctr_hl_other_hw;
            contract.ctr_hl_cond_hw = model.ctr_hl_cond_hw;
            contract.ctr_hl_curtains_hw = model.ctr_hl_curtains_hw;
            contract.ctr_hl_heat_steam = model.ctr_hl_heat_steam;
            contract.ctr_hl_vent_steam = model.ctr_hl_vent_steam;
            contract.ctr_hl_gvsm_steam = model.ctr_hl_gvsm_steam;
            contract.ctr_hl_gvss_steam = model.ctr_hl_gvss_steam;
            contract.ctr_hl_tech_steam = model.ctr_hl_tech_steam;
            contract.ctr_hl_other_steam = model.ctr_hl_other_steam;
            contract.ctr_hl_cond_steam = model.ctr_hl_cond_steam;
            contract.ctr_hl_curtains_steam = model.ctr_hl_curtains_steam;

            if (!isNewData)
                contract.edit_date = DateTime.Now;
            contract.user_id = userId;
        }

        private async Task<ContractConsumers?> Create_HeatLoadsSupplyContract_ContractConsumers(Consumers_HeatLoadsSupplyContractDataViewModel model)
        {
            try
            {
                var new_cons_loads = new ContractConsumers { data_status = model.data_status, consumer_id = model.consumer_id, create_date = DateTime.Now };
                SetExistModel_HeatLoadsSupplyContract_ContractConsumers(new_cons_loads, model, true);
                await _context.ContractConsumers.AddAsync(new_cons_loads);
                await _context.SaveChangesAsync();
                return new_cons_loads;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void SetExistModel_HeatLoadsSupplyContract_ContractConsumers(ContractConsumers contract, Consumers_HeatLoadsSupplyContractDataViewModel model, bool isNewData = false)
        {
            contract.contract_id = model.contract_id;
            contract.address = model.address;
            contract.purpose = model.purpose;
            contract.bti_build_unom = model.bti_build_unom;

            if (!isNewData)
                contract.edit_date = DateTime.Now;
            contract.user_id = userId;
        }

        #endregion

        #region SAVE_DATA ADD REMOVE GEN INFO

        // Сохранение значений при добавлении или удалении общих сведений ТП
        [ValidateAntiForgeryToken]
        [TypeFilter(typeof(ControllerActionFilterCheckDS))]
        public async Task<IActionResult> ConsumerAddRemove_GenInfoData_Save(ConsumerAddRemoveGenInfoDataViewModel model)
        {
            try
            {
                var cons_id = await SetAllParameters_AddRemove_GenInfoData(model);
                await _context.SaveChangesAsync();

                if (model.is_deleted != null && (bool)model.is_deleted)
                    return Json(new { success = true, is_deleted = true, _is_new_consumer });

                var address = await _context.fnt_GetDistrictUnomAddress((int)cons_id, model.data_status).FirstOrDefaultAsync();
                return Json(new { success = true, is_deleted = false, _is_new_consumer, consumer_id = cons_id, unom = address.unom_addr, district = address.district });
            }
            catch (Exception ex)
            {
                //_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
                return Json(new { success = false });
            }
        }

        private async Task<int?> SetAllParameters_AddRemove_GenInfoData(ConsumerAddRemoveGenInfoDataViewModel model)
        {
            var cons_hist = await _context.ConsumersHistory.Where(x => x.data_status == model.data_status && x.consumer_id == model.consumer_id).FirstOrDefaultAsync();
            var cons = await _context.Consumers.Where(x => x.consumer_id == model.consumer_id).FirstOrDefaultAsync();

            try
            {
                if (cons_hist == null && cons != null)
                {
                    cons_hist = await CreateConsumersHistory(model, cons.consumer_id);
                    SetExistModel_GenInfoData_Consumer(cons, model);
                    _context.Consumers.Update(cons);
                }
                else if (cons_hist == null && cons == null)
                {
                    cons = await CreateConsumer(model);
                    cons_hist = await CreateConsumersHistory(model, cons.consumer_id);
                    _is_new_consumer = true;
                }
                else
                {
                    SetExistModel_GenInfoData_Consumers_History(cons_hist, model);
                    SetExistModel_GenInfoData_Consumer(cons, model);
                    _context.ConsumersHistory.Update(cons_hist);
                    _context.Consumers.Update(cons);
                }
            }
            catch (Exception ex) { }

            return cons_hist.consumer_id;
        }

        private void SetExistModel_GenInfoData_Consumers_History(ConsumersHistory history, ConsumerAddRemoveGenInfoDataViewModel model, bool isNewData = false)
        {
            history.manag_org_id = model.add_remove_gen_info_cons_org_owner_id;

            if (!isNewData)
                history.edit_date = DateTime.Now;
            history.user_id = userId;
            history.is_deleted = model.is_deleted ?? false;
        }

        private void SetExistModel_GenInfoData_Consumer(Consumers consumer, ConsumerAddRemoveGenInfoDataViewModel model, bool isNewData = false)
        {
            consumer.building_id = model.add_remove_gen_info_building_id;

            if (!isNewData)
                consumer.edit_date = DateTime.Now;
            consumer.user_id = userId;
        }

        private async Task<ConsumersHistory?> CreateConsumersHistory(ConsumerAddRemoveGenInfoDataViewModel model, int consumer_id)
        {
            try
            {
                var new_cons_hist = new ConsumersHistory { data_status = model.data_status, consumer_id = consumer_id, create_date = DateTime.Now };
                SetExistModel_GenInfoData_Consumers_History(new_cons_hist, model, true);
                await _context.ConsumersHistory.AddAsync(new_cons_hist);
                await _context.SaveChangesAsync();
                return new_cons_hist;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task<Consumers?> CreateConsumer(ConsumerAddRemoveGenInfoDataViewModel model)
        {
            try
            {
                var cons_hp = new Consumers { create_date = DateTime.Now };
                SetExistModel_GenInfoData_Consumer(cons_hp, model, true);
                await _context.Consumers.AddAsync(cons_hp);
                await _context.SaveChangesAsync();
                return cons_hp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region SAVE_DATA ADD REMOVE YEAR IMPLEMENT SCHEME PARAM

        // Сохранение значений при добавлении или удалении общих сведений ТП
        [ValidateAntiForgeryToken]
        [TypeFilter(typeof(ControllerActionFilterCheckDS))]
        public async Task<IActionResult> Consumer_YearParamsCalcLoadData_Save(IFormCollection model)
        {
            try
            {
                var dict = YearParamsCalcLoadData_CreateModelsDict(model);
                await YearParamsCalcLoadData_CreateDataRows(dict);
                var _data_status = Convert.ToInt16(dict["data_status"][0]);
                var _consumer_id = Convert.ToInt16(dict["consumer_id"][0]);
                return Json(new { success = true, data_status = _data_status, consumer_id = _consumer_id });
            }
            catch (Exception ex)
            {
                //_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
                return Json(new { success = false });
            }
        }

        private Dictionary<string, string?[]> YearParamsCalcLoadData_CreateModelsDict(IFormCollection model)
        {
            var dictionary = new Dictionary<string, string?[]>()
            {
                ["calc_on_main_dev_prog"] = model["calc_on_main_dev_prog"].ToArray(),
                ["consumer_main_dev_prog_id"] = model["consumer_main_dev_prog_id"].ToArray(),
                ["consumer_yisp_perspective_year"] = model["consumer_yisp_perspective_year"].ToArray(),
                ["consumer_ypcl_status_select"] = model["consumer_ypcl_status_select"].ToArray(),
                ["consumer_start_year_caprepair"] = model["consumer_start_year_caprepair"].ToArray(),
                ["consumer_ypcl_area"] = model["consumer_ypcl_area"].ToArray(),
                ["consumer_ypcl_increase_area"] = model["consumer_ypcl_increase_area"].ToArray(),
                ["consumer_ypcl_unom_source_name"] = model["consumer_ypcl_unom_source_name"].ToArray(),
                ["consumer_ypcl_heat_point_select"] = model["consumer_ypcl_heat_point_select"].ToArray(),
                ["consumer_ypcl_is_open_gvs_select"] = model["consumer_ypcl_is_open_gvs_select"].ToArray(),
                ["calc_hl_tech_hw"] = model["calc_hl_tech_hw"].ToArray(),
                ["calc_hl_heat_hw"] = model["calc_hl_heat_hw"].ToArray(),
                ["calc_hl_vent_hw"] = model["calc_hl_vent_hw"].ToArray(),
                ["calc_hl_gvss_hw"] = model["calc_hl_gvss_hw"].ToArray(),
                ["calc_hl_tech_steam"] = model["calc_hl_tech_steam"].ToArray(),
                ["calc_hl_heat_steam"] = model["calc_hl_heat_steam"].ToArray(),
                ["calc_hl_vent_steam"] = model["calc_hl_vent_steam"].ToArray(),
                ["calc_hl_gvss_steam"] = model["calc_hl_gvss_steam"].ToArray(),
                ["data_status"] = model["tz_list.data_status"].ToArray(),
                ["consumer_id"] = model["tz_list.consumer_id"].ToArray()
            };
            return dictionary;
        }

        private async Task YearParamsCalcLoadData_CreateDataRows(Dictionary<string, string?[]> dict)
        {
            var _data_status = Convert.ToInt16(dict["data_status"][0]);
            var _consumer_id = Convert.ToInt16(dict["consumer_id"][0]);
            var _calc_on_main_dev_prog = dict["calc_on_main_dev_prog"].Length == 0 ? false : Convert.ToBoolean(dict["calc_on_main_dev_prog"][0]);
            int? _consumer_main_dev_prog_id = dict["consumer_main_dev_prog_id"][0] == "" ? null : Convert.ToInt32(dict["consumer_main_dev_prog_id"][0]);
            int? _start_year_caprepair = dict["consumer_start_year_caprepair"][0] == "" ? null : Convert.ToInt32(dict["consumer_start_year_caprepair"][0]);
            bool is_new_load_calc;
            var list = new List<LoadsCalculates>();
            var key_value = await GetConsumerHistoryAsync(_data_status, _consumer_id);
            var is_new_cons_hist = key_value.Key;
            var item = key_value.Value;
            item.start_year_caprepair = _start_year_caprepair;
            item.calc_on_main_dev_prog = _calc_on_main_dev_prog;
            item.main_dev_prog_id = _consumer_main_dev_prog_id;
            await Consumer_NumSignOtherDb_SaveDb(item, is_new_cons_hist);
            int row_count = Convert.ToInt32(dict["consumer_yisp_perspective_year"].Length);

            for (int i = 0; i < row_count; i++)
            {
                is_new_load_calc = false;
                var _perspective_year = Convert.ToInt16(dict["consumer_yisp_perspective_year"][i]);
                LoadsCalculates row;
                row = await _context.LoadsCalculates.FirstOrDefaultAsync(n => n.data_status == _data_status && n.perspective_year == _perspective_year && n.consumer_id == _consumer_id);

                if (row == null)
                {
                    row = new LoadsCalculates();
                    is_new_load_calc = true;
                    row.consumer_id = _consumer_id;
                    row.perspective_year = _perspective_year;
                    row.data_status = _data_status;
                }

                row.heat_point_id = dict["consumer_ypcl_heat_point_select"][i] == "" ? null : Convert.ToInt32(dict["consumer_ypcl_heat_point_select"][i]);
                row.consumer_status_id = dict["consumer_ypcl_status_select"][i] == "" ? null : Convert.ToInt16(dict["consumer_ypcl_status_select"][i]);
                row.area = dict["consumer_ypcl_area"][i] == "" ? null : Convert.ToDecimal(dict["consumer_ypcl_area"][i]);
                row.increase_area = dict["consumer_ypcl_increase_area"][i] == "" ? null : Convert.ToDecimal(dict["consumer_ypcl_increase_area"][i]);
                row.calc_hl_heat_hw = dict["calc_hl_heat_hw"][i] == "" ? null : Convert.ToDecimal(dict["calc_hl_heat_hw"][i]);
                row.calc_hl_vent_hw = dict["calc_hl_vent_hw"][i] == "" ? null : Convert.ToDecimal(dict["calc_hl_vent_hw"][i]);
                row.calc_hl_gvss_hw = dict["calc_hl_gvss_hw"][i] == "" ? null : Convert.ToDecimal(dict["calc_hl_gvss_hw"][i]);
                row.calc_hl_tech_hw = dict["calc_hl_tech_hw"][i] == "" ? null : Convert.ToDecimal(dict["calc_hl_tech_hw"][i]);
                row.calc_hl_heat_steam = dict["calc_hl_heat_steam"][i] == "" ? null : Convert.ToDecimal(dict["calc_hl_heat_steam"][i]);
                row.calc_hl_vent_steam = dict["calc_hl_vent_steam"][i] == "" ? null : Convert.ToDecimal(dict["calc_hl_vent_steam"][i]);
                row.calc_hl_gvss_steam = dict["calc_hl_gvss_steam"][i] == "" ? null : Convert.ToDecimal(dict["calc_hl_gvss_steam"][i]);
                row.calc_hl_tech_steam = dict["calc_hl_tech_steam"][i] == "" ? null : Convert.ToDecimal(dict["calc_hl_tech_steam"][i]);
                row.is_in_open_gvs = dict["consumer_ypcl_is_open_gvs_select"][i] == "" ? null : Convert.ToBoolean(dict["consumer_ypcl_is_open_gvs_select"][i]);
                row.user_id = userId;

                await YearParamsCalcLoadData_SaveDataRow(row, is_new_load_calc);
            }
        }

        private async Task YearParamsCalcLoadData_AddDataRow(LoadsCalculates perspective)
        {
            perspective.create_date = DateTime.Now;
            await _context.LoadsCalculates.AddAsync(perspective);
        }

        private async Task YearParamsCalcLoadData_UpdateDataRow(LoadsCalculates perspective)
        {
            perspective.edit_date = DateTime.Now;
            _context.LoadsCalculates.Update(perspective);
        }

        private async Task YearParamsCalcLoadData_SaveDataRow(LoadsCalculates perspective, bool isNew)
        {
            if (isNew)
                await YearParamsCalcLoadData_AddDataRow(perspective);
            else
                await YearParamsCalcLoadData_UpdateDataRow(perspective);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region SAVE_DATA YEAR HEAT CONSUMPTION 

        // Сохранение значений при добавлении или изменении в Теплопотребление по годам реализации схемы
        [ValidateAntiForgeryToken]
        [TypeFilter(typeof(ControllerActionFilterCheckDS))]
        public async Task<IActionResult> Consumer_YearHeatConsumption_Save(IFormCollection collection)
        {
            try
            {
                var dictionary = Consumer_YearHeatConsumption_CreateModelsDict(collection).Result;
                var model = await Create_YearHeatConsumptionViewModel(dictionary);
                await Consumer_YearHeatConsumption_CreateData(model);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                //_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
                return Json(new { success = false });
            }
        }

        private async Task<Dictionary<string, string?[]>> Consumer_YearHeatConsumption_CreateModelsDict(IFormCollection model)
        {
            var dictionary = new Dictionary<string, string?[]>();

            await Task.Run(() =>
            {
                dictionary.Add("hc_calc_method_id", model["fact.hc_calc_method_id"].ToArray());
                dictionary.Add("fact_watercons_gvs", model["fact.fact_watercons_gvs"].ToArray());
                dictionary.Add("fact_total_hc_steam", model["fact.fact_total_hc_steam"].ToArray());
                dictionary.Add("fact_total_hc_hw", model["fact.fact_total_hc_hw"].ToArray());
                dictionary.Add("consumer_yisp_perspective_year", model["consumer_yisp_perspective_year"].ToArray());
                dictionary.Add("watercons_gvs", model["watercons_gvs"].ToArray());
                dictionary.Add("data_status", model["tz_list.data_status"].ToArray());
                dictionary.Add("consumer_id", model["tz_list.consumer_id"].ToArray());
            });

            return dictionary;
        }

        private async Task<Consumers_YearHeatConsumptionViewModel> Create_YearHeatConsumptionViewModel(Dictionary<string, string?[]> dictionary)
        {
            var _data_status = Convert.ToInt32(dictionary["data_status"][0]);
            var _consumer_id = Convert.ToInt16(dictionary["consumer_id"][0]);
            int row_count = Convert.ToInt32(dictionary["consumer_yisp_perspective_year"].Length);

            var model = new Consumers_YearHeatConsumptionViewModel();
            var fact = new Consumers_YearHeatConsumptionViewModel_Fact
            {
                consumer_id = _consumer_id,
                data_status = _data_status,
                hc_calc_method_id = Convert.ToInt16(dictionary["hc_calc_method_id"][0]),
                fact_total_hc_hw = Convert.ToDecimal(dictionary["fact_total_hc_hw"][0]),
                fact_total_hc_steam = Convert.ToDecimal(dictionary["fact_total_hc_steam"][0]),
                fact_watercons_gvs = Convert.ToDecimal(dictionary["fact_watercons_gvs"][0])
            };

            var perspective_list = new List<Consumers_YearHeatConsumptionViewModel_Perspective>();

            for (int i = 0; i < row_count; i++)
            {
                perspective_list.Add(new Consumers_YearHeatConsumptionViewModel_Perspective
                {
                    data_status = _data_status,
                    consumer_perspective_year = Convert.ToInt32(dictionary["consumer_yisp_perspective_year"][i]),
                    consumer_id = _consumer_id,
                    watercons_gvs = Convert.ToDecimal(dictionary["watercons_gvs"][i]),
                });

            }

            model.fact = fact;
            model.perspective = perspective_list;

            return model;
        }

        private async Task Consumer_YearHeatConsumption_CreateData(Consumers_YearHeatConsumptionViewModel model)
        {
            bool is_new_fact = false;
            bool is_new_perspective = false;
            var fact = await _context.HeatWaterConsumptions_Fact.FirstOrDefaultAsync(n => n.data_status == model.fact.data_status && n.consumer_id == model.fact.consumer_id);
            var perspective_list = await _context.HeatConsumptions_Perspective.Where(n => n.data_status == model.fact.data_status && n.consumer_id == model.fact.consumer_id).ToListAsync();

            if (fact == null)
                is_new_fact = true;
            if (perspective_list == null)
                is_new_perspective = true;

            perspective_list = await SetConsumer_YearHeatConsumption_Perspective(model, perspective_list);
            await Consumer_YearHeatConsumption_SaveDb_Perspective(perspective_list, is_new_perspective);
            fact = await SetConsumer_YearHeatConsumption_Fact(model, fact);
            await Consumer_YearHeatConsumption_SaveDb_Fact(fact, is_new_fact);
        }

        private async Task<HeatWaterConsumptions_Fact> SetConsumer_YearHeatConsumption_Fact(Consumers_YearHeatConsumptionViewModel model, HeatWaterConsumptions_Fact fact)
        {

            if (fact == null)
            {
                fact = new HeatWaterConsumptions_Fact();
                await CreateConsumer_YearHeatConsumption_Fact(model, fact);
            }
            else
                await UpdateConsumer_YearHeatConsumption_Fact(model, fact);

            return fact;
        }

        private async Task UpdateConsumer_YearHeatConsumption_Fact(Consumers_YearHeatConsumptionViewModel model, HeatWaterConsumptions_Fact fact)
        {
            await Task.Run(() =>
            {
                fact.hc_calc_method_id = model.fact.hc_calc_method_id;
                fact.fact_watercons_gvs = model.fact.fact_watercons_gvs;
                fact.fact_total_hc_hw = model.fact.fact_total_hc_hw;
                fact.fact_total_hc_steam = model.fact.fact_total_hc_steam;
                fact.user_id = userId;
            });
        }

        private async Task CreateConsumer_YearHeatConsumption_Fact(Consumers_YearHeatConsumptionViewModel model, HeatWaterConsumptions_Fact fact)
        {
            fact.data_status = model.fact.data_status;
            fact.consumer_id = model.fact.consumer_id;
            await UpdateConsumer_YearHeatConsumption_Fact(model, fact);
        }

        private async Task Consumer_YearHeatConsumption_SaveDb_Fact(HeatWaterConsumptions_Fact fact, bool is_new)
        {
            if (is_new)
            {
                fact.create_date = DateTime.Now;
                await _context.HeatWaterConsumptions_Fact.AddAsync(fact);
            }
            else
            {
                fact.edit_date = DateTime.Now;
                await Task.Run(() => _context.HeatWaterConsumptions_Fact.Update(fact));
            }
            await _context.SaveChangesAsync();
        }

        private async Task<List<HeatConsumptions_Perspective>> SetConsumer_YearHeatConsumption_Perspective(Consumers_YearHeatConsumptionViewModel model, List<HeatConsumptions_Perspective> perspective_list)
        {
            if (perspective_list == null)
            {
                perspective_list = new List<HeatConsumptions_Perspective>();
                await CreateConsumer_YearHeatConsumption_Perspective(model, perspective_list);
            }
            else
                await UpdateConsumer_YearHeatConsumption_Perspective(model, perspective_list);

            return perspective_list;
        }

        private async Task UpdateConsumer_YearHeatConsumption_Perspective(Consumers_YearHeatConsumptionViewModel model, List<HeatConsumptions_Perspective> perspective_list)
        {
            await Task.Run(() =>
            {
                foreach (var perspective in perspective_list)
                {
                    perspective.watercons_gvs = model.perspective.FirstOrDefault(n => n.consumer_perspective_year == perspective.perspective_year).watercons_gvs;
                    perspective.data_status = model.fact.data_status;
                    perspective.consumer_id = model.fact.consumer_id;
                    perspective.user_id = userId;
                }

            });
        }

        private async Task CreateConsumer_YearHeatConsumption_Perspective(Consumers_YearHeatConsumptionViewModel model, List<HeatConsumptions_Perspective> perspective_list)
        {
            perspective_list = new List<HeatConsumptions_Perspective>();

            await Task.Run(() =>
            {
                foreach (var perspective in model.perspective)
                {
                    perspective_list.Add(new HeatConsumptions_Perspective
                    {
                        perspective_year = model.perspective.FirstOrDefault(n => n.consumer_perspective_year == perspective.consumer_perspective_year).consumer_perspective_year,
                        watercons_gvs = model.perspective.FirstOrDefault(n => n.consumer_perspective_year == perspective.consumer_perspective_year).watercons_gvs,
                        hc_heat_hw = model.perspective.FirstOrDefault(n => n.consumer_perspective_year == perspective.consumer_perspective_year).hc_heat_hw,
                        hc_vent_hw = model.perspective.FirstOrDefault(n => n.consumer_perspective_year == perspective.consumer_perspective_year).hc_vent_hw,
                        hc_gvs_hw = model.perspective.FirstOrDefault(n => n.consumer_perspective_year == perspective.consumer_perspective_year).hc_gvs_hw,
                        hc_tech_hw = model.perspective.FirstOrDefault(n => n.consumer_perspective_year == perspective.consumer_perspective_year).hc_tech_hw,
                        hc_heat_steam = model.perspective.FirstOrDefault(n => n.consumer_perspective_year == perspective.consumer_perspective_year).hc_heat_steam,
                        hc_vent_steam = model.perspective.FirstOrDefault(n => n.consumer_perspective_year == perspective.consumer_perspective_year).hc_vent_steam,
                        hc_gvs_steam = model.perspective.FirstOrDefault(n => n.consumer_perspective_year == perspective.consumer_perspective_year).hc_gvs_steam,
                        hc_tech_steam = model.perspective.FirstOrDefault(n => n.consumer_perspective_year == perspective.consumer_perspective_year).hc_tech_steam,
                        data_status = model.perspective.FirstOrDefault(n => n.consumer_perspective_year == perspective.consumer_perspective_year).data_status,
                        consumer_id = model.perspective.FirstOrDefault(n => n.consumer_perspective_year == perspective.consumer_perspective_year).consumer_id,
                        user_id = userId
                    });
                }
            });
        }

        private async Task Consumer_YearHeatConsumption_SaveDb_Perspective(List<HeatConsumptions_Perspective> perspective_list, bool is_new)
        {
            if (is_new)
            {
                foreach (var perspective in perspective_list)
                {
                    perspective.create_date = DateTime.Now;
                    await _context.HeatConsumptions_Perspective.AddAsync(perspective);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                foreach (var perspective in perspective_list)
                {
                    perspective.edit_date = DateTime.Now;
                    await Task.Run(() => _context.HeatConsumptions_Perspective.Update(perspective));
                    await _context.SaveChangesAsync();
                }
            }
        }

        #endregion

        #region SAVE_DATA HEAT_EXCHANGE_EQUIPMENT

        // Сохранение значений при добавлении или изменении в Теплообменное оборудование Состава оборудования
        [ValidateAntiForgeryToken]
        [TypeFilter(typeof(ControllerActionFilterCheckDS))]
        public async Task<IActionResult> HpAddRemove_HeatExchangeEquipment_Save(HPAddRemoveHP_HeatExchangerMappsDataViewModel model)
        {
            try
            {
                await HpAddRemove_HeatExchangeEquipment_CreateData(model);

                if (model.is_deleted != null && (bool)model.is_deleted)
                    return Json(new { success = true, is_deleted = true });

                return Json(new { success = true, is_deleted = false });
            }
            catch (Exception ex)
            {
                //_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
                return Json(new { success = false });
            }
        }

        private async Task HpAddRemove_HeatExchangeEquipment_CreateData(HPAddRemoveHP_HeatExchangerMappsDataViewModel model)
        {
            bool is_new = false;
            var item = await _context.HP_HeatExchangerMapps.FirstOrDefaultAsync
                (n => n.data_status == model.data_status && n.heat_point_id == model.heat_point_id && n.ht_exch_num == model.ht_exch_num);

            if (item == null)
            {
                if (model.ht_exch_num != 0)
                {
                    item = await _context.HP_HeatExchangerMapps.FirstOrDefaultAsync
                        (n => n.heat_point_id == model.heat_point_id && n.ht_exch_num == model.ht_exch_num);
                }
                else
                {
                    var value = await _context.HP_HeatExchangerMapps.Where
                        (n => n.heat_point_id == model.heat_point_id).MaxAsync(n => (short?)n.ht_exch_num) ?? 0;

                    item = new HP_HeatExchangerMapps { ht_exch_num = ++value };
                }
                is_new = true;
            }
            else
                item.ht_exch_num = model.ht_exch_num;

            item.type_equip_id = model.type_equip_id;
            item.type_id = model.type_id;
            item.stage_scheme_gvs_id = model.stage_scheme_gvs_id;
            item.equip_id = model.equip_id;
            item.ht_exch_count = model.ht_exch_count;
            item.heat_exchange_surface = model.heat_exchange_surface;
            item.inst_heat_power = model.inst_heat_power;
            item.section_count = model.section_count;
            item.casing_diameter = model.casing_diameter;
            item.length_section = model.length_section;
            item.heat_point_id = model.heat_point_id;
            item.data_status = model.data_status;
            item.user_id = userId;
            item.is_deleted = model.is_deleted ?? false;

            await HpAddRemove_HeatExchangeEquipment_SaveDb(item, is_new);
            var val = HpAddRemove_HeatExchangeEquipment_CalculateInstHeatPower(model).Result;
            await HpAddRemove_HeatExchangeEquipment_SaveCalculateInstHeatPower(val, model);
        }

        private async Task HpAddRemove_HeatExchangeEquipment_SaveDb(HP_HeatExchangerMapps item, bool is_new)
        {
            if (is_new)
            {
                item.create_date = DateTime.Now;
                await _context.HP_HeatExchangerMapps.AddAsync(item);
            }
            else
            {
                item.edit_date = DateTime.Now;
                _context.HP_HeatExchangerMapps.Update(item);
            }
            await _context.SaveChangesAsync();
        }

        private async Task<decimal> HpAddRemove_HeatExchangeEquipment_CalculateInstHeatPower(HPAddRemoveHP_HeatExchangerMappsDataViewModel model)
        {
            var list = await _context.HPAddRemoveHP_HeatExchangerMappsDataViewModel.FromSqlInterpolated($"exec heat_points.sp_GetHP_HeatExchange {model.data_status},{model.heat_point_id}").ToListAsync();

            decimal sum = 0;

            foreach (var item in list)
                if (item.inst_heat_power != null && item.ht_exch_count != null)
                    sum += (decimal)item.inst_heat_power * (int)item.ht_exch_count;

            return sum;
        }

        private async Task HpAddRemove_HeatExchangeEquipment_SaveCalculateInstHeatPower(decimal value, HPAddRemoveHP_HeatExchangerMappsDataViewModel model)
        {
            bool is_new = false;
            var item = await _context.HP_History.Where(n => n.data_status == model.data_status && n.heat_point_id == model.heat_point_id).FirstOrDefaultAsync();

            if (item == null)
            {
                item = await _context.HP_History.Where(n => n.heat_point_id == model.heat_point_id).OrderByDescending(n => n.data_status).FirstAsync();
                item.data_status = model.data_status;
                is_new = true;
            }

            if (item.inst_heat_power == value)
                return;

            item.inst_heat_power = value;
            item.user_id = userId;

            if (is_new)
            {
                item.create_date = DateTime.Now;
                await _context.HP_History.AddAsync(item);
            }
            else
            {
                item.edit_date = DateTime.Now;
                _context.HP_History.Update(item);
            }

            _context.SaveChanges();
        }

        #endregion

        #region SAVE_DATA PUMP

        // Сохранение значений при добавлении или изменении в Насосное оборудование Состава оборудования
        [ValidateAntiForgeryToken]
        [TypeFilter(typeof(ControllerActionFilterCheckDS))]
        public async Task<IActionResult> HpAddRemove_Pump_Save(HPAddRemoveHP_PumpMappsDataViewModel model)
        {
            try
            {
                await HpAddRemove_Pump_CreateData(model);

                if (model.is_deleted != null && (bool)model.is_deleted)
                    return Json(new { success = true, is_deleted = true });

                return Json(new { success = true, is_deleted = false });
            }
            catch (Exception ex)
            {
                //_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
                return Json(new { success = false });
            }
        }

        private async Task HpAddRemove_Pump_CreateData(HPAddRemoveHP_PumpMappsDataViewModel model)
        {
            bool is_new = false;
            var item = await _context.HP_PumpMapps.FirstOrDefaultAsync
                (n => n.data_status == model.data_status && n.heat_point_id == model.heat_point_id && n.pump_num == model.pump_num);

            if (item == null)
            {
                if (model.pump_num != 0)
                {
                    item = await _context.HP_PumpMapps.FirstOrDefaultAsync
                        (n => n.heat_point_id == model.heat_point_id && n.pump_num == model.pump_num);
                }
                else
                {
                    var value = await _context.HP_PumpMapps.Where
                        (n => n.heat_point_id == model.heat_point_id).MaxAsync(n => (short?)n.pump_num) ?? 0;

                    item = new HP_PumpMapps { pump_num = ++value };
                }
                is_new = true;
            }
            else
                item.pump_num = model.pump_num;

            item.type_pump_id = model.type_pump_id;
            item.equip_id = model.equip_id;
            item.pump_count = model.pump_count;
            item.pump_capacity = model.pump_capacity;
            item.pump_press = model.pump_press;
            item.heat_point_id = model.heat_point_id;
            item.data_status = model.data_status;
            item.user_id = userId;
            item.is_deleted = model.is_deleted ?? false;

            await HpAddRemove_Pump_SaveDb(item, is_new);
        }

        private async Task HpAddRemove_Pump_SaveDb(HP_PumpMapps item, bool is_new)
        {
            if (is_new)
            {
                item.create_date = DateTime.Now;
                await _context.HP_PumpMapps.AddAsync(item);
            }
            else
            {
                item.edit_date = DateTime.Now;
                _context.HP_PumpMapps.Update(item);
            }
            await _context.SaveChangesAsync();
        }

        #endregion

        #region SAVE_DATA AUTOMATIZATION

        // Сохранение значений при добавлении или изменении в Состав оборудования
        [ValidateAntiForgeryToken]
        [TypeFilter(typeof(ControllerActionFilterCheckDS))]
        public async Task<IActionResult> HpAddRemove_Automatization_Save(HPAddRemove_AutomatizationViewModel model)
        {
            try
            {
                await HpAddRemove_Automatization_CreateData(model);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                //_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
                return Json(new { success = false });
            }
        }

        private async Task HpAddRemove_Automatization_CreateData(HPAddRemove_AutomatizationViewModel model)
        {
            bool is_new = false;
            var item = await _context.HP_Automatization.FirstOrDefaultAsync(n => n.data_status == model.data_status && n.heat_point_id == model.heat_point_id);

            if (item == null)
            {
                item = new HP_Automatization();
                is_new = true;
            }

            if (model.avail_automatic != null && (bool)model.avail_automatic)
                item.automatic_type_id = model.automatic_type_id;
            else
                item.automatic_type_id = null;

            item.avail_automatic = model.avail_automatic;
            item.avail_flow_limiter = model.avail_flow_limiter;
            item.avail_automatic_heat_control = model.avail_automatic_heat_control;
            item.heat_point_id = model.heat_point_id;
            item.data_status = model.data_status;
            item.user_id = userId;

            await HpAddRemove_Automatization_SaveDb(item, is_new);
        }

        private async Task HpAddRemove_Automatization_SaveDb(HP_Automatization item, bool is_new)
        {
            if (is_new)
            {
                item.create_date = DateTime.Now;
                await _context.HP_Automatization.AddAsync(item);
            }
            else
            {
                item.edit_date = DateTime.Now;
                _context.HP_Automatization.Update(item);
            }

            await _context.SaveChangesAsync();
        }

        #endregion

        #region SAVE_DATA NUM SIGN OTHER DATA BASE

        // Сохранение значений при добавлении или изменении в Номера и обозначения ТП в других базах данных 
        [ValidateAntiForgeryToken]
        [TypeFilter(typeof(ControllerActionFilterCheckDS))]
        public async Task<IActionResult> Consumer_PerspectiveDev_YearDynamic_Save(int data_status, int consumer_id, int main_dev_prog_id)
        {
            try
            {
                //var model = models.FirstOrDefault(n => n.is_main_prog == true);
                await YearDynamic_SaveData(data_status, consumer_id, main_dev_prog_id);
                return Json(new { success = true, main_dev_prog_id = main_dev_prog_id });
            }
            catch (Exception ex)
            {
                //_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
                return Json(new { success = false });
            }
        }

        private async Task YearDynamic_SaveData(int data_status, int consumer_id, int main_dev_prog_id)
        {
            var key_value = await GetConsumerHistoryAsync(data_status, consumer_id);
            var item = key_value.Value;
            item.main_dev_prog_id = main_dev_prog_id;
            await Consumer_NumSignOtherDb_SaveDb(item, false);
        }

        #endregion

        #region SAVE_DATA ACC RESOURCES

        // Сохранение значений при добавлении или изменении в Учет ресурсов
        [ValidateAntiForgeryToken]
        [TypeFilter(typeof(ControllerActionFilterCheckDS))]
        public async Task<IActionResult> HpAddRemove_AccResources_Save(HPAddRemove_AccResourcesViewModel model)
        {
            try
            {
                await HpAddRemove_AccResources_CreateData(model);

                if (model.is_deleted != null && (bool)model.is_deleted)
                    return Json(new { success = true, is_deleted = true });

                return Json(new { success = true, is_deleted = false });
            }
            catch (Exception ex)
            {
                //_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
                return Json(new { success = false });
            }
        }

        private async Task HpAddRemove_AccResources_CreateData(HPAddRemove_AccResourcesViewModel model)
        {
            bool is_new = false;
            var item = await _context.HP_AccResources.FirstOrDefaultAsync
                (n => n.data_status == model.data_status && n.heat_point_id == model.heat_point_id && n.device_num == model.device_num);

            if (item == null)
            {
                if (model.device_num != 0)
                {
                    item = await _context.HP_AccResources.FirstOrDefaultAsync
                        (n => n.heat_point_id == model.heat_point_id && n.device_num == model.device_num);
                }
                else
                {
                    var value = await _context.HP_AccResources.Where
                        (n => n.heat_point_id == model.heat_point_id).MaxAsync(n => (int?)n.device_num) ?? 0;

                    item = new HP_AccResources { device_num = ++value };
                }
                is_new = true;
            }
            else
                item.device_num = model.device_num;

            item.acc_resource_type_id = model.acc_resource_type_id;
            item.meter_device_mark = model.meter_device_mark;
            item.count_devices = model.count_devices;
            item.heat_point_id = model.heat_point_id;
            item.data_status = model.data_status;
            item.user_id = userId;
            item.is_deleted = model.is_deleted ?? false;

            await HpAddRemove_AccResources_SaveDb(item, is_new);
        }

        private async Task HpAddRemove_AccResources_SaveDb(HP_AccResources item, bool is_new)
        {
            if (is_new)
            {
                item.create_date = DateTime.Now;
                await _context.HP_AccResources.AddAsync(item);
            }
            else
            {
                item.edit_date = DateTime.Now;
                _context.HP_AccResources.Update(item);
            }
            await _context.SaveChangesAsync();
        }

        #endregion

        #region SAVE_DATA NUM SIGN OTHER DATA BASE

        // Сохранение значений при добавлении или изменении в Номера и обозначения ТП в других базах данных 
        [ValidateAntiForgeryToken]
        [TypeFilter(typeof(ControllerActionFilterCheckDS))]
        public async Task<IActionResult> Consumer_NumSignOtherDb_Save(Consumers_NumSignOtherDBViewModel model)
        {
            try
            {
                await Consumer_NumSignOtherDb_CreateData(model);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                //_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
                return Json(new { success = false });
            }
        }

        private async Task Consumer_NumSignOtherDb_CreateData(Consumers_NumSignOtherDBViewModel model)
        {
            var key_value = await GetConsumerHistoryAsync(model.data_status, model.consumer_id);
            var is_new = key_value.Key;
            var item = key_value.Value;

            item.muid = model.muid;
            item.con_code = model.con_code;
            item.kod_dp_m = model.kod_dp_m;
            item.kks_m = model.kks_m;
            item.data_status = model.data_status;
            item.user_id = userId;

            await Consumer_NumSignOtherDb_SaveDb(item, is_new);
        }

        private async Task<KeyValuePair<bool, ConsumersHistory>> GetConsumerHistoryAsync(int data_status, int consumer_id)
        {
            var is_new = false;
            var item = await _context.ConsumersHistory.FirstOrDefaultAsync
                (n => n.data_status == data_status && n.consumer_id == consumer_id && n.is_deleted != true);

            if (item == null)
            {
                item = await _context.ConsumersHistory.Where
                        (n => n.consumer_id == consumer_id).OrderByDescending(n => n.data_status).FirstAsync();

                if (item == null)
                    item = new ConsumersHistory();
                is_new = true;
            }

            return new KeyValuePair<bool, ConsumersHistory> (is_new, item);
        }

        private async Task Consumer_NumSignOtherDb_SaveDb(ConsumersHistory item, bool is_new)
        {
            if (is_new)
            {
                item.create_date = DateTime.Now;
                await _context.ConsumersHistory.AddAsync(item);
            }
            else
            {
                item.edit_date = DateTime.Now;
                _context.ConsumersHistory.Update(item);
            }
            await _context.SaveChangesAsync();
        }

        #endregion

        #region SAVE_DATA DOCS FOOTAGE

        // Сохранение документации и фотоматериалов по ТП
        [ValidateAntiForgeryToken]
        [TypeFilter(typeof(ControllerActionFilterCheckDS))]
        public async Task<IActionResult> HpAddRemove_DocsFootage_Save(HPAddRemove_DocsFootageViewModel model)
        {
            try
            {
                await HpAddRemove_DocsFootage_CreateData(model);

                if (model.is_deleted != null && (bool)model.is_deleted)
                    return Json(new { success = true, is_deleted = true });

                return Json(new { success = true, is_deleted = false });
            }
            catch (Exception ex)
            {
                //_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
                return Json(new { success = false });
            }
        }

        private async Task HpAddRemove_DocsFootage_CreateData(HPAddRemove_DocsFootageViewModel model)
        {
            bool is_new = false;
            bool is_deleting_file = false;
            var item = await _context.HP_DocsPhotos_History.FirstOrDefaultAsync
                (n => n.data_status == model.data_status && n.heat_point_id == model.heat_point_id && n.doc_num == model.doc_num);

            if (item == null)
            {
                if (model.doc_num != 0)
                {
                    item = await _context.HP_DocsPhotos_History.FirstOrDefaultAsync
                        (n => n.heat_point_id == model.heat_point_id && n.doc_num == model.doc_num);
                }
                else
                {
                    var value = await _context.HP_DocsPhotos_History.Where
                        (n => n.heat_point_id == model.heat_point_id).MaxAsync(n => (int?)n.doc_num) ?? 0;

                    item = new HP_DocsPhotos_History { doc_num = ++value };
                }
                is_new = true;
            }
            else
            {
                is_deleting_file = true;
                item.doc_num = model.doc_num;
            }

            item.doc_type_id = model.doc_type_id;
            item.doc_description = model.doc_description;
            item.heat_point_id = model.heat_point_id;
            item.data_status = model.data_status;
            item.user_id = userId;
            item.is_deleted = model.is_deleted ?? false;

            if ((bool)!item.is_deleted)
                HpAddRemove_DocsFootage_SaveFile(item, is_deleting_file);

            await HpAddRemove_DocsFootage_SaveDb(item, is_new);
        }

        private async Task HpAddRemove_DocsFootage_SaveDb(HP_DocsPhotos_History item, bool is_new)
        {
            if (is_new)
            {
                item.create_date = DateTime.Now;
                await _context.HP_DocsPhotos_History.AddAsync(item);
            }
            else
            {
                item.edit_date = DateTime.Now;
                _context.HP_DocsPhotos_History.Update(item);
            }
            await _context.SaveChangesAsync();
        }

        private void HpAddRemove_DocsFootage_SaveFile(HP_DocsPhotos_History item, bool isDelFile)
        {
            var file = HttpContext.Request.Form.Files.First();
            string webRootPath = _hostingEnvironment.WebRootPath;
            string upload = webRootPath + "\\Docs\\HeatPointDocs\\";

            if (file == null)
                return;

            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string extension = Path.GetExtension(file.FileName);
            string url = upload + fileName + extension;
            var oldUrl = Path.Combine(upload, item.doc_url ?? "");

            if (isDelFile && System.IO.File.Exists(oldUrl))
                System.IO.File.Move(oldUrl, oldUrl + "_deleted");

            string date = DateTime.Now.ToString("yyyy/MM/dd/hh/mm/ss");
            item.doc_name = fileName + extension;
            fileName = fileName + "_" + item.data_status + "_" + item.heat_point_id + "_" + item.doc_num + "_" + date + extension;

            using (var fs = new FileStream(Path.Combine(upload, fileName), FileMode.Create))
                file.CopyTo(fs);

            item.doc_url = fileName;
        }

        #endregion
    }
}
