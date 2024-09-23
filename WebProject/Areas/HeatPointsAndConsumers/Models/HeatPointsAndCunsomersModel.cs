using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Areas.HeatPointsAndConsumers.Models
{
	/// <summary>
	/// Перечень переключаемых теплопунктов
	/// </summary>
	[Keyless]
	public class HP_SwitchableUnit
	{
		public int? hp_switchable_data_status{ get; set; }

		/// <summary>
		/// УНОМ ТП
		/// </summary>
		public int? hp_switchable_unom_id { get; set; }

		/// <summary>
		/// Наименование
		/// </summary>
		public string? hp_switchable_name { get; set; }

		/// <summary>
		/// Суммарная нагрузка до переключения
		/// </summary>
		public decimal? hp_switchable_summary_before { get; set; }

		/// <summary>
		/// Суммарная нагрузка после переключения
		/// </summary>
		public decimal? hp_switchable_summary_after { get; set; }

		/// <summary>
		/// Тип
		/// </summary>
		public string? hp_switchable_type { get; set; }
	}

	/// <summary>
	/// Наименование теплопункта
	/// </summary>
	[Keyless]
	public class HP_NameListUnit
	{
		/// <summary>
		/// Год
		/// </summary>
		public int? hp_names_list_year{ get; set; }

		/// <summary>
		/// УНОМ ИСТ до переключения
		/// </summary>
		public int? hp_names_list_unom_source_before { get; set; }

		/// <summary>
		/// Наименование до переключения
		/// </summary>
		public string? hp_names_list_owner_name_before { get; set; }

		/// <summary>
		/// Вывод до переключения
		/// </summary>
		public string? hp_names_list_output_before { get; set; }

		/// <summary>
		/// Суммарная нагрузка до переключения	
		/// </summary>
		public decimal? hp_names_list_summary_before { get; set; }

		/// <summary>
		/// УНОМ ИСТ до переключения
		/// </summary>
		public int? hp_names_list_unom_source_after { get; set; }

		/// <summary>
		/// Наименование после переключения
		/// </summary>
		public string? hp_names_list_owner_name_after { get; set; }

		/// <summary>
		/// Вывод после переключения
		/// </summary>
		public string? hp_names_list_output_after { get; set; }

		/// <summary>
		/// Суммарная нагрузка после переключения	
		/// </summary>
		public decimal? hp_names_list_summary_after { get; set; }
	}

	/// <summary>
	/// Добавление / удаление теплопункта
	/// </summary>
	[Keyless]
	public class HP_MainListUnit
	{
		/// <summary>
		/// Источник тепловой энергии. УНОМ ИСТ id (1)
		/// </summary>
		public int? source_unom { get; set; }

		/// <summary>
		/// Источник тепловой энергии. Наименование (2)
		/// </summary>
		public string? source_name { get; set; }

		/// <summary>
		/// Источник тепловой энергии. Статус источника (3)
		/// </summary>
		public string? source_status_name { get; set; }

		/// <summary>
		/// Источник тепловой энергии. УНОМ Вывода (4)
		/// </summary>
		public string? source_output_unom { get; set; }

		/// <summary>
		/// Источник тепловой энергии. Наименование вывода (5)
		/// </summary>
		public string? source_output_name { get; set; }

		/// <summary>
		/// Тепловой пункт. УНОМ ТП (6)
		/// </summary>
		public int? heat_point_id { get; set; }

		/// <summary>
		/// Тепловой пункт. Номер теплового пункта (7)	
		/// </summary>
		public string? hp_num { get; set; }

		/// <summary>
		/// Тепловой пункт. Административный округ (8)
		/// </summary>
		public string? hp_region_name { get; set; }

		/// <summary>
		/// Тепловой пункт. Район (9)
		/// </summary>
		public string? hp_district_name { get; set; }

		/// <summary>
		/// Тепловой пункт. Адрес месторасположения теплового пункта (10)
		/// </summary>
		public string? hp_address { get; set; }

		/// <summary>
		/// Тепловой пункт. Тип теплового пункта (11)	
		/// </summary>
		public string? hp_type_name { get; set; }

		/// <summary>
		/// Тепловой пункт. Номер типовой схемы теплового пункта id (12)
		/// </summary>
		public short? hp_type_scheme_id { get; set; }

		/// <summary>
		/// Тепловой пункт. Тип размещения теплового пункта (13)
		/// </summary>
		public string? hp_type_location_name { get; set; }

		/// <summary>
		/// Тепловой пункт. Статус теплового пункта (14)
		/// </summary>
		public string? hp_status_name { get; set; }

		/// <summary>
		/// Тепловой пункт. Год ввода в эксплуатацию (15)
		/// </summary>
		public int? hp_start_year_expl { get; set; }

		/// <summary>
		/// Тепловой пункт. Год проведения последней реконструкции (16)
		/// </summary>
		public int? hp_last_year_reconstr { get; set; }

		/// <summary>
		/// Тепловой пункт. Год ликвидации теплового пункта (17)
		/// </summary>
		public int? hp_year_liquidate { get; set; }

		/// <summary>
		/// Температурный график. Проектный на тепловом вводе Т1/Т2, гр.С (18)
		/// </summary>
		public string? hp_tg_ht_name { get; set; }

		/// <summary>
		/// Температурный график. Проектный в системе отопления Т3/Т4, гр.С (19)
		/// </summary>
		public string? hp_tg_heat_name { get; set; }

		/// <summary>
		/// Температурный график. Проектный в системе вентиляции Т3v/Т4v, гр.С (20)
		/// </summary>
		public string? hp_tg_vent_name { get; set; }

		/// <summary>
		/// Температурный график. Проектный в системе ГВС Т5/Т6, гр.С (21)
		/// </summary>
		public string? hp_tg_gvs_name { get; set; }

		/// <summary>
		/// Температурный график. Проектный в системе технологии Тt1/Тt2, гр.С (22)
		/// </summary>
		public string? hp_tg_tech_id_name { get; set; }

		/// <summary>
		/// Зона деятельности ЕТО / тарифная зона (ТЗ) / теплоснабжающая организация ТСО). КОД ЕТО (23)
		/// </summary>
		public string? hp_unom_eto { get; set; }

		/// <summary>
		/// Зона деятельности ЕТО / тарифная зона (ТЗ) / теплоснабжающая организация ТСО). Наименование ЕТО (24)
		/// </summary>
		public string? hp_eto_short_name { get; set; }

		/// <summary>
		/// Зона деятельности ЕТО / тарифная зона (ТЗ) / теплоснабжающая организация ТСО). УНОМ ТЗ (25)
		/// </summary>
		public string? hp_unom_tz { get; set; }

		/// <summary>
		/// Зона деятельности ЕТО / тарифная зона (ТЗ) / теплоснабжающая организация ТСО). КОД ТСО (26)
		/// </summary>
		public string? hp_code_tso { get; set; }

		/// <summary>
		/// Зона деятельности ЕТО / тарифная зона (ТЗ) / теплоснабжающая организация ТСО). Наименование ТСО (27)
		/// </summary>
		public string? hp_tso_short_name { get; set; }

		/// <summary>
		/// Организация собственник. Организация собственник (28)
		/// </summary>
		public string? hp_org_owner_name { get; set; }

		/// <summary>
		/// Организация собственник. ИНН организации собственника (29)
		/// </summary>
		public string? hp_org_owner_inn { get; set; }

		/// <summary>
		/// Организация собственник. Право владения объектом (30)
		/// </summary>
		public string? hp_obj_ownership_name { get; set; }

		/// <summary>
		/// Условные номера. Кадастровый номер земельного участка (КН ЗУ) (31)
		/// </summary>
		public string? hp_kad_num_zu { get; set; }

		/// <summary>
		/// Условные номера. Кадастровый номер объекта капитального строительства (КН ОКС) (32)
		/// </summary>
		public string? hp_kad_num_oks { get; set; }

		/// <summary>
		/// Условные номера. ФИАС строения/здания теплового пункта (33)
		/// </summary>
		public int? hp_fias_build { get; set; }

		/// <summary>
		/// Условные номера. ФИАС земельного участка (34)
		/// </summary>
		public int? hp_fias_zu { get; set; }

		/// <summary>
		/// Условные номера. MUID (35)
		/// </summary>
		public string? hp_muid { get; set; }

		/// <summary>
		/// Условные номера. LOTUS ID (36)
		/// </summary>
		public string? hp_lotus_id { get; set; }

		/// <summary>
		/// Условные номера. KASU_COMPOSITE_ID (37)
		/// </summary>
		public string? hp_kasu_composite_id { get; set; }

		/// <summary>
		/// Условные номера. KASU_ID (38)
		/// </summary>
		public string? hp_kasu_id { get; set; }

		/// <summary>
		/// Условные номера. KASU_MAP_ID (39)
		/// </summary>
		public string? hp_kasu_map_id { get; set; }

		/// <summary>
		/// Объект относится к бесхозяйному имуществу (40)
		/// </summary>
		public string? obj_vacant_prop { get; set; }

		/// <summary>
		/// Объект относится к ветхому имуществу (41)
		/// </summary>
		public string? obj_old_prop { get; set; }

		/// <summary>
		/// Установленная тепловая мощность теплового пункта, Гкал/ч (42)
		/// </summary>
		public decimal? hp_inst_heat_power { get; set; }

		/// <summary>
		/// Тепловая нагрузка по договорам теплоснабжения. В горячей воде. Технология, Гкал/ч (43)
		/// </summary>
		public decimal? ctr_hl_tech_hw { get; set; }

		/// <summary>
		/// Тепловая нагрузка по договорам теплоснабжения. В горячей воде. Отопление, Гкал/ч (44)
		/// </summary>
		public decimal? ctr_hl_heat_hw { get; set; }

		/// <summary>
		/// Тепловая нагрузка по договорам теплоснабжения. В горячей воде. Вентиляция, Гкал/ч (45)
		/// </summary>
		public decimal? ctr_hl_vent_hw { get; set; }

		/// <summary>
		/// Тепловая нагрузка по договорам теплоснабжения. В горячей воде. ГВСср.ч., Гкал/ч (46)
		/// </summary>
		public decimal? ctr_hl_gvss_hw { get; set; }

		/// <summary>
		/// Тепловая нагрузка по договорам теплоснабжения. В горячей воде. ВСЕГО, Гкал/ч (47)
		/// </summary>
		public decimal? ctr_hl_hw_sum { get; set; }

		/// <summary>
		/// Тепловая нагрузка по договорам теплоснабжения. В паре. Технология, т/ч (48)
		/// </summary>
		public decimal? ctr_hl_tech_steam { get; set; }

		/// <summary>
		/// Тепловая нагрузка по договорам теплоснабжения. В паре. Отопление, т/ч (49)
		/// </summary>
		public decimal? ctr_hl_heat_steam { get; set; }

		/// <summary>
		/// Тепловая нагрузка по договорам теплоснабжения. В паре. Вентиляция, т/ч (50)
		/// </summary>
		public decimal? ctr_hl_vent_steam { get; set; }

		/// <summary>
		/// Тепловая нагрузка по договорам теплоснабжения. В паре. ГВСср.ч., т/ч (51)
		/// </summary>
		public decimal? ctr_hl_gvss_steam { get; set; }

		/// <summary>
		/// Тепловая нагрузка по договорам теплоснабжения. В паре. ВСЕГО, т/ч (52)
		/// </summary>
		public decimal? ctr_hl_steam_sum { get; set; }

		/// <summary>
		/// Тепловая нагрузка по договорам теплоснабжения. В паре. ВСЕГО, Гкал/ч (53)
		/// </summary>
		public decimal? ctr_hl_steam_sum_gkalh { get; set; }

		/// <summary>
		/// Расчетная тепловая нагрузка. В горячей воде. Технология, Гкал/ч (54)
		/// </summary>
		public decimal? calc_hl_tech_hw { get; set; }

		/// <summary>
		/// Расчетная тепловая нагрузка. В горячей воде. Отопление, Гкал/ч (55)
		/// </summary>
		public decimal? calc_hl_heat_hw { get; set; }

		/// <summary>
		/// Расчетная тепловая нагрузка. В горячей воде. Вентиляция, Гкал/ч (56)
		/// </summary>
		public decimal? calc_hl_vent_hw { get; set; }

		/// <summary>
		/// Расчетная тепловая нагрузка. В горячей воде. ГВСср.ч., Гкал/ч (57)
		/// </summary>
		public decimal? calc_hl_gvss_hw { get; set; }

		/// <summary>
		/// Расчетная тепловая нагрузка. В горячей воде. Суммарная  нагрузка, Гкал/ч (58)
		/// </summary>
		public decimal? calc_hl_hw_sum { get; set; }

		/// <summary>
		/// Расчетная тепловая нагрузка. В паре. Технология, т/ч (59)
		/// </summary>
		public decimal? calc_hl_tech_steam { get; set; }

		/// <summary>
		/// Расчетная тепловая нагрузка. В паре. Отопление, т/ч (60)
		/// </summary>
		public decimal? calc_hl_heat_steam { get; set; }

		/// <summary>
		/// Расчетная тепловая нагрузка. В паре. Вентиляция, т/ч (61)
		/// </summary>
		public decimal? calc_hl_vent_steam { get; set; }

		/// <summary>
		/// Расчетная тепловая нагрузка. В паре. ГВСср.ч., т/ч (62)
		/// </summary>
		public decimal? calc_hl_gvss_steam { get; set; }

		/// <summary>
		/// Расчетная тепловая нагрузка. В паре. Суммарная  нагрузка, т/ч (63)
		/// </summary>
		public decimal? calc_hl_steam_sum { get; set; }

		/// <summary>
		/// Расчетная тепловая нагрузка. В паре. Суммарная  нагрузка, Гкал/ч (64)
		/// </summary>
		public decimal? calc_hl_steam_sum_gkalh { get; set; }

		/// <summary>
		/// Зарасчетная тепловая нагрузка. В горячей воде. Технология, Гкал/ч (65)
		/// </summary>
		public decimal? b_calc_hl_tech_hw { get; set; }

		/// <summary>
		/// Зарасчетная тепловая нагрузка. В горячей воде. Отопление, Гкал/ч (66)
		/// </summary>
		public decimal? b_calc_hl_heat_hw { get; set; }

		/// <summary>
		/// Зарасчетная тепловая нагрузка. В горячей воде. Вентиляция, Гкал/ч (67)
		/// </summary>
		public decimal? b_calc_hl_vent_hw { get; set; }

		/// <summary>
		/// Зарасчетная тепловая нагрузка. В горячей воде. ГВСср.ч., Гкал/ч (68)
		/// </summary>
		public decimal? b_calc_hl_gvss_hw { get; set; }

		/// <summary>
		/// Зарасчетная тепловая нагрузка. В горячей воде. Суммарная  нагрузка, Гкал/ч (69)
		/// </summary>
		public decimal? b_calc_hl_hw_sum { get; set; }

		/// <summary>
		/// Зарасчетная тепловая нагрузка. В паре. Технология, т/ч (70)
		/// </summary>
		public decimal? b_calc_hl_tech_steam { get; set; }

		/// <summary>
		/// Зарасчетная тепловая нагрузка. В паре. Отопление, т/ч (71)
		/// </summary>
		public decimal? b_calc_hl_heat_steam { get; set; }

		/// <summary>
		/// Зарасчетная тепловая нагрузка. В паре. Вентиляция, т/ч (72)
		/// </summary>
		public decimal? b_calc_hl_vent_steam { get; set; }

		/// <summary>
		/// Зарасчетная тепловая нагрузка. В паре. ГВСср.ч., т/ч (73)
		/// </summary>
		public decimal? b_calc_hl_gvss_steam { get; set; }

		/// <summary>
		/// Зарасчетная тепловая нагрузка. В паре. Суммарная  нагрузка, т/ч (74)
		/// </summary>
		public decimal? b_calc_hl_steam_sum { get; set; }

		/// <summary>
		/// Зарасчетная тепловая нагрузка. В паре. Суммарная  нагрузка, Гкал/ч (75)
		/// </summary>
		public decimal? b_calc_hl_steam_sum_gkalh { get; set; }

		/// <summary>
		/// Теплопотребление. Фактическое суммарное. В горячей воде, Гкал/год (76)
		/// </summary>
		public decimal? fact_total_hc_hw { get; set; }

		/// <summary>
		/// Теплопотребление. Фактическое суммарное. В паре, т/год (77)
		/// </summary>
		public decimal? fact_total_hc_steam { get; set; }

		/// <summary>
		/// Теплопотребление. Фактическое суммарное. В паре, Гкал/год (78)
		/// </summary>
		public decimal? fact_total_hc_steam_gkalh { get; set; }

		/// <summary>
		/// Теплопотребление. Расчетное. В горячей воде. Технология, Гкал/год (79)
		/// </summary>
		public decimal? calc_hc_tech_hw { get; set; }

		/// <summary>
		/// Теплопотребление. Расчетное. В горячей воде. Отопление, Гкал/год (80)
		/// </summary>
		public decimal? calc_hc_heat_hw { get; set; }

		/// <summary>
		/// Теплопотребление. Расчетное. В горячей воде. Вентиляция, Гкал/год (81)
		/// </summary>
		public decimal? calc_hc_vent_hw { get; set; }

		/// <summary>
		/// Теплопотребление. Расчетное. В горячей воде. ГВС, Гкал/год (82)
		/// </summary>
		public decimal? calc_hc_gvs_hw { get; set; }

		/// <summary>
		/// Теплопотребление. Расчетное. В горячей воде. Суммарное теплопотребление, Гкал/год (83)
		/// </summary>
		public decimal? calc_hc_hw_sum { get; set; }

		/// <summary>
		/// Теплопотребление. Расчетное. В паре. Технология, т/год (84)
		/// </summary>
		public decimal? calc_hc_tech_steam { get; set; }

		/// <summary>
		/// Теплопотребление. Расчетное. В паре. Отопление, т/год (85)
		/// </summary>
		public decimal? calc_hc_heat_steam { get; set; }

		/// <summary>
		/// Теплопотребление. Расчетное. В паре. Вентиляция, т/год (86)
		/// </summary>
		public decimal? calc_hc_vent_steam { get; set; }

		/// <summary>
		/// Теплопотребление. Расчетное. В паре. ГВС, т/год (87)
		/// </summary>
		public decimal? calc_hc_gvs_steam { get; set; }

		/// <summary>
		/// Теплопотребление. Расчетное. В паре. Суммарное теплопотребление, т/год (88)
		/// </summary>
		public decimal? calc_hc_steam_sum { get; set; }

		/// <summary>
		/// Теплопотребление. Расчетное. В паре. Суммарное теплопотребление, Гкал/год (89)
		/// </summary>
		public decimal? calc_hc_steam_sum_gkalh { get; set; }

		/// <summary>
		/// Теплопотребление. Запроектное. В горячей воде. Технология, Гкал/год (90)
		/// </summary>
		public decimal? b_calc_hc_tech_hw { get; set; }

		/// <summary>
		/// Теплопотребление. Запроектное. В горячей воде. Отопление, Гкал/год (91)
		/// </summary>
		public decimal? b_calc_hc_heat_hw { get; set; }

		/// <summary>
		/// Теплопотребление. Запроектное. В горячей воде. Вентиляция, Гкал/год (92)
		/// </summary>
		public decimal? b_calc_hc_vent_hw { get; set; }

		/// <summary>
		/// Теплопотребление. Запроектное. В горячей воде. ГВС, Гкал/год (93)
		/// </summary>
		public decimal? b_calc_hc_gvs_hw { get; set; }

		/// <summary>
		/// Теплопотребление. Запроектное. В горячей воде. Суммарное теплопотребление, Гкал/год (94)
		/// </summary>
		public decimal? b_calc_hc_hw_sum { get; set; }

		/// <summary>
		/// Теплопотребление. Запроектное. В паре. Технология, т/год (95)
		/// </summary>
		public decimal? b_calc_hc_tech_steam { get; set; }

		/// <summary>
		/// Теплопотребление. Запроектное. В паре. Отопление, т/год (96)
		/// </summary>
		public decimal? b_calc_hc_heat_steam { get; set; }

		/// <summary>
		/// Теплопотребление. Запроектное. В паре. Вентиляция, т/год (97)
		/// </summary>
		public decimal? b_calc_hc_vent_steam { get; set; }

		/// <summary>
		/// Теплопотребление. Запроектное. В паре. ГВС, т/год (98)
		/// </summary>
		public decimal? b_calc_hc_gvs_steam { get; set; }

		/// <summary>
		/// Теплопотребление. Запроектное. В паре. Суммарное теплопотребление, т/год (99)
		/// </summary>
		public decimal? b_calc_hc_steam_sum { get; set; }

		/// <summary>
		/// Теплопотребление. Запроектное. В паре. Суммарное теплопотребление, Гкал/год (100)
		/// </summary>
		public decimal? b_calc_hc_steam_sum_gkalh { get; set; }

		/// <summary>
		/// Расход теплоносителя. Фактический расход исходной воды на нужды ГВС, куб. м/год (101)
		/// </summary>
		public decimal? fact_watercons_gvs { get; set; }

		/// <summary>
		/// Расход теплоносителя. В горячей воде. Технология, т/ч (102)
		/// </summary>
		public decimal? heat_carrier_consump_tech_hw { get; set; }

		/// <summary>
		/// Расход теплоносителя. В горячей воде. Отопление, т/ч (103)
		/// </summary>
		public decimal? heat_carrier_consump_heat_hw { get; set; }

		/// <summary>
		/// Расход теплоносителя. В горячей воде. Вентиляция, т/ч (104)
		/// </summary>
		public decimal? heat_carrier_consump_vent_hw { get; set; }

		/// <summary>
		/// Расход теплоносителя. В горячей воде. ГВС, т/ч (105)
		/// </summary>
		public decimal? heat_carrier_consump_gvs_hw { get; set; }

		/// <summary>
		/// Расход теплоносителя. В горячей воде. Суммарный расход, т/ч (106)
		/// </summary>
		public decimal? heat_carrier_consump_sum_hw { get; set; }

		/// <summary>
		/// Расход теплоносителя. В паре. Технология, т/ч (107)
		/// </summary>
		public decimal? heat_carrier_consump_tech_steam { get; set; }

		/// <summary>
		/// Расход теплоносителя. В паре. Отопление, т/ч (108)
		/// </summary>
		public decimal? heat_carrier_consump_heat_steam { get; set; }

		/// <summary>
		/// Расход теплоносителя. В паре. Вентиляция, т/ч (109)
		/// </summary>
		public decimal? heat_carrier_consump_vent_steam { get; set; }

		/// <summary>
		/// Расход теплоносителя. В паре. ГВС, т/ч (110)
		/// </summary>
		public decimal? heat_carrier_consump_gvs_steam { get; set; }

		/// <summary>
		/// Расход теплоносителя. В паре. Суммарный расход, т/ч (111)
		/// </summary>
		public decimal? heat_carrier_consump_sum_steam { get; set; }

		/// <summary>
		/// УНОМ СЛ (112)
		/// </summary>
		public string? layer_unom { get; set; }

		/// <summary>
		/// SYS объекта в расчетном слое (113)
		/// </summary>
		public int? layer_sys { get; set; }

		/// <summary>
		/// УНОМ СЕТЬ теплового ввода (114)
		/// </summary>
		public int? heat_network_id { get; set; }
	}

	[Keyless]
	public class HPSourcesViewModel
	{
		public int value_id { get; set; }
		public string? value_name { get; set; }
		public string? source_name { get; set; }
		public int source_unom { get; set; }
	}


	#region HPHeaderAddRemoveDataViewModel
	[Keyless]
	public class HPHeaderAddRemoveDataViewModel
	{
		public int? hp_id { get; set; }
		public int? data_status { get; set; }
		public string? hp_tso { get; set; }
	}
	#endregion

	#region HPAddRemoveGenInfoDataViewModel
	[Keyless]
	public class HPAddRemoveGenInfoDataViewModel
	{
		public int hp_id { get; set; }
		public int data_status { get; set; }
		public string? add_remove_gen_info_hp_name { get; set; }
		public string? add_remove_gen_info_tso_hp_num { get; set; }
		public string? add_remove_gen_info_hp_address { get; set; }
		public short? add_remove_gen_info_hp_location_id { get; set; }
		public short? add_remove_gen_info_hp_type_id { get; set; }
		public int? add_remove_gen_info_hp_org_owner_id { get; set; }
		public bool add_remove_gen_info_is_ownerless_hp_id { get; set; }
		public bool add_remove_gen_info_is_dilapidated_hp { get; set; }
		public int? add_remove_gen_info_district_hp_id { get; set; }
		public string? add_remove_gen_info_kad_num_zu { get; set; }
		public string? add_remove_gen_info_kad_num_oks { get; set; }
		public int? add_remove_gen_info_fias_zu { get; set; }
		public int? add_remove_gen_info_fias_oks { get; set; }
		public int? add_remove_gen_info_start_year_expl { get; set; }
		public int? add_remove_gen_info_last_year_reconstr { get; set; }
		public int? add_remove_gen_info_liqiudate_year { get; set; }
		public int? add_remove_gen_info_tz_id { get; set; }
		[NotMapped]
		public bool? is_deleted { get; set; }
	}
	#endregion

	#region HPAddRemoveYearImplementSchemeParamDataViewModel
	[Keyless]
	public class HPAddRemoveYearImplementSchemeParamDataViewModel
	{
		public int hp_id { get; set; }
		public int data_status { get; set; }
		public int? add_remove_yisp_perspective_year { get; set; }
		public string? add_remove_yisp_perspective_year_dt { get; set; }
		public short? add_remove_yisp_hp_status_id { get; set; }
		public int? add_remove_yisp_hp_tso_id { get; set; }
		public int? add_remove_yisp_source_output_id { get; set; }
		public int? add_remove_yisp_heat_network_id { get; set; }
		public int? add_remove_yisp_diam_ht_supply { get; set; }
		public short? add_remove_yisp_temp_ht_plan_id { get; set; }
		public short? add_remove_yisp_hp_type_scheme_id { get; set; }
		public short? add_remove_yisp_tech_connect_id { get; set; }
		public short? add_remove_yisp_temp_tech_plan_id { get; set; }
		public short? add_remove_yisp_heat_connect_id { get; set; }
		public short? add_remove_yisp_temp_heat_plan_id { get; set; }
		public short? add_remove_yisp_vent_connect_id { get; set; }
		public short? add_remove_yisp_temp_vent_plan_id { get; set; }
		public short? add_remove_yisp_hw_connect_id { get; set; }
		public short? add_remove_yisp_temp_gvs_plan_id { get; set; }
	}
	#endregion

	[Keyless]
	public class HPAddRemoveHP_HeatExchangerMappsDataViewModel
	{
		public int data_status { get; set; }
		public int heat_point_id { get; set; }
		public short ht_exch_num { get; set; }
		public short? type_equip_id { get; set; }
		public short? type_id { get; set; }
		public short? stage_scheme_gvs_id { get; set; }
		public int? equip_id { get; set; }
		public short? ht_exch_count { get; set; }
		public decimal? heat_exchange_surface { get; set; }
		public decimal? inst_heat_power { get; set; }
		public int? section_count { get; set; }
		public decimal? casing_diameter { get; set; }
		public decimal? length_section { get; set; }

		[NotMapped]
		public bool? is_deleted { get; set; }
	}

	[Keyless]
	public class HPAddRemoveHP_EquipmentByMarkViewModel
	{
		public int equip_id { get; set; }
		public short? htexch_type_id { get; set; }
		public decimal? inst_heat_power { get; set; }
		public decimal? heat_exchange_surface { get; set; }
		public decimal? casing_diameter { get; set; }
		public int? section_count { get; set; }
		public decimal? length_section { get; set; }
	}

	[Keyless]
	public class HPAddRemoveHP_PumpMappsDataViewModel
	{
		public int data_status { get; set; }
		public int heat_point_id { get; set; }
		public short pump_num { get; set; }
		public short? type_pump_id { get; set; }
		public int? equip_id { get; set; }
		public short? pump_count { get; set; }
		public decimal? pump_capacity { get; set; }
		public decimal? pump_press { get; set; }

		[NotMapped]
		public bool? is_deleted { get; set; }
	}

	[Keyless]
	public class HPAddRemoveHP_PumpByMarkViewModel
	{
		public int equip_id { get; set; }
		public decimal? capacity { get; set; }
		public decimal? pump_press { get; set; }
	}

	[Keyless]
	public class HPAddRemove_AutomatizationViewModel
	{
		public int data_status { get; set; }
		public int heat_point_id { get; set; }
		public bool? avail_flow_limiter { get; set; }
		public bool? avail_automatic { get; set; }
		public short? automatic_type_id { get; set; }
		public bool? avail_automatic_heat_control { get; set; }
	}

	[Keyless]
	public class HPAddRemove_AccResourcesViewModel
	{
		public int data_status { get; set; }
		public int heat_point_id { get; set; }
		public short? acc_resource_type_id { get; set; }
		public int device_num { get; set; }
		public string? meter_device_mark { get; set; }
		public int count_devices { get; set; }
		[NotMapped]
		public bool? is_deleted { get; set; }
	}

	[Keyless]
	public class HPAddRemove_NumSignOtherDbViewModel
	{
		public int data_status { get; set; }
		public int heat_point_id { get; set; }
		public string? muid { get; set; }
		public string? lotus_id { get; set; }
		public string? kasu_composite_id { get; set; }
		public string? kasu_id { get; set; }
		public string? kasu_map_id { get; set; }
	}

	[Keyless]
	public class HPAddRemove_DocsFootageViewModel
	{
		public int data_status { get; set; }
		public int heat_point_id { get; set; }
		public short? doc_type_id { get; set; }
		public int doc_num { get; set; }
		public string? doc_description { get; set; }
		public string? doc_url { get; set; }
		public string? doc_name { get; set; }
		[NotMapped]
		public bool? is_deleted { get; set; }
	}
}
