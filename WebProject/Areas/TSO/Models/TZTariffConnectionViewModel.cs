using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Areas.TSO.Models
{
	#region Tariffs

	#region HeatEnergyTariffSteam

	/// <summary>
	/// Тариф на тепловую энергию в паре (одноставочный), руб./Гкал без НДС
	/// </summary>
	[Keyless]
	public class HeatEnergyTariffSteam
	{
		public int tz_id { get; set; } = 0;
		public int data_status { get; set; }
		public int perspective_year { get; set; } // (3)

		public decimal? tariff_econom_justified_p1_per_1 { get; set; }
		public decimal? volume_he_p1_per_1 { get; set; } 
		public decimal? useful_release_ownprod_needs_p1_per_1 { get; set; }
		public decimal? useful_release_household_needs_p1_per_1 { get; set; }
		public decimal? tariff_weighted_invest_p1_per_1 { get; set; }
		public decimal? invest_component_p1_per_1 { get; set; }
		public decimal? tariff_tso_p1_per_1 { get; set; }
		public decimal? volume_he_tso_p1_per_1 { get; set; }
		public decimal? tariff_budgetcons_p1_per_1 { get; set; }
		public decimal? volume_he_budgetcons_p1_per_1 { get; set; }
		public decimal? tariff_public_p1_per_1 { get; set; }
		public decimal? volume_he_public_p1_per_1 { get; set; }
		public decimal? tariff_other_p1_per_1 { get; set; }
		public decimal? volume_he_other_p1_per_1 { get; set; } //(17)

		public decimal? tariff_econom_justified_p1_per_2 { get; set; }
		public decimal? volume_he_p1_per_2 { get; set; }
		public decimal? useful_release_ownprod_needs_p1_per_2 { get; set; }
		public decimal? useful_release_household_needs_p1_per_2 { get; set; }
		public decimal? tariff_weighted_invest_p1_per_2 { get; set; }
		public decimal? invest_component_p1_per_2 { get; set; }
		public decimal? tariff_tso_p1_per_2 { get; set; }
		public decimal? volume_he_tso_p1_per_2 { get; set; }
		public decimal? tariff_budgetcons_p1_per_2 { get; set; }
		public decimal? volume_he_budgetcons_p1_per_2 { get; set; }
		public decimal? tariff_public_p1_per_2 { get; set; }
		public decimal? volume_he_public_p1_per_2 { get; set; }
		public decimal? tariff_other_p1_per_2 { get; set; }
		public decimal? volume_he_other_p1_per_2 { get; set; } //(31)

		public decimal? tariff_econom_justified_p1_per_3 { get; set; }
		public decimal? volume_he_p1_per_3 { get; set; }
		public decimal? useful_release_ownprod_needs_p1_per_3 { get; set; }
		public decimal? useful_release_household_needs_p1_per_3 { get; set; }
		public decimal? tariff_weighted_invest_p1_per_3 { get; set; }
		public decimal? invest_component_p1_per_3 { get; set; }
		public decimal? tariff_tso_p1_per_3 { get; set; }
		public decimal? volume_he_tso_p1_per_3 { get; set; }
		public decimal? tariff_budgetcons_p1_per_3 { get; set; }
		public decimal? volume_he_budgetcons_p1_per_3 { get; set; }
		public decimal? tariff_public_p1_per_3 { get; set; }
		public decimal? volume_he_public_p1_per_3 { get; set; }
		public decimal? tariff_other_p1_per_3 { get; set; }
		public decimal? volume_he_other_p1_per_3 { get; set; } //(45)



		public decimal? tariff_econom_justified_p2_per_1 { get; set; }
		public decimal? volume_he_p2_per_1 { get; set; }
		public decimal? useful_release_ownprod_needs_p2_per_1 { get; set; }
		public decimal? useful_release_household_needs_p2_per_1 { get; set; }
		public decimal? tariff_weighted_invest_p2_per_1 { get; set; }
		public decimal? invest_component_p2_per_1 { get; set; }
		public decimal? tariff_tso_p2_per_1 { get; set; }
		public decimal? volume_he_tso_p2_per_1 { get; set; }
		public decimal? tariff_budgetcons_p2_per_1 { get; set; }
		public decimal? volume_he_budgetcons_p2_per_1 { get; set; }
		public decimal? tariff_public_p2_per_1 { get; set; }
		public decimal? volume_he_public_p2_per_1 { get; set; }
		public decimal? tariff_other_p2_per_1 { get; set; }
		public decimal? volume_he_other_p2_per_1 { get; set; } //(59)

		public decimal? tariff_econom_justified_p2_per_2 { get; set; }
		public decimal? volume_he_p2_per_2 { get; set; }
		public decimal? useful_release_ownprod_needs_p2_per_2 { get; set; }
		public decimal? useful_release_household_needs_p2_per_2 { get; set; }
		public decimal? tariff_weighted_invest_p2_per_2 { get; set; }
		public decimal? invest_component_p2_per_2 { get; set; }
		public decimal? tariff_tso_p2_per_2 { get; set; }
		public decimal? volume_he_tso_p2_per_2 { get; set; }
		public decimal? tariff_budgetcons_p2_per_2 { get; set; }
		public decimal? volume_he_budgetcons_p2_per_2 { get; set; }
		public decimal? tariff_public_p2_per_2 { get; set; }
		public decimal? volume_he_public_p2_per_2 { get; set; }
		public decimal? tariff_other_p2_per_2 { get; set; }
		public decimal? volume_he_other_p2_per_2 { get; set; } //(73)

		public decimal? tariff_econom_justified_p2_per_3 { get; set; }
		public decimal? volume_he_p2_per_3 { get; set; }
		public decimal? useful_release_ownprod_needs_p2_per_3 { get; set; }
		public decimal? useful_release_household_needs_p2_per_3 { get; set; }
		public decimal? tariff_weighted_invest_p2_per_3 { get; set; }
		public decimal? invest_component_p2_per_3 { get; set; }
		public decimal? tariff_tso_p2_per_3 { get; set; }
		public decimal? volume_he_tso_p2_per_3 { get; set; }
		public decimal? tariff_budgetcons_p2_per_3 { get; set; }
		public decimal? volume_he_budgetcons_p2_per_3 { get; set; }
		public decimal? tariff_public_p2_per_3 { get; set; }
		public decimal? volume_he_public_p2_per_3 { get; set; }
		public decimal? tariff_other_p2_per_3 { get; set; }
		public decimal? volume_he_other_p2_per_3 { get; set; } //(87)



		public decimal? tariff_econom_justified_p3_per_1 { get; set; }
		public decimal? volume_he_p3_per_1 { get; set; }
		public decimal? useful_release_ownprod_needs_p3_per_1 { get; set; }
		public decimal? useful_release_household_needs_p3_per_1 { get; set; }
		public decimal? tariff_weighted_invest_p3_per_1 { get; set; }
		public decimal? invest_component_p3_per_1 { get; set; }
		public decimal? tariff_tso_p3_per_1 { get; set; }
		public decimal? volume_he_tso_p3_per_1 { get; set; }
		public decimal? tariff_budgetcons_p3_per_1 { get; set; }
		public decimal? volume_he_budgetcons_p3_per_1 { get; set; }
		public decimal? tariff_public_p3_per_1 { get; set; }
		public decimal? volume_he_public_p3_per_1 { get; set; }
		public decimal? tariff_other_p3_per_1 { get; set; }
		public decimal? volume_he_other_p3_per_1 { get; set; } //(101)

		public decimal? tariff_econom_justified_p3_per_2 { get; set; }
		public decimal? volume_he_p3_per_2 { get; set; }
		public decimal? useful_release_ownprod_needs_p3_per_2 { get; set; }
		public decimal? useful_release_household_needs_p3_per_2 { get; set; }
		public decimal? tariff_weighted_invest_p3_per_2 { get; set; }
		public decimal? invest_component_p3_per_2 { get; set; }
		public decimal? tariff_tso_p3_per_2 { get; set; }
		public decimal? volume_he_tso_p3_per_2 { get; set; }
		public decimal? tariff_budgetcons_p3_per_2 { get; set; }
		public decimal? volume_he_budgetcons_p3_per_2 { get; set; }
		public decimal? tariff_public_p3_per_2 { get; set; }
		public decimal? volume_he_public_p3_per_2 { get; set; }
		public decimal? tariff_other_p3_per_2 { get; set; }
		public decimal? volume_he_other_p3_per_2 { get; set; } //(115)

		public decimal? tariff_econom_justified_p3_per_3 { get; set; }
		public decimal? volume_he_p3_per_3 { get; set; }
		public decimal? useful_release_ownprod_needs_p3_per_3 { get; set; }
		public decimal? useful_release_household_needs_p3_per_3 { get; set; }
		public decimal? tariff_weighted_invest_p3_per_3 { get; set; }
		public decimal? invest_component_p3_per_3 { get; set; }
		public decimal? tariff_tso_p3_per_3 { get; set; }
		public decimal? volume_he_tso_p3_per_3 { get; set; }
		public decimal? tariff_budgetcons_p3_per_3 { get; set; }
		public decimal? volume_he_budgetcons_p3_per_3 { get; set; }
		public decimal? tariff_public_p3_per_3 { get; set; }
		public decimal? volume_he_public_p3_per_3 { get; set; }
		public decimal? tariff_other_p3_per_3 { get; set; }
		public decimal? volume_he_other_p3_per_3 { get; set; } //(129)



		public decimal? tariff_econom_justified_p4_per_1 { get; set; }
		public decimal? volume_he_p4_per_1 { get; set; }
		public decimal? useful_release_ownprod_needs_p4_per_1 { get; set; }
		public decimal? useful_release_household_needs_p4_per_1 { get; set; }
		public decimal? tariff_weighted_invest_p4_per_1 { get; set; }
		public decimal? invest_component_p4_per_1 { get; set; }
		public decimal? tariff_tso_p4_per_1 { get; set; }
		public decimal? volume_he_tso_p4_per_1 { get; set; }
		public decimal? tariff_budgetcons_p4_per_1 { get; set; }
		public decimal? volume_he_budgetcons_p4_per_1 { get; set; }
		public decimal? tariff_public_p4_per_1 { get; set; }
		public decimal? volume_he_public_p4_per_1 { get; set; }
		public decimal? tariff_other_p4_per_1 { get; set; }
		public decimal? volume_he_other_p4_per_1 { get; set; } //(143)

		public decimal? tariff_econom_justified_p4_per_2 { get; set; }
		public decimal? volume_he_p4_per_2 { get; set; }
		public decimal? useful_release_ownprod_needs_p4_per_2 { get; set; }
		public decimal? useful_release_household_needs_p4_per_2 { get; set; }
		public decimal? tariff_weighted_invest_p4_per_2 { get; set; }
		public decimal? invest_component_p4_per_2 { get; set; }
		public decimal? tariff_tso_p4_per_2 { get; set; }
		public decimal? volume_he_tso_p4_per_2 { get; set; }
		public decimal? tariff_budgetcons_p4_per_2 { get; set; }
		public decimal? volume_he_budgetcons_p4_per_2 { get; set; }
		public decimal? tariff_public_p4_per_2 { get; set; }
		public decimal? volume_he_public_p4_per_2 { get; set; }
		public decimal? tariff_other_p4_per_2 { get; set; }
		public decimal? volume_he_other_p4_per_2 { get; set; } //(157)

		public decimal? tariff_econom_justified_p4_per_3 { get; set; }
		public decimal? volume_he_p4_per_3 { get; set; }
		public decimal? useful_release_ownprod_needs_p4_per_3 { get; set; }
		public decimal? useful_release_household_needs_p4_per_3 { get; set; }
		public decimal? tariff_weighted_invest_p4_per_3 { get; set; }
		public decimal? invest_component_p4_per_3 { get; set; }
		public decimal? tariff_tso_p4_per_3 { get; set; }
		public decimal? volume_he_tso_p4_per_3 { get; set; }
		public decimal? tariff_budgetcons_p4_per_3 { get; set; }
		public decimal? volume_he_budgetcons_p4_per_3 { get; set; }
		public decimal? tariff_public_p4_per_3 { get; set; }
		public decimal? volume_he_public_p4_per_3 { get; set; }
		public decimal? tariff_other_p4_per_3 { get; set; }
		public decimal? volume_he_other_p4_per_3 { get; set; } //(171)
	}

	#endregion

	#region TwoStageTariff


	/// <summary>
	/// Тариф на тепловую энергию в горячей воде, руб./Гкал без НДС. Двухставочный тариф, без НДС
	/// </summary>
	[Keyless]
	public class TwoStageTariffUnit
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }

		/// <summary>
		/// Перевод в одноставочный тариф. Бюджетные потребители. Первое полугодие
		/// </summary>
		public decimal? convers_twostage_to_singlestage_budget_per_1 { get; set; }

		/// <summary>
		/// Ставка за тепловую энергию. Бюджетные потребители. Первое полугодие
		/// </summary>
		public decimal? heatenergy_doublerate_rate_budget_per_1 { get; set; }

		/// <summary>
		/// Объем тепловой энергии (при двухставочном тарифе), Гкал. Бюджетные потребители. Первое полугодие
		/// </summary>
		public decimal? heatenergy_doublerate_volume_budget_per_1 { get; set; }

		/// <summary>
		/// Ставка за тепловую нагрузку (мощность). Бюджетные потребители. Первое полугодие
		/// </summary>
		public decimal? heatload_doublerate_rate_budget_per_1 { get; set; }

		/// <summary>
		/// Величина тепловой нагрузки, Гкал/ч. Бюджетные потребители. Первое полугодие
		/// </summary>
		public decimal? heatload_doublerate_value_budget_per_1 { get; set; }

		/// <summary>
		/// Объём дотаций из всех уровней бюджета. Бюджетные потребители. Первое полугодие
		/// </summary>
		public decimal? amount_grants_budget_per_1 { get; set; }

		/// <summary>
		/// Удельная величина дотаций. Бюджетные потребители. Первое полугодие
		/// </summary>
		public decimal? specific_value_grants_budget_per_1 { get; set; }


		/// <summary>
		/// Перевод в одноставочный тариф. Бюджетные потребители. Второе полугодие
		/// </summary>
		public decimal? convers_twostage_to_singlestage_budget_per_2 { get; set; }

		/// <summary>
		/// Ставка за тепловую энергию. Бюджетные потребители. Второе полугодие
		/// </summary>
		public decimal? heatenergy_doublerate_rate_budget_per_2 { get; set; }

		/// <summary>
		/// Объем тепловой энергии (при двухставочном тарифе), Гкал. Бюджетные потребители. Второе полугодие
		/// </summary>
		public decimal? heatenergy_doublerate_volume_budget_per_2 { get; set; }

		/// <summary>
		/// Ставка за тепловую нагрузку (мощность). Бюджетные потребители. Второе полугодие
		/// </summary>
		public decimal? heatload_doublerate_rate_budget_per_2 { get; set; }

		/// <summary>
		/// Величина тепловой нагрузки, Гкал/ч. Бюджетные потребители. Второе полугодие
		/// </summary>
		public decimal? heatload_doublerate_value_budget_per_2 { get; set; }

		/// <summary>
		/// Объём дотаций из всех уровней бюджета. Бюджетные потребители. Второе полугодие
		/// </summary>
		public decimal? amount_grants_budget_per_2 { get; set; }

		/// <summary>
		/// Удельная величина дотаций. Бюджетные потребители. Второе полугодие
		/// </summary>
		public decimal? specific_value_grants_budget_per_2 { get; set; }



		/// <summary>
		/// Перевод в одноставочный тариф. Бюджетные потребители. Весь год
		/// </summary>
		public decimal? convers_twostage_to_singlestage_budget_per_3 { get; set; }

		/// <summary>
		/// Ставка за тепловую энергию. Бюджетные потребители. Весь год
		/// </summary>
		public decimal? heatenergy_doublerate_rate_budget_per_3 { get; set; }

		/// <summary>
		/// Объем тепловой энергии (при двухставочном тарифе), Гкал. Бюджетные потребители. Весь год
		/// </summary>
		public decimal? heatenergy_doublerate_volume_budget_per_3 { get; set; }

		/// <summary>
		/// Ставка за тепловую нагрузку (мощность). Бюджетные потребители. Весь год
		/// </summary>
		public decimal? heatload_doublerate_rate_budget_per_3 { get; set; }

		/// <summary>
		/// Величина тепловой нагрузки, Гкал/ч. Бюджетные потребители. Весь год
		/// </summary>
		public decimal? heatload_doublerate_value_budget_per_3 { get; set; }

		/// <summary>
		/// Объём дотаций из всех уровней бюджета. Бюджетные потребители. Весь год
		/// </summary>
		public decimal? amount_grants_budget_per_3 { get; set; }

		/// <summary>
		/// Удельная величина дотаций. Бюджетные потребители. Весь год
		/// </summary>
		public decimal? specific_value_grants_budget_per_3 { get; set; }







		/// <summary>
		/// Перевод в одноставочный тариф. Население. Первое полугодие
		/// </summary>
		public decimal? convers_twostage_to_singlestage_public_per_1 { get; set; }

		/// <summary>
		/// Ставка за тепловую энергию. Население. Первое полугодие
		/// </summary>
		public decimal? heatenergy_doublerate_rate_public_per_1 { get; set; }

		/// <summary>
		/// Объем тепловой энергии (при двухставочном тарифе), Гкал. Население. Первое полугодие
		/// </summary>
		public decimal? heatenergy_doublerate_volume_public_per_1 { get; set; }

		/// <summary>
		/// Ставка за тепловую нагрузку (мощность). Население. Первое полугодие
		/// </summary>
		public decimal? heatload_doublerate_rate_public_per_1 { get; set; }

		/// <summary>
		/// Величина тепловой нагрузки, Гкал/ч. Население. Первое полугодие
		/// </summary>
		public decimal? heatload_doublerate_value_public_per_1 { get; set; }

		/// <summary>
		/// Объём дотаций из всех уровней бюджета. Население. Первое полугодие
		/// </summary>
		public decimal? amount_grants_public_per_1 { get; set; }

		/// <summary>
		/// Удельная величина дотаций. Население. Первое полугодие
		/// </summary>
		public decimal? specific_value_grants_public_per_1 { get; set; }


		/// <summary>
		/// Перевод в одноставочный тариф. Население. Второе полугодие
		/// </summary>
		public decimal? convers_twostage_to_singlestage_public_per_2 { get; set; }

		/// <summary>
		/// Ставка за тепловую энергию. Население. Второе полугодие
		/// </summary>
		public decimal? heatenergy_doublerate_rate_public_per_2 { get; set; }

		/// <summary>
		/// Объем тепловой энергии (при двухставочном тарифе), Гкал. Население. Второе полугодие
		/// </summary>
		public decimal? heatenergy_doublerate_volume_public_per_2 { get; set; }

		/// <summary>
		/// Ставка за тепловую нагрузку (мощность). Население. Второе полугодие
		/// </summary>
		public decimal? heatload_doublerate_rate_public_per_2 { get; set; }

		/// <summary>
		/// Величина тепловой нагрузки, Гкал/ч. Население. Второе полугодие
		/// </summary>
		public decimal? heatload_doublerate_value_public_per_2 { get; set; }

		/// <summary>
		/// Объём дотаций из всех уровней бюджета. Население. Второе полугодие
		/// </summary>
		public decimal? amount_grants_public_per_2 { get; set; }

		/// <summary>
		/// Удельная величина дотаций. Население. Второе полугодие
		/// </summary>
		public decimal? specific_value_grants_public_per_2 { get; set; }



		/// <summary>
		/// Перевод в одноставочный тариф. Население. Весь год
		/// </summary>
		public decimal? convers_twostage_to_singlestage_public_per_3 { get; set; }

		/// <summary>
		/// Ставка за тепловую энергию. Население. Весь год
		/// </summary>
		public decimal? heatenergy_doublerate_rate_public_per_3 { get; set; }

		/// <summary>
		/// Объем тепловой энергии (при двухставочном тарифе), Гкал. Население. Весь год
		/// </summary>
		public decimal? heatenergy_doublerate_volume_public_per_3 { get; set; }

		/// <summary>
		/// Ставка за тепловую нагрузку (мощность). Население. Весь год
		/// </summary>
		public decimal? heatload_doublerate_rate_public_per_3 { get; set; }

		/// <summary>
		/// Величина тепловой нагрузки, Гкал/ч. Население. Весь год
		/// </summary>
		public decimal? heatload_doublerate_value_public_per_3 { get; set; }

		/// <summary>
		/// Объём дотаций из всех уровней бюджета. Население. Весь год
		/// </summary>
		public decimal? amount_grants_public_per_3 { get; set; }

		/// <summary>
		/// Удельная величина дотаций. Население. Весь год
		/// </summary>
		public decimal? specific_value_grants_public_per_3 { get; set; }






		/// <summary>
		/// Перевод в одноставочный тариф. Прочие. Первое полугодие
		/// </summary>
		public decimal? convers_twostage_to_singlestage_other_per_1 { get; set; }

		/// <summary>
		/// Ставка за тепловую энергию. Прочие. Первое полугодие
		/// </summary>
		public decimal? heatenergy_doublerate_rate_other_per_1 { get; set; }

		/// <summary>
		/// Объем тепловой энергии (при двухставочном тарифе), Гкал. Прочие. Первое полугодие
		/// </summary>
		public decimal? heatenergy_doublerate_volume_other_per_1 { get; set; }

		/// <summary>
		/// Ставка за тепловую нагрузку (мощность). Прочие. Первое полугодие
		/// </summary>
		public decimal? heatload_doublerate_rate_other_per_1 { get; set; }

		/// <summary>
		/// Величина тепловой нагрузки, Гкал/ч. Прочие. Первое полугодие
		/// </summary>
		public decimal? heatload_doublerate_value_other_per_1 { get; set; }

		/// <summary>
		/// Объём дотаций из всех уровней бюджета. Прочие. Первое полугодие
		/// </summary>
		public decimal? amount_grants_other_per_1 { get; set; }

		/// <summary>
		/// Удельная величина дотаций. Прочие. Первое полугодие
		/// </summary>
		public decimal? specific_value_grants_other_per_1 { get; set; }


		/// <summary>
		/// Перевод в одноставочный тариф. Прочие. Второе полугодие
		/// </summary>
		public decimal? convers_twostage_to_singlestage_other_per_2 { get; set; }

		/// <summary>
		/// Ставка за тепловую энергию. Прочие. Второе полугодие
		/// </summary>
		public decimal? heatenergy_doublerate_rate_other_per_2 { get; set; }

		/// <summary>
		/// Объем тепловой энергии (при двухставочном тарифе), Гкал. Прочие. Второе полугодие
		/// </summary>
		public decimal? heatenergy_doublerate_volume_other_per_2 { get; set; }

		/// <summary>
		/// Ставка за тепловую нагрузку (мощность). Прочие. Второе полугодие
		/// </summary>
		public decimal? heatload_doublerate_rate_other_per_2 { get; set; }

		/// <summary>
		/// Величина тепловой нагрузки, Гкал/ч. Прочие. Второе полугодие
		/// </summary>
		public decimal? heatload_doublerate_value_other_per_2 { get; set; }

		/// <summary>
		/// Объём дотаций из всех уровней бюджета. Прочие. Второе полугодие
		/// </summary>
		public decimal? amount_grants_other_per_2 { get; set; }

		/// <summary>
		/// Удельная величина дотаций. Прочие. Второе полугодие
		/// </summary>
		public decimal? specific_value_grants_other_per_2 { get; set; }



		/// <summary>
		/// Перевод в одноставочный тариф. Прочие. Весь год
		/// </summary>
		public decimal? convers_twostage_to_singlestage_other_per_3 { get; set; }

		/// <summary>
		/// Ставка за тепловую энергию. Прочие. Весь год
		/// </summary>
		public decimal? heatenergy_doublerate_rate_other_per_3 { get; set; }

		/// <summary>
		/// Объем тепловой энергии (при двухставочном тарифе), Гкал. Прочие. Весь год
		/// </summary>
		public decimal? heatenergy_doublerate_volume_other_per_3 { get; set; }

		/// <summary>
		/// Ставка за тепловую нагрузку (мощность). Прочие. Весь год
		/// </summary>
		public decimal? heatload_doublerate_rate_other_per_3 { get; set; }

		/// <summary>
		/// Величина тепловой нагрузки, Гкал/ч. Прочие. Весь год
		/// </summary>
		public decimal? heatload_doublerate_value_other_per_3 { get; set; }

		/// <summary>
		/// Объём дотаций из всех уровней бюджета. Прочие. Весь год
		/// </summary>
		public decimal? amount_grants_other_per_3 { get; set; }

		/// <summary>
		/// Удельная величина дотаций. Прочие. Весь год
		/// </summary>
		public decimal? specific_value_grants_other_per_3 { get; set; }


		//           Скрытые поля.
		/// <summary>
		/// Полезный отпуск бюджетным потребителям (план), Гкал. Первое полугодие
		/// </summary>
		public decimal? twostage_useful_release_buget_cons_plan_per_1 { get; set; }

		/// <summary>
		/// Полезный отпуск населению (план), Гкал. Первое полугодие
		/// </summary>
		public decimal? twostage_useful_release_public_plan_per_1 { get; set; }

		/// <summary>
		/// Полезный отпуск прочим потребителям (план), Гкал. Первое полугодие
		/// </summary>
		public decimal? twostage_useful_release_other_cons_plan_per_1 { get; set; }

		/// <summary>
		/// Полезный отпуск бюджетным потребителям (план), Гкал. Второе полугодие
		/// </summary>
		public decimal? twostage_useful_release_buget_cons_plan_per_2 { get; set; }

		/// <summary>
		/// Полезный отпуск населению (план), Гкал. Второе полугодие
		/// </summary>
		public decimal? twostage_useful_release_public_plan_per_2 { get; set; }

		/// <summary>
		/// Полезный отпуск прочим потребителям (план), Гкал. Второе полугодие
		/// </summary>
		public decimal? twostage_useful_release_other_cons_plan_per_2 { get; set; }
	}

	#endregion

	#region SingleStageTariff
	/// <summary>
	/// Тариф на тепловую энергию в горячей воде, руб./Гкал без НДС. Одноставочный тариф, без НДС
	/// </summary>
	[Keyless]
	public class SingleStageTariffUnit
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }

		/// <summary>
		/// ТСО-перепродавцы. Первое полугодие
		/// </summary>
		public decimal? tariff_tso_per_1 { get; set; }

		/// <summary>
		/// Бюджетные потребители. Первое полугодие
		/// </summary>
		public decimal? tariff_budgetcons_per_1 { get; set; }

		/// <summary>
		/// Население. Первое полугодие
		/// </summary>
		public decimal? tariff_public_per_1 { get; set; }

		/// <summary>
		/// Прочие. Первое полугодие
		/// </summary>
		public decimal? tariff_other_per_1 { get; set; }

		/// <summary>
		/// ТСО-перепродавцы. Второе полугодие
		/// </summary>
		public decimal? tariff_tso_per_2 { get; set; }

		/// <summary>
		/// Бюджетные потребители. Второе полугодие
		/// </summary>
		public decimal? tariff_budgetcons_per_2 { get; set; }

		/// <summary>
		/// Население. Второе полугодие
		/// </summary>
		public decimal? tariff_public_per_2 { get; set; }

		/// <summary>
		/// Прочие. Второе полугодие
		/// </summary>
		public decimal? tariff_other_per_2 { get; set; }

		/// <summary>
		/// ТСО-перепродавцы. Весь год
		/// </summary>
		public decimal? tariff_tso_per_3 { get; set; }

		/// <summary>
		/// Бюджетные потребители. Весь год
		/// </summary>
		public decimal? tariff_budgetcons_per_3 { get; set; }

		/// <summary>
		/// Население. Весь год
		/// </summary>
		public decimal? tariff_public_per_3 { get; set; }

		/// <summary>
		/// Прочие. Весь год
		/// </summary>
		public decimal? tariff_other_per_3 { get; set; }


		//           Скрытые поля.
		/// <summary>
		/// Полезный отпуск ТСО-перепродавцам (план), Гкал. Первое полугодие
		/// </summary>
		public decimal? useful_release_tso_salers_plan_per_1 { get; set; }

		/// <summary>
		/// Полезный отпуск бюджетным потребителям (план), Гкал. Первое полугодие
		/// </summary>
		public decimal? useful_release_buget_cons_plan_per_1 { get; set; }

		/// <summary>
		/// Полезный отпуск населению (план), Гкал. Первое полугодие
		/// </summary>
		public decimal? useful_release_public_plan_per_1 { get; set; }

		/// <summary>
		/// Полезный отпуск прочим потребителям (план), Гкал. Первое полугодие
		/// </summary>
		public decimal? useful_release_other_cons_plan_per_1 { get; set; }

		/// <summary>
		/// Полезный отпуск ТСО-перепродавцам (план), Гкал. Второе полугодие
		/// </summary>
		public decimal? useful_release_tso_salers_plan_per_2 { get; set; }

		/// <summary>
		/// Полезный отпуск бюджетным потребителям (план), Гкал. Второе полугодие
		/// </summary>
		public decimal? useful_release_buget_cons_plan_per_2 { get; set; }

		/// <summary>
		/// Полезный отпуск населению (план), Гкал. Второе полугодие
		/// </summary>
		public decimal? useful_release_public_plan_per_2 { get; set; }

		/// <summary>
		/// Полезный отпуск прочим потребителям (план), Гкал. Второе полугодие
		/// </summary>
		public decimal? useful_release_other_cons_plan_per_2 { get; set; }
	}

	#endregion

	#region HeatEnergyTariffWater
	/// <summary>
	/// Тариф на тепловую энергию в горячей воде
	/// </summary>
	[Keyless]
	public class HeatEnergyTariffWater
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }

		/// <summary>
		/// Экономически обоснованный тариф. Первое полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_econom_justified_per_1 { get; set; }

		/// <summary>
		/// Средневзвешенный тариф с учётом инвестиционной составляющей. Первое полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_weighted_invest_per_1 { get; set; }

		/// <summary>
		/// Инвестиционная составляющая. Первое полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_invest_component_per_1 { get; set; }

		/// <summary>
		/// Экономически обоснованный тариф. Второе полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_econom_justified_per_2 { get; set; }

		/// <summary>
		/// Средневзвешенный тариф с учётом инвестиционной составляющей. Второе полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_weighted_invest_per_2 { get; set; }

		/// <summary>
		/// Инвестиционная составляющая. Второе полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_invest_component_per_2 { get; set; }

		/// <summary>
		/// Экономически обоснованный тариф. Весь год
		/// </summary>
		public decimal? heat_energy_tariff_water_econom_justified_per_3 { get; set; }

		/// <summary>
		/// Средневзвешенный тариф с учётом инвестиционной составляющей. Весь год
		/// </summary>
		public decimal? heat_energy_tariff_water_weighted_invest_per_3 { get; set; }

		/// <summary>
		/// Инвестиционная составляющая. Весь год
		/// </summary>
		public decimal? heat_energy_tariff_water_invest_component_per_3 { get; set; }

		//           Скрытые поля.
		/// <summary>
		/// Полезный отпуск ТСО-перепродавцам (план), Гкал. Первое полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_useful_release_tso_salers_plan_per_1 { get; set; }

		/// <summary>
		/// Полезный отпуск бюджетным потребителям (план), Гкал. Первое полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_useful_release_buget_cons_plan_per_1 { get; set; }

		/// <summary>
		/// Полезный отпуск населению (план), Гкал. Первое полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_useful_release_public_plan_per_1 { get; set; }

		/// <summary>
		/// Полезный отпуск прочим потребителям (план), Гкал. Первое полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_useful_release_other_cons_plan_per_1 { get; set; }

		/// <summary>
		/// Полезный отпуск ТСО-перепродавцам (план), Гкал. Второе полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_useful_release_tso_salers_plan_per_2 { get; set; }

		/// <summary>
		/// Полезный отпуск бюджетным потребителям (план), Гкал. Второе полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_useful_release_buget_cons_plan_per_2 { get; set; }

		/// <summary>
		/// Полезный отпуск населению (план), Гкал. Второе полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_useful_release_public_plan_per_2 { get; set; }

		/// <summary>
		/// Полезный отпуск прочим потребителям (план), Гкал. Второе полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_useful_release_other_cons_plan_per_2 { get; set; }

		/// <summary>
		/// Полезный отпуск населению (план), Гкал. Первое полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_convers_doublerate_to_singlerate_budjet_per_1 { get; set; }

		/// <summary>
		/// Формула 2. Компонент. Первое полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_volume_heat_energy_budjet_per_1 { get; set; }

		/// <summary>
		/// Формула 2. Компонент. Первое полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_convers_doublerate_to_singlerate_public_per_1 { get; set; }

		/// <summary>
		/// Формула 2. Компонент. Первое полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_volume_heat_energy_public_per_1 { get; set; }

		/// <summary>
		/// Формула 2. Компонент. Первое полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_convers_doublerate_to_singlerate_other_per_1 { get; set; }

		/// <summary>
		/// Формула 2. Компонент. Первое полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_volume_heat_energy_other_per_1 { get; set; }

		/// <summary>
		/// Формула 2. Компонент. Второе полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_convers_doublerate_to_singlerate_budjet_per_2 { get; set; }

		/// <summary>
		/// Формула 2. Компонент. Второе полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_volume_heat_energy_budjet_per_2 { get; set; }

		/// <summary>
		/// Формула 2. Компонент. Второе полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_convers_doublerate_to_singlerate_public_per_2 { get; set; }

		/// <summary>
		/// Формула 2. Компонент. Второе полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_volume_heat_energy_public_per_2 { get; set; }

		/// <summary>
		/// Формула 2. Компонент. Второе полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_convers_doublerate_to_singlerate_other_per_2 { get; set; }

		/// <summary>
		/// Формула 2. Компонент. Второе полугодие
		/// </summary>
		public decimal? heat_energy_tariff_water_volume_heat_energy_other_per_2 { get; set; }
	}

	#endregion

	#region CrossSubsidizationAmount
	/// <summary>
	/// Объем перекрестного субсидирования
	/// </summary>
	[Keyless]
	public class CrossSubsidizationAmount 
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }

		/// <summary>
		/// ТСО-перепродавцы. Первое полугодие
		/// </summary>
		public decimal? cross_subsidization_amount_tariff_tso_per_1 { get; set; }

		/// <summary>
		/// Бюджетные потребители. Первое полугодие
		/// </summary>
		public decimal? cross_subsidization_amount_tariff_budgetcons_per_1 { get; set; }

		/// <summary>
		/// Население. Первое полугодие
		/// </summary>
		public decimal? cross_subsidization_amount_tariff_public_per_1 { get; set; }

		/// <summary>
		/// Прочие потребители. Первое полугодие
		/// </summary>
		public decimal? cross_subsidization_amount_tariff_other_per_1 { get; set; }



		/// <summary>
		/// ТСО-перепродавцы. Второе полугодие
		/// </summary>
		public decimal? cross_subsidization_amount_tariff_tso_per_2 { get; set; }

		/// <summary>
		/// Бюджетные потребители. Второе полугодие
		/// </summary>
		public decimal? cross_subsidization_amount_tariff_budgetcons_per_2 { get; set; }

		/// <summary>
		/// Население. Второе полугодие
		/// </summary>
		public decimal? cross_subsidization_amount_tariff_public_per_2 { get; set; }

		/// <summary>
		/// Прочие потребители. Второе полугодие
		/// </summary>
		public decimal? cross_subsidization_amount_tariff_other_per_2 { get; set; }



		/// <summary>
		/// ТСО-перепродавцы. Весь год
		/// </summary>
		public decimal? cross_subsidization_amount_tariff_tso_per_3 { get; set; }

		/// <summary>
		/// Бюджетные потребители. Весь год
		/// </summary>
		public decimal? cross_subsidization_amount_tariff_budgetcons_per_3 { get; set; }

		/// <summary>
		/// Население. Весь год
		/// </summary>
		public decimal? cross_subsidization_amount_tariff_public_per_3 { get; set; }

		/// <summary>
		/// Прочие потребители. Весь год
		/// </summary>
		public decimal? cross_subsidization_amount_tariff_other_per_3 { get; set; }
	}
	#endregion

	#region HeatEnergyTariffSteamHotWater

	/// <summary>
	/// Тариф на теплоноситель (горячая вода / пар) и тарифы на горячую воду, без НДС
	/// </summary>
	[Keyless]
	public class HeatEnergyTariffSteamHotWater
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }

		/// <summary>
		/// Тариф на теплоноситель (горячая вода), руб./куб.м без НДС. Первое полугодие
		/// </summary>
		public decimal? hot_water_heat_carrier_tariff_per_1 { get; set; }

		/// <summary>
		/// Тариф на теплоноситель (пар), руб./куб.м без НДС. Первое полугодие
		/// </summary>
		public decimal? steam_heat_carrier_tariff_per_1 { get; set; }

		/// <summary>
		/// Однокомпонентный тариф на горячую воду, руб./куб.м без НДС. Первое полугодие
		/// </summary>
		public decimal? hot_water_tariff_per_1 { get; set; }

		/// <summary>
		/// Удельный расход тепловой энергии на подогрев исходной воды, Гкал/куб.м. Первое полугодие
		/// </summary>
		public decimal? heat_energy_specific_consumption_per_1 { get; set; }

		/// <summary>
		/// Компонент на теплоноситель. Первое полугодие
		/// </summary>
		public decimal? heat_carrier_component_per_1 { get; set; }

		/// <summary>
		/// Компонент на тепловую энергию. Первое полугодие
		/// </summary>
		public decimal? heat_energy_component_per_1 { get; set; }

		/// <summary>
		/// Тариф на теплоноситель (горячая вода), руб./куб.м без НДС. Второе полугодие
		/// </summary>
		public decimal? hot_water_heat_carrier_tariff_per_2 { get; set; }

		/// <summary>
		/// Тариф на теплоноситель (пар), руб./куб.м без НДС. Второе полугодие
		/// </summary>
		public decimal? steam_heat_carrier_tariff_per_2 { get; set; }

		/// <summary>
		/// Однокомпонентный тариф на горячую воду, руб./куб.м. без НДС. Второе полугодие
		/// </summary>
		public decimal? hot_water_tariff_per_2 { get; set; }

		/// <summary>
		/// Удельный расход тепловой энергии на подогрев исходной воды, Гкал/куб.м. Второе полугодие
		/// </summary>
		public decimal? heat_energy_specific_consumption_per_2 { get; set; }

		/// <summary>
		/// Компонент на теплоноситель. Второе полугодие
		/// </summary>
		public decimal? heat_carrier_component_per_2 { get; set; }

		/// <summary>
		/// Компонент на тепловую энергию. Второе полугодие
		/// </summary>
		public decimal? heat_energy_component_per_2 { get; set; }

		/// <summary>
		/// Тариф на теплоноситель (горячая вода), руб./куб.м без НДС. Весь год
		/// </summary>
		public decimal? hot_water_heat_carrier_tariff_per_3 { get; set; }

		/// <summary>
		/// Тариф на теплоноситель (пар), руб./куб.м без НДС. Весь год
		/// </summary>
		public decimal? steam_heat_carrier_tariff_per_3 { get; set; }

		/// <summary>
		/// Однокомпонентный тариф на горячую воду, руб./куб.м. без НДС. Весь год
		/// </summary>
		public decimal? hot_water_tariff_per_3 { get; set; }

		/// <summary>
		/// Удельный расход тепловой энергии на подогрев исходной воды, Гкал/куб.м. Весь год
		/// </summary>
		public decimal? heat_energy_specific_consumption_per_3 { get; set; }

		/// <summary>
		/// Компонент на теплоноситель. Весь год
		/// </summary>
		public decimal? heat_carrier_component_per_3 { get; set; }

		/// <summary>
		/// Компонент на тепловую энергию. Весь год
		/// </summary>
		public decimal? heat_energy_component_per_3 { get; set; }


		//           Скрытые поля.
		/// <summary>
		/// Полезный отпуск бюджетным потребителям (план), Гкал. Первое полугодие
		/// </summary>
		public decimal? he_tariff_steam_hotwater_useful_release_buget_cons_plan_per_1 { get; set; }

		/// <summary>
		/// Полезный отпуск населению (план), Гкал. Первое полугодие
		/// </summary>
		public decimal? he_tariff_steam_hotwater_useful_release_public_cons_plan_per_1 { get; set; }

		/// <summary>
		/// Полезный отпуск прочим потребителям (план), Гкал. Первое полугодие
		/// </summary>
		public decimal? he_tariff_steam_hotwater_useful_release_other_cons_plan_per_1 { get; set; }

		/// <summary>
		/// Полезный отпуск бюджетным потребителям (план), Гкал. Второе полугодие
		/// </summary>
		public decimal? he_tariff_steam_hotwater_useful_release_buget_cons_plan_per_2 { get; set; }

		/// <summary>
		/// Полезный отпуск населению (план), Гкал. Второе полугодие
		/// </summary>
		public decimal? he_tariff_steam_hotwater_useful_release_public_cons_plan_per_2 { get; set; }

		/// <summary>
		/// Полезный отпуск прочим потребителям (план), Гкал. Второе полугодие
		/// </summary>
		public decimal? he_tariff_steam_hotwater_useful_release_other_cons_plan_per_2 { get; set; }

		/// <summary>
		/// Объем закупок теплоносителя. Первое полугодие
		/// </summary>
		public decimal? he_tariff_steam_hotwater_volume_buy_heat_carrier_plan_per_1 { get; set; }

		/// <summary>
		/// Объем закупок теплоносителя. Второе полугодие
		/// </summary>
		public decimal? he_tariff_steam_hotwater_volume_buy_heat_carrier_plan_per_2 { get; set; }

		/// <summary>
		/// Формула 39 (Весь год). Главный делитель. 
		/// </summary>
		public decimal? he_tariff_steam_hotwater_volume_buy_coldwater_plan_divider_main { get; set; }

		/// <summary>
		/// Формула 39 (Весь год). Второстепенный делитель.
		/// </summary>
		public decimal? he_tariff_steam_hotwater_volume_buy_coldwater_plan_subdivider { get; set; }

		/// <summary>
		/// Формула 39 (Первое полугодие). Параметр 3.3.96
		/// </summary>
		public decimal? he_tariff_steam_hotwater_price_coldwater_plan_div_per_1 { get; set; }

		/// <summary>
		/// Формула 39 (Второе полугодие). Параметр 3.3.96
		/// </summary>
		public decimal? he_tariff_steam_hotwater_price_coldwater_plan_div_per_2 { get; set; }

		/// <summary>
		/// Формула 39 (Первое полугодие). Параметр 3.8.436
		/// </summary>
		public decimal? he_tariff_steam_hotwater_tariff_other_per_1 { get; set; }

		/// <summary>
		/// Формула 39 (Второе полугодие). Параметр 3.8.436
		/// </summary>
		public decimal? he_tariff_steam_hotwater_tariff_other_per_2 { get; set; }
	}
	#endregion

	#region HeatEnergyTransfer
	/// <summary>
	/// Тариф на передачу тепловой энергии, руб./Гкал без НДС
	/// </summary>
	[Keyless]	
	public class HeatEnergyTransfer
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }

		/// <summary>
		/// Тариф на нужды организации
		/// </summary>
		public decimal? he_transfer_household_needs_tariff_per_1 { get; set; }

		/// <summary>
		/// Средневзвешенный тариф с учётом инвестиционной составляющей
		/// </summary>
		public decimal? he_transfer_tariff_weighted_invest_per_1 { get; set; }

		/// <summary>
		/// Инвестиционная составляющая
		/// </summary>
		public decimal? he_transfer_invest_component_per_1 { get; set; }

		/// <summary>
		/// ТСО-перепродавцы
		/// </summary>
		public decimal? he_transfer_tariff_tso_per_1 { get; set; }

		/// <summary>
		/// Бюджетные потребители
		/// </summary>
		public decimal? he_transfer_tariff_budgetcons_per_1 { get; set; }

		/// <summary>
		/// Население
		/// </summary>
		public decimal? he_transfer_tariff_public_per_1 { get; set; }

		/// <summary>
		/// Прочие потребители
		/// </summary>
		public decimal? he_transfer_tariff_other_per_1 { get; set; }




		/// <summary>
		/// Тариф на нужды организации
		/// </summary>
		public decimal? he_transfer_household_needs_tariff_per_2 { get; set; }

		/// <summary>
		/// Средневзвешенный тариф с учётом инвестиционной составляющей
		/// </summary>
		public decimal? he_transfer_tariff_weighted_invest_per_2 { get; set; }

		/// <summary>
		/// Инвестиционная составляющая
		/// </summary>
		public decimal? he_transfer_invest_component_per_2 { get; set; }

		/// <summary>
		/// ТСО-перепродавцы
		/// </summary>
		public decimal? he_transfer_tariff_tso_per_2 { get; set; }

		/// <summary>
		/// Бюджетные потребители
		/// </summary>
		public decimal? he_transfer_tariff_budgetcons_per_2 { get; set; }

		/// <summary>
		/// Население
		/// </summary>
		public decimal? he_transfer_tariff_public_per_2 { get; set; }

		/// <summary>
		/// Прочие потребители
		/// </summary>
		public decimal? he_transfer_tariff_other_per_2 { get; set; }




		/// <summary>
		/// Тариф на нужды организации
		/// </summary>
		public decimal? he_transfer_household_needs_tariff_per_3 { get; set; }

		/// <summary>
		/// Средневзвешенный тариф с учётом инвестиционной составляющей
		/// </summary>
		public decimal? he_transfer_tariff_weighted_invest_per_3 { get; set; }

		/// <summary>
		/// Инвестиционная составляющая
		/// </summary>
		public decimal? he_transfer_invest_component_per_3 { get; set; }

		/// <summary>
		/// ТСО-перепродавцы
		/// </summary>
		public decimal? he_transfer_tariff_tso_per_3 { get; set; }

		/// <summary>
		/// Бюджетные потребители
		/// </summary>
		public decimal? he_transfer_tariff_budgetcons_per_3 { get; set; }

		/// <summary>
		/// Население
		/// </summary>
		public decimal? he_transfer_tariff_public_per_3 { get; set; }

		/// <summary>
		/// Прочие потребители
		/// </summary>
		public decimal? he_transfer_tariff_other_per_3 { get; set; }


		//           Скрытые поля.

		/// <summary>
		/// Полезный отпуск на собственное производство предприятия (план), Гкал. Первое полугодие
		/// </summary>
		public decimal? he_transfer_useful_release_ownprod_plan_per_1 { get; set; }

		/// <summary>
		/// Полезный отпуск на хозяйственные нужды предприятия (план), Гкал. Первое полугодие
		/// </summary>
		public decimal? he_transfer_useful_release_household_needs_plan_per_1 { get; set; }

		/// <summary>
		/// Полезный отпуск населению (план), Гкал. Первое полугодие
		/// </summary>
		public decimal? he_transfer_useful_release_public_plan_per_1 { get; set; }

		/// <summary>
		/// Полезный отпуск прочим потребителям (план), Гкал. Первое полугодие
		/// </summary>
		public decimal? he_transfer_useful_release_other_cons_plan_per_1 { get; set; }


		/// <summary>
		/// Полезный отпуск ТСО-перепродавцам (план), Гкал. Первое полугодие
		/// </summary>
		public decimal? he_transfer_useful_release_tso_salers_plan_per_1 { get; set; }

		/// <summary>
		/// Полезный отпуск бюджетным потребителям (план), Гкал. Первое полугодие
		/// </summary>
		public decimal? he_transfer_useful_release_buget_cons_plan_per_1 { get; set; }

		/// <summary>
		/// Полезный отпуск на собственное производство предприятия (план), Гкал. Второе полугодие
		/// </summary>
		public decimal? he_transfer_useful_release_ownprod_plan_per_2 { get; set; }

		/// <summary>
		/// Полезный отпуск на хозяйственные нужды предприятия (план), Гкал. Второе полугодие
		/// </summary>
		public decimal? he_transfer_useful_release_household_needs_plan_per_2 { get; set; }

		/// <summary>
		/// Полезный отпуск ТСО-перепродавцам (план), Гкал. Второе полугодие
		/// </summary>
		public decimal? he_transfer_useful_release_tso_salers_plan_per_2 { get; set; }

		/// <summary>
		/// Полезный отпуск бюджетным потребителям (план), Гкал. Второе полугодие
		/// </summary>
		public decimal? he_transfer_useful_release_buget_cons_plan_per_2 { get; set; }

		/// <summary>
		/// Полезный отпуск населению (план), Гкал. Второе полугодие
		/// </summary>
		public decimal? he_transfer_useful_release_public_plan_per_2 { get; set; }

		/// <summary>
		/// Полезный отпуск прочим потребителям (план), Гкал. Второе полугодие
		/// </summary>
		public decimal? he_transfer_useful_release_other_cons_plan_per_2 { get; set; }
	}
	#endregion


	#region ETO_Amount_Preference

	/// <summary>
	/// Тариф ЕТО / Тариф с субсидир. / Льготный тариф, руб./Гкал без НДС
	/// </summary>
	[Keyless]
	public class ETO_Amount_Preference
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }

		/// <summary>
		/// Бюджетные потребители. Тариф с возмещением / компенсацией / субсидированием организации. Первое полугодие
		/// </summary>
		public decimal? tariff_compensation_budgetcons_per_1 { get; set; }

		/// <summary>
		/// Население. Тариф с возмещением / компенсацией / субсидированием организации. Первое полугодие
		/// </summary>
		public decimal? tariff_compensation_public_per_1 { get; set; }

		/// <summary>
		/// Прочие потребители. Тариф с возмещением / компенсацией / субсидированием организации. Первое полугодие
		/// </summary>
		public decimal? tariff_compensation_other_per_1 { get; set; }

		/// <summary>
		/// Бюджетные потребители. Тариф с возмещением / компенсацией / субсидированием организации. Второе полугодие
		/// </summary>
		public decimal? tariff_compensation_budgetcons_per_2 { get; set; }

		/// <summary>
		/// Население. Тариф с возмещением / компенсацией / субсидированием организации. Второе полугодие
		/// </summary>
		public decimal? tariff_compensation_public_per_2 { get; set; }

		/// <summary>
		/// Прочие потребители. Тариф с возмещением / компенсацией / субсидированием организации. Второе полугодие
		/// </summary>
		public decimal? tariff_compensation_other_per_2 { get; set; }

		/// <summary>
		/// Бюджетные потребители. Тариф с возмещением / компенсацией / субсидированием организации. Весь год
		/// </summary>
		public decimal? tariff_compensation_budgetcons_per_3 { get; set; }

		/// <summary>
		/// Население. Тариф с возмещением / компенсацией / субсидированием организации. Весь год
		/// </summary>
		public decimal? tariff_compensation_public_per_3 { get; set; }

		/// <summary>
		/// Прочие потребители. Тариф с возмещением / компенсацией / субсидированием организации. Весь год
		/// </summary>
		public decimal? tariff_compensation_other_per_3 { get; set; }



		/// <summary>
		/// Бюджетные потребители. Тариф ЕТО. Первое полугодие
		/// </summary>
		public decimal? tariff_eto_budgetcons_per_1 { get; set; }

		/// <summary>
		/// Население. Тариф ЕТО. Первое полугодие
		/// </summary>
		public decimal? tariff_eto_public_per_1 { get; set; }

		/// <summary>
		/// Прочие потребители. Тариф ЕТО. Первое полугодие
		/// </summary>
		public decimal? tariff_eto_other_per_1 { get; set; }

		/// <summary>
		/// Бюджетные потребители. Тариф ЕТО. Второе полугодие
		/// </summary>
		public decimal? tariff_eto_budgetcons_per_2 { get; set; }

		/// <summary>
		/// Население. Тариф ЕТО. Второе полугодие
		/// </summary>
		public decimal? tariff_eto_public_per_2 { get; set; }

		/// <summary>
		/// Прочие потребители. Тариф ЕТО. Второе полугодие
		/// </summary>
		public decimal? tariff_eto_other_per_2 { get; set; }

		/// <summary>
		/// Бюджетные потребители. Тариф ЕТО. Весь год
		/// </summary>
		public decimal? tariff_eto_budgetcons_per_3 { get; set; }

		/// <summary>
		/// Население. Тариф ЕТО. Весь год
		/// </summary>
		public decimal? tariff_eto_public_per_3 { get; set; }

		/// <summary>
		/// Прочие потребители. Тариф ЕТО. Весь год
		/// </summary>
		public decimal? tariff_eto_other_per_3 { get; set; }





		/// <summary>
		/// Бюджетные потребители. Льготный тариф. Первое полугодие
		/// </summary>
		public decimal? tariff_preferential_budgetcons_per_1 { get; set; }

		/// <summary>
		/// Население. Льготный тариф. Первое полугодие
		/// </summary>
		public decimal? tariff_preferential_public_per_1 { get; set; }

		/// <summary>
		/// Прочие потребители. Льготный тариф. Первое полугодие
		/// </summary>
		public decimal? tariff_preferential_other_per_1 { get; set; }

		/// <summary>
		/// Бюджетные потребители. Льготный тариф. Второе полугодие
		/// </summary>
		public decimal? tariff_preferential_budgetcons_per_2 { get; set; }

		/// <summary>
		/// Население. Льготный тариф. Второе полугодие
		/// </summary>
		public decimal? tariff_preferential_public_per_2 { get; set; }

		/// <summary>
		/// Прочие потребители. Льготный тариф. Второе полугодие
		/// </summary>
		public decimal? tariff_preferential_other_per_2 { get; set; }

		/// <summary>
		/// Бюджетные потребители. Льготный тариф. Весь год
		/// </summary>
		public decimal? tariff_preferential_budgetcons_per_3 { get; set; }

		/// <summary>
		/// Население. Льготный тариф. Весь год
		/// </summary>
		public decimal? tariff_preferential_public_per_3 { get; set; }

		/// <summary>
		/// Прочие потребители. Льготный тариф. Весь год
		/// </summary>
		public decimal? tariff_preferential_other_per_3 { get; set; }

		//           Скрытые поля.
		/// <summary>
		/// Полезный отпуск бюджетным потребителям (план), Гкал. Первое полугодие
		/// </summary>
		public decimal? eto_amount_preference_twostage_useful_release_buget_cons_plan_per_1 { get; set; }

		/// <summary>
		/// Полезный отпуск населению (план), Гкал. Первое полугодие
		/// </summary>
		public decimal? eto_amount_preference_twostage_useful_release_public_cons_plan_per_1 { get; set; }

		/// <summary>
		/// Полезный отпуск прочим потребителям (план), Гкал. Первое полугодие
		/// </summary>
		public decimal? eto_amount_preference_twostage_useful_release_other_cons_plan_per_1 { get; set; }

		/// <summary>
		/// Полезный отпуск бюджетным потребителям (план), Гкал. Второе полугодие
		/// </summary>
		public decimal? eto_amount_preference_twostage_useful_release_buget_cons_plan_per_2 { get; set; }

		/// <summary>
		/// Полезный отпуск населению (план), Гкал. Второе полугодие
		/// </summary>
		public decimal? eto_amount_preference_twostage_useful_release_public_cons_plan_per_2 { get; set; }

		/// <summary>
		/// Полезный отпуск прочим потребителям (план), Гкал. Второе полугодие
		/// </summary>
		public decimal? eto_amount_preference_twostage_useful_release_other_cons_plan_per_2 { get; set; }
	}

	#endregion

	#region TZOneCalcTariffDataViewModel
	[Keyless]
	public class TZHeaderTariffDataViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public string? tz_territory { get; set; }
		public string? combine_prod_more25 { get; set; }
		public string? combine_prod_less25 { get; set; }
		public string? not_combine_prod { get; set; }
		public string? transfer { get; set; }
		public string? sale { get; set; }
		public string? delivery_contract { get; set; }
		public short? tariff_differentiation { get; set; }
		public string? decision_date { get; set; }
		public string? decision_num { get; set; }
		public string? protocol_num { get; set; }
		public string? protocol_date { get; set; }
	}
	#endregion

	#region TZTariffTabModel
	[Keyless]
	public class TZTariffHotWaterTabModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }

		public string? unom_tz { get; set; } //(1)
		public string? territory { get; set; } //(2)
		public string? code_tso { get; set; } //(3)
		public string? short_name { get; set; } //(4)
		public string? activity_types { get; set; } //(5)
		public decimal? tariff_econom_justified_calc { get; set; } //(6)
		public decimal? service_sold_value { get; set; } //(7)
		public decimal? tariff_weighted_invest { get; set; } //(8)
		public decimal? investСomponent { get; set; } //(9)
		public decimal? transfer_org_needs_tariff { get; set; } //(10)

		public decimal? transfer_org_needs_volume { get; set; } //(11)
		public decimal? tso_single_tariff_transfer { get; set; } //(12)
		public decimal? tso_tariff_transfer { get; set; } //(13)
		public decimal? tso_sales_service_volume { get; set; } //(14)
		public decimal? budjet_single_tariff_transfer { get; set; } //(15)
		public decimal? budjet_tariff_transfer { get; set; } //(16)
		public decimal? budjet_sales_service_volume { get; set; } //(17)
		public decimal? doublerate_convers_doublerate_to_singlerate { get; set; } //(18)
		public decimal? budjet_heat_energy_doublerate { get; set; } //(19)
		public decimal? volume_he_budjet_doublerate { get; set; } //(20)

		public decimal? budjet_heat_load_doublerate { get; set; } //(21)
		public decimal? volume_heat_load_budget_doublerate { get; set; } //(22)
		public decimal? amount_grants_budget { get; set; } //(23)
		public decimal? subsidy_spec_value_budjet { get; set; }//(24)
		public decimal? public_single_tariff_transfer { get; set; } //(25)
		public decimal? public_tariff_transfer { get; set; } //(26)
		public decimal? public_plan_transfer { get; set; } //(27)
		public decimal? doublerate_convers_doublerate_to_singlerate_public { get; set; } //(28)
		public decimal? double_heat_energy_doublerate_public { get; set; } //(29)
		public decimal? volume_he_public_doublerate { get; set; } //(30)

		public decimal? double_heat_load_doublerate_public { get; set; } //(31)
		public decimal? volume_heat_load_public_doublerate { get; set; } //(32)
		public decimal? amount_grants_public { get; set; } //(33)
		public decimal? subsidy_spec_value_public { get; set; } //(34)
		public decimal? other_single_tariff { get; set; } //(35)
		public decimal? other_tariff { get; set; } //(36)
		public decimal? volume_he_other { get; set; } //(37)
		public decimal? doublerate_convers_doublerate_to_singlerate_other { get; set; } //(38)
		public decimal? doublerate_heat_energy_rate_other { get; set; } //(39)
		public decimal? volume_he_other_doublerate { get; set; } //(40)

		public decimal? doublerate_heat_load_rate_other { get; set; } //(41)
		public decimal? volume_heat_load_other_doublerate { get; set; } //(42)
		public decimal? amount_grants_other { get; set; } //(43)
		public decimal? subsidy_spec_value_other { get; set; } //(44)
		public decimal? cross_subsidy_volume { get; set; } //(45)
		public decimal? tariff_eto_budjet { get; set; } //(46)
		public decimal? tariff_eto_public { get; set; } //(47)
		public decimal? tariff_eto_other { get; set; } //(48)
		public decimal? tariff_compensation_budjet { get; set; } //(49)
		public decimal? tariff_compensation_public { get; set; } //(50)

		public decimal? tariff_compensation_other { get; set; } //(51)
		public decimal? tariff_preferential_budjet { get; set; } //(52)
		public decimal? tariff_preferential_public { get; set; } //(53)
		public decimal? tariff_preferential_other { get; set; } //(54)
		public decimal? tariff_heat_carrier { get; set; } //(55)
		public decimal? tariff_single_component_hw { get; set; } //(56)
		public decimal? volume_buy_coldwater_plan { get; set; } //(57)
		public decimal? tariff_double_component_hc { get; set; } //(58)
		public decimal? tariff_double_open_he { get; set; } //(59)
		public string? decision_num { get; set; } //(60)

		public string? decision_date { get; set; } //(61)
		public string? protocol_num { get; set; } //(62)
		public string? protocol_date { get; set; } //(63)
	}

	[Keyless]
	public class TZTariffSteamTabModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }

		public string? unom_tz { get; set; } //(1)
		public string? territory { get; set; } //(2)
		public string? code_tso { get; set; } //(3)
		public string? short_name { get; set; } //(4)
		public string? activity_types { get; set; } //(5)
		public decimal? tariff_econom_justified_1 { get; set; } //(6)
		public decimal? service_sold_value_1 { get; set; } //(7)
		public decimal? tariff_weighted_invest_1 { get; set; } //(8)
		public decimal? invest_component_1 { get; set; } //(9)
		public decimal? tso_single_tariff_1 { get; set; } //(10)

		public decimal? volume_he_tso_1 { get; set; } //(11)
		public decimal? budgetcons_single_tariff_1 { get; set; } //(12)
		public decimal? volume_he_budgetcons_1 { get; set; } //(13)
		public decimal? public_single_tariff_1 { get; set; } //(14)
		public decimal? volume_he_public_1 { get; set; } //(15)
		public decimal? other_single_tariff_1 { get; set; } //(16)
		public decimal? volume_he_other_1 { get; set; } //(17)
		public decimal? tariff_econom_justified_2 { get; set; } //(18)
		public decimal? service_sold_value_2 { get; set; } //(19)
		public decimal? tariff_weighted_invest_2 { get; set; } //(20)

		public decimal? invest_component_2 { get; set; } //(21)
		public decimal? tso_single_tariff_2 { get; set; } //(22)
		public decimal? volume_he_tso_2 { get; set; } //(23)
		public decimal? budgetcons_single_tariff_2 { get; set; }//(24)
		public decimal? volume_he_budgetcons_2 { get; set; } //(25)
		public decimal? public_single_tariff_2 { get; set; } //(26)
		public decimal? volume_he_public_2 { get; set; } //(27)
		public decimal? other_single_tariff_2 { get; set; } //(28)
		public decimal? volume_he_other_2 { get; set; } //(29)
		public decimal? tariff_econom_justified_3 { get; set; } //(30)

		public decimal? service_sold_value_3 { get; set; } //(31)
		public decimal? tariff_weighted_invest_3 { get; set; } //(32)
		public decimal? invest_component_3 { get; set; } //(33)
		public decimal? tso_single_tariff_3 { get; set; } //(34)
		public decimal? volume_he_tso_3 { get; set; } //(35)
		public decimal? budgetcons_single_tariff_3 { get; set; } //(36)
		public decimal? volume_he_budgetcons_3 { get; set; } //(37)
		public decimal? public_single_tariff_3 { get; set; } //(38)
		public decimal? volume_he_public_3 { get; set; } //(39)
		public decimal? other_single_tariff_3 { get; set; } //(40)

		public decimal? volume_he_other_3 { get; set; } //(41)
		public decimal? tariff_econom_justified_4 { get; set; } //(42)
		public decimal? service_sold_value_4 { get; set; } //(43)
		public decimal? tariff_weighted_invest_4 { get; set; } //(44)
		public decimal? invest_component_4 { get; set; } //(45)
		public decimal? tso_single_tariff_4 { get; set; } //(46)
		public decimal? volume_he_tso_4 { get; set; } //(47)
		public decimal? budgetcons_single_tariff_4 { get; set; } //(48)
		public decimal? volume_he_budgetcons_4 { get; set; } //(49)
		public decimal? public_single_tariff_4 { get; set; } //(50)

		public decimal? volume_he_public_4 { get; set; } //(51)
		public decimal? other_single_tariff_4 { get; set; } //(52)
		public decimal? volume_he_other_4 { get; set; } //(53)
		public decimal? cross_subsidy_volume { get; set; } //(54)
		public decimal? tariff_eto_budget_sum { get; set; } //(55)
		public decimal? tariff_eto_public_sum { get; set; } //(56)
		public decimal? tariff_eto_other_sum { get; set; } //(57)
		public decimal? tariff_compensation_budget_sum { get; set; } //(58)
		public decimal? tariff_compensation_public_sum { get; set; } //(59)
		public decimal? tariff_compensation_other_sum { get; set; } //(60)

		public decimal? tariff_preferential_budget_sum { get; set; } //(61)
		public decimal? tariff_preferential_public_sum { get; set; } //(62)
		public decimal? tariff_preferential_other_sum { get; set; } //(63)
		public decimal? heat_carrier_tariff { get; set; } //(64)
		public decimal? amount_grants_budget_sum_all { get; set; } //(65)
		public decimal? specific_grants_budget_sum { get; set; } //(66)
		public decimal? amount_grants_budget_sum { get; set; } //(67)
		public decimal? specific_grants_public_sum { get; set; } //(68)
		public decimal? amount_grants_other_sum { get; set; } //(69)
		public decimal? specific_grants_other_sum { get; set; } //(70)

		public string? decision_num { get; set; } //(71)
		public string? decision_date { get; set; } //(72)
		public string? protocol_num { get; set; } //(73)
		public string? protocol_date { get; set; } //(74)
	}



	#endregion


	#endregion

	#region IndividualFeeTable

	/// <summary>
	/// Индивидуальная плата ТП
	/// </summary>
	[Keyless]
	public class IndividualFeeTable
	{
		public int tso_id { get; set; }
		public int data_status { get; set; }
		public int individual_fee_dev_prog_id { get; set; }
		public int individual_fee_consumer_id { get; set; }

		/// <summary>
		/// КОД ТСО (1)
		/// </summary>
		public string? individual_fee_code_tso { get; set; }

		/// <summary>
		/// Наименование ТСО (2)
		/// </summary>
		public string? individual_fee_short_name { get; set; }

		/// <summary>
		/// Решение об утверждении платы за подключение. Номер (3) 
		/// </summary>
		public string? individual_fee_decision_num { get; set; }

		/// <summary>
		/// Решение об утверждении платы за подключение. Дата (4)
		/// </summary>
		public string? individual_fee_decision_date { get; set; }

		/// <summary>
		/// Статус решения (5)
		/// </summary>
		public string? individual_fee_decision_charge_name { get; set; }

		/// <summary>
		/// УНОМ ПП (6)
		/// </summary>
		public string? individual_fee_unom_pp { get; set; }

		/// <summary>
		/// Наименование заявителя (7)
		/// </summary>
		public string? individual_fee_appl_name { get; set; }

		/// <summary>
		/// Наименование подключаемого объекта (8)
		/// </summary>
		public string? individual_fee_obj_name { get; set; }

		/// <summary>
		/// Адрес подключаемого объекта (9)
		/// </summary>
		public string? individual_fee_address { get; set; }

		/// <summary>
		/// Округ подключаемого объекта (10)
		/// </summary>
		public string? individual_fee_region_name { get; set; }

		/// <summary>
		/// Район подключаемого объекта (11)
		/// </summary>
		public string? individual_fee_district_name { get; set; }

		/// <summary>
		/// Суммарная подключаемая тепловая нагрузка (с учетом ГВСmax), Гкал/ч (12)
		/// </summary>
		public decimal? individual_fee_hl_gvsm_hw_sum { get; set; }

		/// <summary>
		/// Утрвежденный размер платы за подключение. Bсего, тыс. руб. без НДС (13)
		/// </summary>
		public decimal? individual_fee_confirm_size_charge_connect { get; set; }

		/// <summary>
		/// Утрвежденный размер платы за подключение. Удельная величина, млн. руб. /(Гкал/ч) без НДС) (14)
		/// </summary>
		public decimal? individual_fee_specific_value { get; set; }
	}


	/// <summary>
	/// Плата в индивидуальном порядке
	/// </summary>
	[Keyless]
	public class OpenIndividualFeeTable
	{
		public int individual_fee_one_tso_id { get; set; }
		public int individual_fee_one_consumer_id { get; set; }
		public int individual_fee_one_dev_prog_id { get; set; }
		public int individual_fee_one_data_status { get; set; }
		public int individual_fee_one_decision_status_id { get; set; }

		[NotMapped]
		public int individual_fee_one_is_modyfied { get; set; }

		/// <summary>
		/// Наименование ТСО
		/// </summary>
		public string? individual_fee_one_short_name { get; set; }

		/// <summary>
		/// УНОМ ПП
		/// </summary>
		public string? individual_fee_one_unom_pp { get; set; }

		/// <summary>
		/// Программа развития
		/// </summary>
		public string? individual_fee_one_prog_name { get; set; }

		/// <summary>
		/// Наименование заявителя
		/// </summary>
		public string? individual_fee_one_appl_name { get; set; }

		/// <summary>
		/// Тепловая нагрузка ГВСmax в горячей воде (заявленная), Гкал/ч
		/// </summary>
		public decimal? individual_fee_one_hl_gvsm_hw { get; set; }

		/// <summary>
		/// Утрвежденный размер платы за подключение. Bсего, тыс. руб. без НДС
		/// </summary>
		public decimal? individual_fee_one_confirm_size_charge_connect { get; set; }

		/// <summary>
		/// Утрвежденный размер платы за подключение. Удельная величина, млн. руб. /(Гкал/ч) без НДС)
		/// </summary>
		public decimal? individual_fee_one_specific_value { get; set; }

		/// <summary>
		/// Решение об утверждении платы за подключение. Дата
		/// </summary>
		public string? individual_fee_one_decision_date { get; set; }

		/// <summary>
		/// Решение об утверждении платы за подключение. Номер
		/// </summary>
		public string? individual_fee_one_decision_num { get; set; }

		/// <summary>
		/// Протокол об утверждении платы за подключение. Дата
		/// </summary>
		public string? individual_fee_one_protocol_date { get; set; }

		/// <summary>
		/// Протокол об утверждении платы за подключение. Номер
		/// </summary>
		public string? individual_fee_one_protocol_num { get; set; }
	}

	[Keyless]
	public class IndividualFeeApplNameObject
	{
		public string? appl_name { get; set; }
		public decimal? individual_fee_one_hl_gvsm_hw { get; set; }
	}

	#endregion

	#region StandardizedRatesTable

	/// <summary>
	/// Индивидуальная плата ТП
	/// </summary>
	[Keyless]
	public class StandardizedRatesTable
	{
		public int standardized_rates_tso_id { get; set; }
		public int data_status { get; set; }
		//public int perspective_year { get; set; }

		/// <summary>
		/// КОД ТСО (1)
		/// </summary>
		public string? standardized_rates_code_tso { get; set; }

		/// <summary>
		/// Наименование ТСО (2)
		/// </summary>
		public string? standardized_rates_short_name { get; set; }

		/// <summary>
		/// Решение об утверждении стандартизированных ставок за подключение. Номер (3) 
		/// </summary>
		public string? standardized_rates_decision_num { get; set; }

		/// <summary>
		/// Решение об утверждении стандартизированных ставок за подключение. Дата (4)
		/// </summary>
		public string? standardized_rates_decision_date { get; set; }

		/// <summary>
		/// Статус решения (5)
		/// </summary>
		public string? standardized_rates_decision_charge_name { get; set; }

		/// <summary>
		/// Расходы на проведение мероприятий по подключению, руб./(Гкал/ч) без НДС (6)
		/// </summary>
		public decimal? standardized_rates_cost_connect_event { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Подземная канальная прокладка. Ду 250 мм (7)
		/// </summary>
		public decimal? standardized_rates_cost_hn_underground_ch_du250 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Подземная канальная прокладка. Ду 251-400 мм (8)
		/// </summary>
		public decimal? standardized_rates_cost_hn_underground_ch_du251_400 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Подземная канальная прокладка. Ду 401-550 мм (9)
		/// </summary>
		public decimal? standardized_rates_cost_hn_underground_ch_du401_550 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Подземная канальная прокладка. Ду 551-700 мм (10)
		/// </summary>
		public decimal? standardized_rates_cost_hn_underground_ch_du551_700 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Подземная канальная прокладка. Ду 700 мм и выше (11)
		/// </summary>
		public decimal? standardized_rates_cost_hn_underground_ch_du700 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Подземная бесканальная прокладка. Ду 250 мм (12)
		/// </summary>
		public decimal? standardized_rates_cost_hn_underground_ch_free_du250 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Подземная бесканальная прокладка. Ду 251-400 мм (13)
		/// </summary>
		public decimal? standardized_rates_cost_hn_underground_ch_free_du251_400 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Подземная бесканальная прокладка. Ду 401-550 мм (14)
		/// </summary>
		public decimal? standardized_rates_cost_hn_underground_ch_free_du401_550 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Подземная бесканальная прокладка. Ду 551-700 мм (15)
		/// </summary>
		public decimal? standardized_rates_cost_hn_underground_ch_free_du551_700 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Подземная бесканальная прокладка. Ду 700 мм и выше (16)
		/// </summary>
		public decimal? standardized_rates_cost_hn_underground_ch_free_du700 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Надземная прокладка. Ду 250 мм (17)
		/// </summary>
		public decimal? standardized_rates_cost_hn_overground_du250 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Надземная прокладка. Ду 251-400 мм (18)
		/// </summary>
		public decimal? standardized_rates_cost_hn_overground_du251_400 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Надземная прокладка. Ду 401-550 мм (19)
		/// </summary>
		public decimal? standardized_rates_cost_hn_overground_du401_550 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Надземная прокладка. Ду 551-700 мм (20)
		/// </summary>
		public decimal? standardized_rates_cost_hn_overground_du551_700 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Надземная прокладка. Ду 700 мм и выше (21)
		/// </summary>
		public decimal? standardized_rates_cost_hn_overground_du700 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых пунктов, руб./(Гкал/ч) без НДС (22)
		/// </summary>
		public decimal? standardized_rates_cost_hp { get; set; }

		/// <summary>
		/// Налог на прибыль, руб./(Гкал/ч) без НДС (23)
		/// </summary>
		public decimal? standardized_rates_profit_tax { get; set; }

		/// <summary>
		/// Протокол об утверждении стандартизированных ставок за подключение. Номер
		/// </summary>
		public string? standardized_rates_protocol_num { get; set; }
	}




	/// <summary>
	/// Плата в индивидуальном порядке
	/// </summary>
	[Keyless]
	public class OpenStandardizedRatesTable
	{
		public int standardized_rates_one_tso_id { get; set; }
		public int standardized_rates_one_data_status { get; set; }
		public short standardized_rates_one_decision_status_id { get; set; }

		[NotMapped]
		public int standardized_rates_one_is_modyfied { get; set; }

		/// <summary>
		/// Статус решения
		/// </summary>
		public string? standardized_rates_one_decision_charge_name { get; set; }

		/// <summary>
		/// Наименование ТСО
		/// </summary>
		public string? standardized_rates_one_short_name { get; set; }

		/// <summary>
		/// Расходы на проведение мероприятий по подключению, руб./(Гкал/ч) без НДС
		/// </summary>
		public decimal? standardized_rates_one_cost_connect_event { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Подземная канальная прокладка. Ду 250 мм 
		/// </summary>
		public decimal? standardized_rates_one_cost_hn_underground_ch_du250 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Подземная канальная прокладка. Ду 251-400 мм 
		/// </summary>
		public decimal? standardized_rates_one_cost_hn_underground_ch_du251_400 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Подземная канальная прокладка. Ду 401-550 мм 
		/// </summary>
		public decimal? standardized_rates_one_cost_hn_underground_ch_du401_550 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Подземная канальная прокладка. Ду 551-700 мм 
		/// </summary>
		public decimal? standardized_rates_one_cost_hn_underground_ch_du551_700 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Подземная канальная прокладка. Ду 700 мм и выше 
		/// </summary>
		public decimal? standardized_rates_one_cost_hn_underground_ch_du700 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Подземная бесканальная прокладка. Ду 250 мм 
		/// </summary>
		public decimal? standardized_rates_one_cost_hn_underground_ch_free_du250 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Подземная бесканальная прокладка. Ду 251-400 мм 
		/// </summary>
		public decimal? standardized_rates_one_cost_hn_underground_ch_free_du251_400 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Подземная бесканальная прокладка. Ду 401-550 мм 
		/// </summary>
		public decimal? standardized_rates_one_cost_hn_underground_ch_free_du401_550 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Подземная бесканальная прокладка. Ду 551-700 мм 
		/// </summary>
		public decimal? standardized_rates_one_cost_hn_underground_ch_free_du551_700 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Подземная бесканальная прокладка. Ду 700 мм и выше 
		/// </summary>
		public decimal? standardized_rates_one_cost_hn_underground_ch_free_du700 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Надземная прокладка. Ду 250 мм 
		/// </summary>
		public decimal? standardized_rates_one_cost_hn_overground_du250 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Надземная прокладка. Ду 251-400 мм 
		/// </summary>
		public decimal? standardized_rates_one_cost_hn_overground_du251_400 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Надземная прокладка. Ду 401-550 мм 
		/// </summary>
		public decimal? standardized_rates_one_cost_hn_overground_du401_550 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Надземная прокладка. Ду 551-700 мм 
		/// </summary>
		public decimal? standardized_rates_one_cost_hn_overground_du551_700 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых сетей, руб./(Гкал/ч) без НДС. Надземная прокладка. Ду 700 мм и выше 
		/// </summary>
		public decimal? standardized_rates_one_cost_hn_overground_du700 { get; set; }

		/// <summary>
		/// Расходы на создание (реконструкцию) тепловых пунктов, руб./(Гкал/ч) без НДС 
		/// </summary>
		public decimal? standardized_rates_one_cost_hp { get; set; }

		/// <summary>
		/// Налог на прибыль, руб./(Гкал/ч) без НДС 
		/// </summary>
		public decimal? standardized_rates_one_profit_tax { get; set; }





		/// <summary>
		/// Решение об утверждении стандартизированных ставок за подключение. Дата
		/// </summary>
		public string? standardized_rates_one_decision_date { get; set; }

		/// <summary>
		/// Решение об утверждении стандартизированных ставок за подключение. Номер
		/// </summary>
		public string? standardized_rates_one_decision_num { get; set; }

		/// <summary>
		/// Протокол об утверждении стандартизированных ставок за подключение. Дата
		/// </summary>
		public string? standardized_rates_one_protocol_date { get; set; }

		/// <summary>
		/// Протокол об утверждении стандартизированных ставок за подключение. Номер
		/// </summary>
		public string? standardized_rates_one_protocol_num { get; set; }
	}

	#endregion

	#region PowerReservePaymentTable

	/// <summary>
	/// Плата за резерв мощности
	/// </summary>
	[Keyless]
	public class PowerReservePaymentTable
	{
		public int power_reserve_payment_tso_id { get; set; }
		public int data_status { get; set; }

		/// <summary>
		/// КОД ТСО (1)
		/// </summary>
		public string? power_reserve_payment_code_tso { get; set; }

		/// <summary>
		/// Наименование ТСО (2)
		/// </summary>
		public string? power_reserve_payment_short_name { get; set; }

		/// <summary>
		/// Решение об утверждении платы за услуги по поддержанию резервной тепловой мощности. Номер (3) 
		/// </summary>
		public string? power_reserve_payment_decision_num { get; set; }

		/// <summary>
		/// Решение об утверждении платы за услуги по поддержанию резервной тепловой мощности. Дата (4)
		/// </summary>
		public string? power_reserve_payment_decision_date { get; set; }

		/// <summary>
		/// Статус решения (5)
		/// </summary>
		public string? power_reserve_payment_decision_charge_name { get; set; }

		/// <summary>
		/// Плата за услуги по поддержанию резервной тепловой мощности, руб./(Гкал/ч) в месяц без НДС. До тепловых пунктов (6)
		/// </summary>
		public decimal? power_reserve_payment_cost_service_before_hp { get; set; }

		/// <summary>
		/// Плата за услуги по поддержанию резервной тепловой мощности, руб./(Гкал/ч) в месяц без НДС. После тепловых пунктов (7)
		/// </summary>
		public decimal? power_reserve_payment_cost_service_after_hp { get; set; }
	}

	/// <summary>
	/// Плата в индивидуальном порядке
	/// </summary>
	[Keyless]
	public class OpenPowerReservePaymentTable
	{
		public int power_reserve_payment_one_tso_id { get; set; }
		public int power_reserve_payment_one_data_status { get; set; }
		public short power_reserve_payment_one_decision_status_id { get; set; }

		[NotMapped]
		public int power_reserve_payment_one_is_modyfied { get; set; }

		/// <summary>
		/// Статус решения
		/// </summary>
		public string? power_reserve_payment_one_decision_charge_name { get; set; }

		/// <summary>
		/// Наименование ТСО
		/// </summary>
		public string? power_reserve_payment_one_short_name { get; set; }

		/// <summary>
		/// Плата за услуги по поддержанию резервной тепловой мощности, руб./(Гкал/ч) в месяц без НДС. До тепловых пунктов
		/// </summary>
		public decimal? power_reserve_payment_one_cost_service_before_hp { get; set; }

		/// <summary>
		/// Плата за услуги по поддержанию резервной тепловой мощности, руб./(Гкал/ч) в месяц без НДС. После тепловых пунктов
		/// </summary>
		public decimal? power_reserve_payment_one_cost_service_after_hp { get; set; }

		/// <summary>
		/// Решение об утверждении платы за услуги по поддержанию резервной тепловой мощности. Дата
		/// </summary>
		public string? power_reserve_payment_one_decision_date { get; set; }

		/// <summary>
		/// Решение об утверждении платы за услуги по поддержанию резервной тепловой мощности. Номер
		/// </summary>
		public string? power_reserve_payment_one_decision_num { get; set; }

		/// <summary>
		/// Протокол об утверждении платы за услуги по поддержанию резервной тепловой мощности. Дата
		/// </summary>
		public string? power_reserve_payment_one_protocol_date { get; set; }

		/// <summary>
		/// Протокол об утверждении платы за услуги по поддержанию резервной тепловой мощности. Номер
		/// </summary>
		public string? power_reserve_payment_one_protocol_num { get; set; }
	}


	#endregion
}
