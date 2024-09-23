using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;

namespace WebProject.Areas.HeatPointsAndConsumers.Models
{
	/// <summary>
	/// Тепловые пункты. Оборудование
	/// </summary>
	[Keyless]
	public class HeatPointsEquipment
	{
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
		/// Установленная тепловая мощность теплового пункта, Гкал/ч (18)
		/// </summary>
		public decimal? hp_inst_heat_power { get; set; }

		/// <summary>
		/// Емкость баков аккумуляторов, куб. м (19)
		/// </summary>
		public decimal? tank_capacity { get; set; }

		/// <summary>
		/// Тип теплообменника отопления (20)
		/// </summary>
		public string? he_count_type_heat { get; set; }

		/// <summary>
		/// Марка теплообменника отопления (21)
		/// </summary>
		public string? he_count_mark_heat { get; set; }

		/// <summary>
		/// Поверхность теплообмена теплообменника отопления, кв. м (22)
		/// </summary>
		public decimal? he_surf_heat { get; set; }

		/// <summary>
		/// Теплопроизводительность теплообменника отопления, Гкал/ч (23)
		/// </summary>
		public decimal? he_capacity_heat { get; set; }

		/// <summary>
		/// Тип теплообменника вентиляции (24)
		/// </summary>
		public string? he_count_type_vent { get; set; }

		/// <summary>
		/// Марка теплообменника вентиляции (25)
		/// </summary>
		public string? he_count_mark_vent { get; set; }

		/// <summary>
		/// Поверхность теплообмена теплообменника вентиляции, кв. м (26)
		/// </summary>
		public decimal? he_surf_vent { get; set; }

		/// <summary>
		/// Теплопроизводительность теплообменника вентиляции, Гкал/ч (27)
		/// </summary>
		public decimal? he_capacity_vent { get; set; }

		/// <summary>
		/// Тип теплообменника вентиляции (28)
		/// </summary>
		public string? he_count_type_gvs { get; set; }

		/// <summary>
		/// Марка теплообменника вентиляции (29)
		/// </summary>
		public string? he_count_mark_gvs { get; set; }

		/// <summary>
		/// Поверхность теплообмена теплообменника вентиляции, кв. м (30)
		/// </summary>
		public decimal? he_surf_gvs { get; set; }

		/// <summary>
		/// Теплопроизводительность теплообменника вентиляции, Гкал/ч (31)
		/// </summary>
		public decimal? he_capacity_gvs { get; set; }

        /// <summary>
        /// Всего количество теплообменников, т/ч (32)
        /// </summary>
        public int? count_exch { get; set; }

        /// <summary>
        /// Марка насоса ХВС (33)
        /// </summary>
        public string? pump_count_mark_hvs { get; set; }

		/// <summary>
		/// Производительность насоса ХВС, т/ч (34)
		/// </summary>
		public decimal? pump_capacity_hvs { get; set; }

		/// <summary>
		/// Марка насоса ГВС (35)
		/// </summary>
		public string? pump_count_mark_gvs { get; set; }

		/// <summary>
		/// Производительность насоса ГВС, т/ч (36)
		/// </summary>
		public decimal? pump_capacity_gvs { get; set; }

		/// <summary>
		/// Марка насоса отопления (37)
		/// </summary>
		public string? pump_count_mark_heat { get; set; }

		/// <summary>
		/// Производительность насоса отопления, т/ч (38)
		/// </summary>
		public decimal? pump_capacity_heat { get; set; }

		/// <summary>
		/// Марка подпиточного насоса отопления (39)
		/// </summary>
		public string? pump_count_mark_energize_heat { get; set; }

		/// <summary>
		/// Производительность подпиточного насоса отопления, т/ч (40)
		/// </summary>
		public decimal? pump_capacity_energize_heat { get; set; }

		/// <summary>
		/// Марка насоса вентиляции (41)
		/// </summary>
		public string? pump_count_mark_vent { get; set; }

		/// <summary>
		/// Производительность насоса вентиляции, т/ч (42)
		/// </summary>
		public decimal? pump_capacity_vent { get; set; }

		/// <summary>
		/// Марка подпиточного насоса вентиляции (43)
		/// </summary>
		public string? pump_count_mark_energize_vent { get; set; }

		/// <summary>
		/// Производительность подпиточного насоса вентиляции, т/ч (44)
		/// </summary>
		public decimal? pump_capacity_energize_vent { get; set; }

		/// <summary>
		/// Марка дренажного насоса (45)
		/// </summary>
		public string? pump_count_mark_drain { get; set; }

		/// <summary>
		/// Производительность дренажного насоса, т/ч (46)
		/// </summary>
		public decimal? pump_capacity_drain { get; set; }

		/// <summary>
		/// Марка пожарного насоса (47)
		/// </summary>
		public string? pump_count_mark_fire { get; set; }

		/// <summary>
		/// Производительность пожарного насоса, т/ч (48)
		/// </summary>
		public decimal? pump_capacity_fire { get; set; }

		/// <summary>
		/// Марка хозяйственного насоса (49)
		/// </summary>
		public string? pump_count_mark_household { get; set; }

		/// <summary>
		/// Производительность хозяйственного насоса, т/ч (50)
		/// </summary>
		public decimal? pump_capacity_household { get; set; }

		/// <summary>
		/// Марка насоса смешения (51)
		/// </summary>
		public string? pump_count_mark_mixing { get; set; }

		/// <summary>
		/// Производительность насоса смешения, т/ч (52)
		/// </summary>
		public decimal? pump_capacity_mixing { get; set; }

        /// <summary>
        /// Всего количество насосов, т/ч (53)
        /// </summary>
        public int? count_pump { get; set; }

        /// <summary>
        /// Всего производительность насосов, т/ч (54)
        /// </summary>
        public decimal? pump_capacity_all { get; set; }
    }
}
