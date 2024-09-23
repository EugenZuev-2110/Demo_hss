using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;

namespace WebProject.Areas.HeatPointsAndConsumers.Models
{
	/// <summary>
	/// Нагрузки и расходы. Левая часть главного окна
	/// </summary>
	[Keyless]
	public class LoadsAndExpensivesMainList
	{
		public int? data_status { get; set; }
		public int? hp_type_id { get; set; }
		public int? hp_status_id { get; set; }
		public int? source_id { get; set; }
		public int? tso_id { get; set; }
		public int? load_type { get; set; }

		/// <summary>
		/// Источник тепловой энергии. УНОМ ИСТ (1)
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
		/// Тепловой пункт. Номер типовой схемы теплового пункта (12)
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
		/// Коэффициент для перевода т/ч в Гкал/ч
		/// </summary>
		public decimal? coef { get; set; }

		/// <summary>
		/// Существующее положение. Договорная приведеная тепловая нагрузка в горячей воде. Технология, Гкал/ч (18)
		/// </summary>
		public decimal? hl_tech_hw { get; set; }

		/// <summary>
		/// Существующее положение. Договорная приведеная тепловая нагрузка в горячей воде. Отопление, Гкал/ч (19)
		/// </summary>
		public decimal? hl_heat_hw { get; set; }

		/// <summary>
		/// Существующее положение. Договорная приведеная тепловая нагрузка в горячей воде. Вентиляция, Гкал/ч (20)
		/// </summary>
		public decimal? hl_vent_hw { get; set; }

		/// <summary>
		/// Существующее положение. Договорная приведеная тепловая нагрузка в горячей воде. ГВСср.ч., Гкал/ч (21)
		/// </summary>
		public decimal? hl_gvss_hw { get; set; }

		/// <summary>
		/// Существующее положение. Договорная приведеная тепловая нагрузка в горячей воде. ВСЕГО, Гкал/ч (22)
		/// </summary>
		public decimal? hl_hw_sum { get; set; }

		/// <summary>
		/// Существующее положение. Договорная приведеная тепловая нагрузка в паре. Технология, т/ч (23)
		/// </summary>
		public decimal? hl_tech_steam { get; set; }

		/// <summary>
		/// Существующее положение. Договорная приведеная тепловая нагрузка в паре. Отопление, т/ч (24)
		/// </summary>
		public decimal? hl_heat_steam { get; set; }

		/// <summary>
		/// Существующее положение. Договорная приведеная тепловая нагрузка в паре. Вентиляция, т/ч (25)
		/// </summary>
		public decimal? hl_vent_steam { get; set; }

		/// <summary>
		/// Существующее положение. Договорная приведеная тепловая нагрузка в паре. ГВСср.ч., т/ч (26)
		/// </summary>
		public decimal? hl_gvss_steam { get; set; }

		/// <summary>
		/// Существующее положение. Договорная приведеная тепловая нагрузка в паре. ВСЕГО, т/ч (27)
		/// </summary>
		public decimal? hl_steam_sum { get; set; }

		/// <summary>
		/// Существующее положение. Договорная приведеная тепловая нагрузка в паре. ВСЕГО, Гкал/ч (28)
		/// </summary>
		public decimal? hl_steam_sum_gkalh { get; set; }

		/// <summary>
		/// Расход теплоносителя. В горячей воде. Технология, т/ч (29)
		/// </summary>
		public decimal? heat_carrier_consump_tech_hw { get; set; }

		/// <summary>
		/// Расход теплоносителя. В горячей воде. Отопление, т/ч (30)
		/// </summary>
		public decimal? heat_carrier_consump_heat_hw { get; set; }

		/// <summary>
		/// Расход теплоносителя. В горячей воде. Вентиляция, т/ч (31)
		/// </summary>
		public decimal? heat_carrier_consump_vent_hw { get; set; }

		/// <summary>
		/// Расход теплоносителя. В горячей воде. ГВС, т/ч (32)
		/// </summary>
		public decimal? heat_carrier_consump_gvs_hw { get; set; }

		/// <summary>
		/// Расход теплоносителя. В горячей воде. ВСЕГО, т/ч (33)
		/// </summary>
		public decimal? heat_carrier_consump_sum_hw { get; set; }

		/// <summary>
		/// Расход теплоносителя. В паре. Технология, т/ч (34)
		/// </summary>
		public decimal? heat_carrier_consump_tech_steam { get; set; }

		/// <summary>
		/// Расход теплоносителя. В паре. Отопление, т/ч (35)
		/// </summary>
		public decimal? heat_carrier_consump_heat_steam { get; set; }

		/// <summary>
		/// Расход теплоносителя. В паре. Вентиляция, т/ч (36)
		/// </summary>
		public decimal? heat_carrier_consump_vent_steam { get; set; }

		/// <summary>
		/// Расход теплоносителя. В паре. ГВС, т/ч (37)
		/// </summary>
		public decimal? heat_carrier_consump_gvs_steam { get; set; }

		/// <summary>
		/// Расход теплоносителя. В паре. ВСЕГО, т/ч (38)
		/// </summary>
		public decimal? heat_carrier_consump_sum_steam { get; set; }
	}

	/// <summary>
	/// Нагрузки и расходы. Правая часть главного окна
	/// </summary>
	[Keyless]
	public class LoadsAndExpensivesPerspectiveMainList
	{
		public int? heat_point_id { get; set; }
		public int? perspective_year { get; set; }

		/// <summary>
		/// Источник тепловой энергии. УНОМ ИСТ (39)
		/// </summary>
		public int? source_unom { get; set; }

		/// <summary>
		/// Источник тепловой энергии. Наименование (40)
		/// </summary>
		public string? source_name { get; set; }

		/// <summary>
		/// Источник тепловой энергии. Статус источника (41)
		/// </summary>
		public string? source_status_name { get; set; }

		/// <summary>
		/// Источник тепловой энергии. УНОМ Вывода (42)
		/// </summary>
		public string? source_output_unom { get; set; }

		/// <summary>
		/// Источник тепловой энергии. Наименование вывода (43)
		/// </summary>
		public string? source_output_name { get; set; }

		/// <summary>
		/// Тепловой пункт. Статус теплового пункта (44)
		/// </summary>
		public string? hp_status_name { get; set; }

		/// <summary>
		/// Нагрузка на n+1 год. Расчетная тепловая нагрузка/Зарасчетная тепловая нагрузка в горячей воде. Технология, Гкал/ч (45)
		/// </summary>
		public decimal? hl_tech_hw { get; set; }

		/// <summary>
		/// Нагрузка на n+1 год. Расчетная тепловая нагрузка/Зарасчетная тепловая нагрузка в горячей воде. Отопление, Гкал/ч (46)
		/// </summary>
		public decimal? hl_heat_hw { get; set; }

		/// <summary>
		/// Нагрузка на n+1 год. Расчетная тепловая нагрузка/Зарасчетная тепловая нагрузка в горячей воде. Вентиляция, Гкал/ч (47)
		/// </summary>
		public decimal? hl_vent_hw { get; set; }

		/// <summary>
		/// Нагрузка на n+1 год. Расчетная тепловая нагрузка/Зарасчетная тепловая нагрузка в горячей воде. ГВСср.ч., Гкал/ч (48)
		/// </summary>
		public decimal? hl_gvss_hw { get; set; }

		/// <summary>
		/// Нагрузка на n+1 год. Расчетная тепловая нагрузка/Зарасчетная тепловая нагрузка в горячей воде. ВСЕГО, Гкал/ч (49)
		/// </summary>
		public decimal? hl_hw_sum { get; set; }

		/// <summary>
		/// Нагрузка на n+1 год. Расчетная тепловая нагрузка/Зарасчетная тепловая нагрузка в паре. Технология, т/ч (50)
		/// </summary>
		public decimal? hl_tech_steam { get; set; }

		/// <summary>
		/// Нагрузка на n+1 год. Расчетная тепловая нагрузка/Зарасчетная тепловая нагрузка в паре. Отопление, т/ч (51)
		/// </summary>
		public decimal? hl_heat_steam { get; set; }

		/// <summary>
		/// Нагрузка на n+1 год. Расчетная тепловая нагрузка/Зарасчетная тепловая нагрузка в паре. Вентиляция, т/ч (52)
		/// </summary>
		public decimal? hl_vent_steam { get; set; }

		/// <summary>
		/// Нагрузка на n+1 год. Расчетная тепловая нагрузка/Зарасчетная тепловая нагрузка в паре. ГВСср.ч., т/ч (53)
		/// </summary>
		public decimal? hl_gvss_steam { get; set; }

		/// <summary>
		/// Нагрузка на n+1 год. Расчетная тепловая нагрузка/Зарасчетная тепловая нагрузка в паре. ВСЕГО, т/ч (54)
		/// </summary>
		public decimal? hl_steam_sum { get; set; }

		/// <summary>
		/// Нагрузка на n+1 год. Расчетная тепловая нагрузка/Зарасчетная тепловая нагрузка в паре. ВСЕГО, Гкал/ч (55)
		/// </summary>
		public decimal? hl_steam_sum_gkalh { get; set; }

		/// <summary>
		/// Нагрузка на n+1 год. Расход теплоносителя. В горячей воде. Технология, т/ч (56)
		/// </summary>
		public decimal? heat_carrier_consump_tech_hw { get; set; }

		/// <summary>
		/// Нагрузка на n+1 год. Расход теплоносителя. В горячей воде. Отопление, т/ч (57)
		/// </summary>
		public decimal? heat_carrier_consump_heat_hw { get; set; }

		/// <summary>
		/// Нагрузка на n+1 год. Расход теплоносителя. В горячей воде. Вентиляция, т/ч (58)
		/// </summary>
		public decimal? heat_carrier_consump_vent_hw { get; set; }

		/// <summary>
		/// Нагрузка на n+1 год. Расход теплоносителя. В горячей воде. ГВС, т/ч (59)
		/// </summary>
		public decimal? heat_carrier_consump_gvs_hw { get; set; }

		/// <summary>
		/// Нагрузка на n+1 год. Расход теплоносителя. В горячей воде. ВСЕГО, т/ч (60)
		/// </summary>
		public decimal? heat_carrier_consump_sum_hw { get; set; }

		/// <summary>
		/// Нагрузка на n+1 год. Расход теплоносителя. В паре. Технология, т/ч (61)
		/// </summary>
		public decimal? heat_carrier_consump_tech_steam { get; set; }

		/// <summary>
		/// Нагрузка на n+1 год. Расход теплоносителя. В паре. Отопление, т/ч (62)
		/// </summary>
		public decimal? heat_carrier_consump_heat_steam { get; set; }

		/// <summary>
		/// Нагрузка на n+1 год. Расход теплоносителя. В паре. Вентиляция, т/ч (63)
		/// </summary>
		public decimal? heat_carrier_consump_vent_steam { get; set; }

		/// <summary>
		/// Нагрузка на n+1 год. Расход теплоносителя. В паре. ГВС, т/ч (64)
		/// </summary>
		public decimal? heat_carrier_consump_gvs_steam { get; set; }

		/// <summary>
		/// Расход теплоносителя. В паре. ВСЕГО, т/ч (65)
		/// </summary>
		public decimal? heat_carrier_consump_sum_steam { get; set; }
	}

	[Keyless]
	public class LoadsAndExpensivesMainModel
	{
		public List<LoadsAndExpensivesMainList> LoadsAndExpensivesMain { get; set; }
		public List<LoadsAndExpensivesPerspectiveMainList> LoadsAndExpensivesPerspectiveMain { get; set; }
	}
}
