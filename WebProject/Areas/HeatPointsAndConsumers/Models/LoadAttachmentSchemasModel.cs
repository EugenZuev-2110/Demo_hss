using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;

namespace WebProject.Areas.HeatPointsAndConsumers.Models
{
	/// <summary>
	/// Тепловые пункты. Схемы присоединения нагрузок
	/// </summary>
	[Keyless]
	public class LoadAttachmentSchemasModel
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
        /// Схема присоединения нагрузки. Отопление (18)
        /// </summary>
        public string? hp_heat_connect_name { get; set; }

        /// <summary>
        /// Схема присоединения нагрузки. Вентиляция (19)
        /// </summary>
        public string? hp_vent_connect_name { get; set; }

        /// <summary>
        /// Схема присоединения нагрузки. ГВС (20)
        /// </summary>
        public string? hp_hw_connect_name { get; set; }

        /// <summary>
        /// Схема присоединения нагрузки. Технологическая (21)
        /// </summary>
        public string? hp_tech_connect_name { get; set; }
	}
}
