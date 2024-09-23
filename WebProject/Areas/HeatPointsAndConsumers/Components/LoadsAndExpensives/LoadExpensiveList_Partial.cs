using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using DataBaseHSS.Models;
using System.Linq;

namespace WebProject.Components
{
    public class LoadExpensiveList_Partial : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public LoadExpensiveList_Partial(HssDbContext context, HSSController m_c)
        {
            _context = context;
            _m_c = m_c;
        }
        public async Task<IViewComponentResult> InvokeAsync(int data_status, int[] perspective_years, int load_type = 2, int hp_status_id = -1, int source_id = -1, int tso_id = -1, int hp_type_id = -1)
        {
            if (data_status == 0)
            {
                data_status = _m_c.GetCurrentDS();
            }

            var model = new LoadsAndExpensivesMainModel();
            int[] footer_per_years = new int[1] { data_status};

            model.LoadsAndExpensivesMain = await _context.LoadsAndExpensivesMainList
                .FromSqlInterpolated($"exec heat_points.sp_GetHeatPointsLoadsDataList {data_status},{hp_type_id},{hp_status_id},{source_id},{tso_id},{load_type}")
                .ToListAsync() ?? new List<LoadsAndExpensivesMainList>();

			if ((perspective_years.Length > 1 || (perspective_years.Length == 1 && perspective_years[0] != data_status)) && load_type > 1)
            {
				footer_per_years = perspective_years;

				var per_year_str = ConvertFromStringArrToSingleString(ref perspective_years, data_status);
				ViewBag.PerspectiveYears = perspective_years;

				model.LoadsAndExpensivesPerspectiveMain = await _context.LoadsAndExpensivesPerspectiveMainList
					.FromSqlInterpolated($"exec heat_points.sp_GetHeatPointsLoadsPerspectiveDataList {data_status},{per_year_str},{hp_type_id},{hp_status_id},{source_id},{tso_id},{load_type}")
					.ToListAsync() ?? new List<LoadsAndExpensivesPerspectiveMainList>();
			}
            else
            {
				ViewBag.PerspectiveYears = new int[0];				
				model.LoadsAndExpensivesPerspectiveMain = new List<LoadsAndExpensivesPerspectiveMainList>();
			}

			ViewBag.FooterPerspectiveYears = footer_per_years;

			return View("LoadExpensiveList_Partial", model);
		}

        private string ConvertFromStringArrToSingleString(ref int[] str, int data_status)
        {
			var per_years_str = string.Empty;

            if (str[0] == data_status)
            {
				var new_str = new int[str.Length - 1];

				for (int i = 1; i < str.Length; i++)
                    new_str[i - 1] = str[i];

                str = new_str;
			}

			for (var i = 0; i < str.Length; i++)
			{
				per_years_str += str[i];
				if (i < str.Length - 1)
					per_years_str += ",";
			}

            return per_years_str;
		}
    }
}
