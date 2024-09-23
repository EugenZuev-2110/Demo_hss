using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Areas.HPConsumers.Models;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Filters;

namespace WebProject.Areas.DictionaryTables.Controllers
{
    [TypeFilter(typeof(ControllerActionFilter))]
    [Area("DictionaryTables")]
    public class PerspectiveDevelopmentTownController : ModuleBaseController
    {
        public PerspectiveDevelopmentTownController(ILogger<TariffZoneController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
            : base(logger, context, context2, httpContextAccessor, hostingEnvironment, m_c) { }

        public IActionResult PerspectiveDevelopmentList()
        {
            return View();
        }

        #region OpenPopups

        public async Task<IActionResult> OpenPerspectiveDevelopment(int dev_prog_id)
        {
            var _hpHeaderAddRemoveDataViewModel = (await _context.PerspectiveDevelopmentHeaderViewModel.FromSqlInterpolated($"exec consumers.sp_GetPerspectiveDevelopmentHeaderDataOne {dev_prog_id}")
                            .ToListAsync()).FirstOrDefault() ?? new PerspectiveDevelopmentHeaderViewModel() { dev_prog_id = dev_prog_id };

            try
            {
                ViewBag.PerspectiveDevelopmentList = await _context.fnt_GetUnomPerspectiveDevelopmentList().ToListAsync();
            }
            catch (Exception ex)
            {
                //_m_c.ExLog_Save("OpenTariffConnection", $"id={id},data_status={data_status},perspective_year={perspective_year}", ex.Message, userId);
            }
            return PartialView(_hpHeaderAddRemoveDataViewModel);
        }

        public async Task<IActionResult> OpenPerspectiveDevelopment_Facilities(int consumer_id, int dev_prog_id)
        {
            var _hpHeaderAddRemoveDataViewModel = (await _context.PerspectiveDevelopment_FacilitiesHeaderViewModel.FromSqlInterpolated($"exec consumers.sp_GetPerspectiveDevelopmentFacilityHeaderDataOne {consumer_id},{dev_prog_id}")
                            .ToListAsync()).FirstOrDefault() ?? new PerspectiveDevelopment_FacilitiesHeaderViewModel() { dev_prog_id = dev_prog_id };

            try
            {
                ViewBag.PerspectiveDevelopmentFacilityList = await _context.fnt_GetFacilityNumbersPerspectiveDevelopmentList(dev_prog_id).ToListAsync();
            }
            catch (Exception ex)
            {
                //_m_c.ExLog_Save("OpenTariffConnection", $"id={id},data_status={data_status},perspective_year={perspective_year}", ex.Message, userId);
            }
            return PartialView(_hpHeaderAddRemoveDataViewModel);
        }

        #endregion

        #region ViewComponents
        public ActionResult OnGetPerspectiveDevelopment_PartialViewComponent()
        {
            return ViewComponent("PerspectiveDevelopmentList_Partial");
        }

        public ActionResult OnGetPerspectiveDevelopmentFacility_PartialViewComponent(int dev_prog_id)
        {
            return ViewComponent("PerspectiveDevelopment_Facilities_Partial", dev_prog_id);
        }

        #endregion

        #region GET DATABASE DATA

        public async Task<IActionResult> GetBildingUnomConsumerId(int consumer_id)
        {
            var consumer_unom = await _context.fnt_GetBildingUnomConsumerIdOne(_m_c.GetCurrentDS(), consumer_id).FirstOrDefaultAsync();
            return Json(new { consumer_unom = consumer_unom });
        }

        public async Task<IActionResult> GetConsumerNumberAddressList(int building_id)
        {
            var consumer_unom_list = await _context.fnt_GetUnomConsumerAddressListByCharsForObjDevProg("", 0, building_id, 0, _m_c.GetCurrentDS()).ToListAsync();
            return Json(new { consumer_unom_list = consumer_unom_list });
        }

        #endregion

        #region SAVE_DATA ADD REMOVE GEN INFO

        // Сохранение значений при добавлении или удалении общих сведений ТП
        [ValidateAntiForgeryToken]
        [TypeFilter(typeof(ControllerActionFilterCheckDS))]
        public async Task<IActionResult> DevProgramm_GenInfoData_Save(PerspectiveDevelopment_GenInfoViewModel model)
        {
            try
            {
                var dev_prog = await SetAllParameters_AddRemove_GenInfoData(model);

                if (model.is_deleted != null && (bool)model.is_deleted)
                    return Json(new { success = true, is_deleted = true, model.is_new_dev_prog });

                //var address = await _context.fnt_GetDistrictUnomAddress((int)cons_id, model.data_status).FirstOrDefaultAsync();

                var per_dev_name = await _context.fnt_GetUnomPerspectiveDevelopmentList().FirstOrDefaultAsync(n => n.value_id == dev_prog.dev_prog_id);

                return Json(new { success = true, is_deleted = false, model.is_new_dev_prog, dev_prog_id = dev_prog.dev_prog_id, per_dev_name = per_dev_name });
            }
            catch (Exception ex)
            {
                //_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
                return Json(new { success = false });
            }
        }

        private async Task<DevProgramms?> SetAllParameters_AddRemove_GenInfoData(PerspectiveDevelopment_GenInfoViewModel model)
        {
            var dev_prog = await _context.DevProgramms.Where(n => n.dev_prog_id == model.dev_prog_id).FirstOrDefaultAsync();

            if (dev_prog == null)
            {
                dev_prog = await CreateDevProgramm(model);
                await _context.DevProgramms.AddAsync(dev_prog);
            }
            else
            {
                await SetExistModel_GenInfoData_DevProgramms(dev_prog, model);
                _context.DevProgramms.Update(dev_prog);
            }

            await _context.SaveChangesAsync();
            return dev_prog;
        }

        private async Task SetExistModel_GenInfoData_DevProgramms(DevProgramms dev_prog, PerspectiveDevelopment_GenInfoViewModel model, bool isNewData = false)
        {
            if (dev_prog.type_dev_prog_id != model.type_dev_prog_id)
            {
                dev_prog.unom_num = await CreateDevProgrammUnom(model);
                dev_prog.type_dev_prog_id = model.type_dev_prog_id;
            }

            dev_prog.prog_name = model.prog_name;
            dev_prog.date_dev_prog = model.date_dev_prog;
            dev_prog.prog_doc_num = model.prog_doc_num;

            if (dev_prog.year_dev_prog != model.year_dev_prog)
            {
                dev_prog.unom_num = UpdateDevProgrammUnom(dev_prog.unom_num, model.year_dev_prog);
                dev_prog.year_dev_prog = model.year_dev_prog;
            }

            dev_prog.layer_id = model.layer_id;
            dev_prog.layer_sys = model.layer_sys;

            if (!isNewData)
                dev_prog.edit_date = DateTime.Now;
            dev_prog.user_id = userId;
            dev_prog.is_deleted = model.is_deleted ?? false;
        }

        private async Task<DevProgramms> CreateDevProgramm(PerspectiveDevelopment_GenInfoViewModel model)
        {
            var new_dev_program = new DevProgramms { is_deleted = false, create_date = DateTime.Now };
            await SetExistModel_GenInfoData_DevProgramms(new_dev_program, model, true);
            model.is_new_dev_prog = true;
            return new_dev_program;
        }

        private async Task<string> CreateDevProgrammUnom(PerspectiveDevelopment_GenInfoViewModel model)
        {
            var dev_prog_list = await _context.DevProgramms.Where(n => n.type_dev_prog_id == model.type_dev_prog_id).ToListAsync();
            var type = await _context.Dict_DevProgTypes.FirstOrDefaultAsync(n => n.Id == model.type_dev_prog_id);
            int num = 0;
            int year = 0;

            await Task.Run(() =>
            {
                foreach (var prog in dev_prog_list)
                {
                    var unom_num = Convert.ToInt32(prog.unom_num.Substring(type.type_prog_short.Length, 5));
                    if (unom_num > num)
                        num = unom_num;
                }
            });

            return $"{type.type_prog_short}{string.Format("{0:d5}", ++num)}.{model.year_dev_prog}";
        }

        private string UpdateDevProgrammUnom(string old_unom, int? year)
        {
            return old_unom.Remove(old_unom.Length - 4) + year;
        }

        #endregion

        #region SAVE_DATA ADD REMOVE FACILITY GEN INFO

        // Сохранение значений при добавлении или удалении общих сведений Объектов в программе развития города (поселения)
        [ValidateAntiForgeryToken]
        [TypeFilter(typeof(ControllerActionFilterCheckDS))]
        public async Task<IActionResult> DevProgramm_Facility_GenInfoData_Save(PerspectiveDevelopmentFacility_GenInfoViewModel model)
        {
            try
            {
                var is_address_exist = await PreSetDataAsync(model);
                if (is_address_exist == true)
                    return Json(new { success = false, is_address_exist = true });
                return await GetJsonResultAsync(model);
            }
            catch (Exception ex)
            {
                //_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
                return Json(new { success = false });
            }
        }

        private async Task<bool?> PreSetDataAsync(PerspectiveDevelopmentFacility_GenInfoViewModel model)
        {
            if (model.consumer_id == null && model.building_id == null)
            {
                var exist_address = await _context.buildings_Histories.FirstOrDefaultAsync(n => n.address == model.address);
                if (exist_address != null)
                    return true;

                await CreateBuilding(model);
                await CreateBuildingHistory(model);
                await CreateConsumer_GenInfoDataFacility(model);
                await CreateConsumerHistory(model);
                return false;
            }
            else if (model.consumer_id == null && model.building_id != null)
            {
                await CreateConsumer_GenInfoDataFacility(model);
                await CreateConsumerHistory(model);
            }

            return null;
        }

        private async Task<JsonResult> GetJsonResultAsync(PerspectiveDevelopmentFacility_GenInfoViewModel model)
        {
            var dev_prog = await SetAllParameters_AddRemove_Facility_GenInfoData(model);
            var data_status = _m_c.GetCurrentDS();
            var consumer_name = (await _context.fnt_GetUnomConsumerAddressListByCharsForObjDevProg("", model.consumer_id ?? 0, model.building_id ?? 0, model.dev_prog_id ?? 0, data_status)
                .FirstOrDefaultAsync(n => n.value_id == model.consumer_id))!.value_name;

            var building_name = (await _context.fnt_GetBildingUnomList(data_status).FirstOrDefaultAsync(n => n.value_id == (model.building_id ?? 0)));
            var unom_facility = (await _context.fnt_GetFacilityNumbersPerspectiveDevelopmentList(model.dev_prog_id ?? 0).FirstOrDefaultAsync(n => n.value_id == (model.consumer_id ?? 0)))!.value_name;

            return Json(new { success = true, is_deleted = false, is_new = false, model = model, consumer_name = consumer_name, building_name = building_name != null ? building_name.value_name : "", unom_facility = unom_facility });
        }

        private async Task<DevProgrammsConsumers?> SetAllParameters_AddRemove_Facility_GenInfoData(PerspectiveDevelopmentFacility_GenInfoViewModel model)
        {
            DevProgrammsConsumers? dev_prog;

            if (model.consumer_id_old == null)
            {
                dev_prog = CreateDevProgrammFacility(model);
                await _context.DevProgrammsConsumers.AddAsync(dev_prog);
            }
            else
            {
                dev_prog = await _context.DevProgrammsConsumers.Where(n => n.dev_prog_id == model.dev_prog_id && n.consumer_id == model.consumer_id_old).FirstOrDefaultAsync();

                if (model.consumer_id_old != model.consumer_id)
                {
                    _context.DevProgrammsConsumers.Remove(dev_prog);
                    dev_prog = CreateDevProgrammFacility(model);
                    await _context.DevProgrammsConsumers.AddAsync(dev_prog);
                    model.consumer_id_old = model.consumer_id;
                }
                else
                {
                    SetExistModel_GenInfoData_DevProgrammsFacility(dev_prog, model, false);
                    _context.DevProgrammsConsumers.Update(dev_prog);
                }
            }

            await _context.SaveChangesAsync();
            return dev_prog;
        }

        private async Task CreateConsumer_GenInfoDataFacility(PerspectiveDevelopmentFacility_GenInfoViewModel model)
        {
            var cons = await CreateConsumer(model);
            cons.building_id = model.building_id;
            cons.edit_date = DateTime.Now;
            cons.user_id = userId;
            _context.Consumers.Update(cons);
        }

        private void SetExistModel_GenInfoData_DevProgrammsFacility(DevProgrammsConsumers dev_prog, PerspectiveDevelopmentFacility_GenInfoViewModel model, bool isNewData = false)
        {
            dev_prog.obj_num = model.obj_num;
            dev_prog.address = model.address;
            dev_prog.district_id = model.district_id;
            dev_prog.obj_name = model.obj_name;
            dev_prog.appl_name = model.appl_name;
            dev_prog.cad_num_zu = model.cad_num_zu;
            dev_prog.bti_build_unom = model.bti_build_unom;
            dev_prog.connect_req_date = model.connect_req_date;
            dev_prog.connect_req_num = model.connect_req_num;
            dev_prog.connect_cond_date = model.connect_cond_date;
            dev_prog.connect_cond_num = model.connect_cond_num;
            dev_prog.connect_agree_date = model.connect_agree_date;
            dev_prog.connect_agree_num = model.connect_agree_num;
            dev_prog.connect_point_hn = model.connect_point_hn;

            if (!isNewData)
                dev_prog.edit_date = DateTime.Now;
            dev_prog.user_id = userId;
            dev_prog.is_deleted = model.is_deleted ?? false;
        }

        private DevProgrammsConsumers CreateDevProgrammFacility(PerspectiveDevelopmentFacility_GenInfoViewModel model)
        {
            var new_dev_program = new DevProgrammsConsumers { is_deleted = false, create_date = DateTime.Now, consumer_id = model.consumer_id ?? 0, dev_prog_id = model.dev_prog_id ?? 0 };
            SetExistModel_GenInfoData_DevProgrammsFacility(new_dev_program, model, true);
            model.is_new_facility = true;
            return new_dev_program;
        }

        private async Task<Consumers> CreateConsumer(PerspectiveDevelopmentFacility_GenInfoViewModel model)
        {
            var cons = new Consumers
            {
                create_date = DateTime.Now,
                building_id = model.building_id,
                user_id = userId
            };

            await _context.Consumers.AddAsync(cons);
            await _context.SaveChangesAsync();
            model.consumer_id = cons.consumer_id;
            return cons;
        }

        private async Task CreateConsumerHistory(PerspectiveDevelopmentFacility_GenInfoViewModel model)
        {
            var cons_hist = new ConsumersHistory
            {
                create_date = DateTime.Now,
                data_status = _m_c.GetCurrentDS(),
                consumer_id = model.consumer_id ?? 0,
                user_id = userId,
                is_deleted = false
            };

            await _context.ConsumersHistory.AddAsync(cons_hist);
        }

        private async Task CreateBuilding(PerspectiveDevelopmentFacility_GenInfoViewModel model)
        {
            var building = new Buildings
            {
                create_date = DateTime.Now,
                district_id = model.district_id,
                bti_build_unom = model.bti_build_unom,
                kadnum_zu = model.cad_num_zu,
                user_id = userId
            };

            await _context.Buildings.AddAsync(building);
            await _context.SaveChangesAsync();
            model.building_id = building.building_id;
        }

        private async Task CreateBuildingHistory(PerspectiveDevelopmentFacility_GenInfoViewModel model)
        {
            var building_history = new Buildings_History
            {
                data_status = _m_c.GetCurrentDS(),
                building_id = model.building_id ?? 0,
                address = model.address,
                create_date = DateTime.Now,
                user_id = userId,
                is_deleted = false
            };

            await _context.buildings_Histories.AddAsync(building_history);
        }

        #endregion

        #region SAVE_DATA ADD REMOVE DESTINATION AND RELATION'S CATEGORY

        // Сохранение значений при добавлении или удалении назначения и категории надежности
        [ValidateAntiForgeryToken]
        [TypeFilter(typeof(ControllerActionFilterCheckDS))]
        public async Task<IActionResult> ConsumerAddRemove_DestRelCatHSData_Save(PerspectiveDevelopment_Facilities_DestRelCatHSViewModel model)
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

        private async Task SetAllParameters_AddRemove_DestRelCatHSData(PerspectiveDevelopment_Facilities_DestRelCatHSViewModel model)
        {
            var dev_prog = await _context.DevProgrammsConsumers.Where(n => n.dev_prog_id == model.dev_prog_id && n.consumer_id == model.consumer_id).FirstOrDefaultAsync();

            try
            {
                SetExistModel_DestRelCatHSData_DevProgrammsConsumers(dev_prog, model);
                _context.DevProgrammsConsumers.Update(dev_prog);
            }
            catch (Exception ex) { }
        }

        private void SetExistModel_DestRelCatHSData_DevProgrammsConsumers(DevProgrammsConsumers dev_prog_cons, PerspectiveDevelopment_Facilities_DestRelCatHSViewModel model, bool isNewData = false)
        {
            dev_prog_cons.type_purpose_id = model.type_purpose_id;
            dev_prog_cons.category_rel_id = model.category_rel_id;
            dev_prog_cons.type_prod_supply_id = model.type_prod_supply_id;
            dev_prog_cons.floors_id = model.floor_id;
            dev_prog_cons.edit_date = DateTime.Now;
            dev_prog_cons.user_id = userId;
            dev_prog_cons.is_deleted = model.is_deleted ?? false;
        }

        #endregion
    }
}
