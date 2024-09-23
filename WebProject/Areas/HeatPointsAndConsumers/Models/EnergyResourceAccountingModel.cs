using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;

namespace WebProject.Areas.HeatPointsAndConsumers.Models
{
    /// <summary>
    /// Тепловые пункты. Автоматизация
    /// </summary>
    [Keyless]
	public class EnergyResourceAccountingModel
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
        /// Автоматизация. УНОМ ТП (6)
        /// </summary>
        public int? heat_point_id { get; set; }

        /// <summary>
        /// Автоматизация. Номер теплового пункта (7)
        /// </summary>
        public string? hp_num { get; set; }

        /// <summary>
        /// Автоматизация. Административный округ (8)
        /// </summary>
        public string? hp_region_name { get; set; }

        /// <summary>
        /// Автоматизация. Район (9)
        /// </summary>
        public string? hp_district_name { get; set; }

        /// <summary>
        /// Автоматизация. Адрес месторасположения теплового пункта (10)
        /// </summary>
        public string? hp_address { get; set; }

        /// <summary>
        /// Автоматизация. Тип теплового пункта (11)
        /// </summary>
        public string? hp_type_name { get; set; }

		/// <summary>
		/// Тепловой пункт. Номер типовой схемы теплового пункта id (12)
		/// </summary>
		public short? hp_type_scheme_id { get; set; }

        /// <summary>
        /// Автоматизация. Тип размещения теплового пункта (13)
        /// </summary>
        public string? hp_type_location_name { get; set; }

        /// <summary>
        /// Автоматизация. Статус теплового пункта (14)
        /// </summary>
        public string? hp_status_name { get; set; }

        /// <summary>
        /// Автоматизация. Год ввода в эксплуатацию (15)
        /// </summary>
        public int? hp_start_year_expl { get; set; }

        /// <summary>
        /// Автоматизация. Год проведения последней реконструкции (16)
        /// </summary>
        public int? hp_last_year_reconstr { get; set; }

        /// <summary>
        /// Автоматизация. Год ликвидации теплового пункта (17)
        /// </summary>
        public int? hp_year_liquidate { get; set; }

        /// <summary>
        /// Учет энергетических ресурсов. Марка общего прибора учета тепловой энергии (18)
        /// </summary>
        public string? meter_device_count_mark { get; set; }

        /// <summary>
        /// Учет энергетических ресурсов. Марка общего прибора учета тепловой энергии, общее количество (19)
        /// </summary>
        public int? meter_device_count_all_mark { get; set; }

        /// <summary>
        /// Учет энергетических ресурсов. Марка прибора учета тепловой энергии отопления / вентиляции (20)
        /// </summary>
        public string? heat_vent_meter_device_count_mark { get; set; }

        /// <summary>
        /// Учет энергетических ресурсов. Марка прибора учета тепловой энергии отопления / вентиляции, общее количество (21)
        /// </summary>
        public int? heat_vent_meter_device_count_all_mark { get; set; }

        /// <summary>
        /// Учет энергетических ресурсов. Марка прибора учета тепловой энергии ГВС (22)
        /// </summary>
        public string? gvs_meter_device_count_mark { get; set; }

        /// <summary>
        /// Учет энергетических ресурсов. Марка прибора учета тепловой энергии ГВС, общее количество (23)
        /// </summary>
        public int? gvs_meter_device_count_all_mark { get; set; }

        /// <summary>
        /// Учет энергетических ресурсов. Марка прибора учета холодной воды (24)
        /// </summary>
        public string? cold_water_meter_device_count_mark { get; set; }

        /// <summary>
        /// Учет энергетических ресурсов. Марка прибора учета холодной воды, общее количество (25)
        /// </summary>
        public int? cold_water_meter_device_count_all_mark { get; set; }

        /// <summary>
        /// Учет энергетических ресурсов. Марка прибора учета электроэнергии (26)
        /// </summary>
        public string? electr_meter_device_count_mark { get; set; }

        /// <summary>
        /// Учет энергетических ресурсов. Марка прибора учета электроэнергии, общее количество (27)
        /// </summary>
        public int? electr_meter_device_count_all_mark { get; set; }
    }
}
