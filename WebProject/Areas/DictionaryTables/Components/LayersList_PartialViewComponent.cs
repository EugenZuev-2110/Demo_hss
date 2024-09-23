using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components
{
	public class LayersList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public LayersList_PartialViewComponent(HssDbContext context, HSSController c)
		{
			_context = context;
			_m_c = c;
		}

		public async Task<IViewComponentResult> InvokeAsync(int data_status, int userId)
		{
			
			var _layer = new List<LayerViewModel>();
			if (data_status == 0)
			{
				data_status = _m_c.GetCurrentDS();
			}
			_layer = (from layer in _context.Layers
					  where layer.layer_data_status == (_context.Layers.Where(x => x.layer_data_status <= data_status && layer.Id == x.Id).Max(x => x.layer_data_status))
					  join l_status in _context.LayerStatuses on layer.layer_status_id equals l_status.Id
					  join l_type in _context.LayerTypes on layer.layer_type_id equals l_type.Id
					  select new LayerViewModel
					  {
						  Id = layer.Id,
						  layer_unom = layer.layer_unom,
						  layer_status = l_status.layer_status_name,
						  layer_type = l_type.layer_type_name,
						  layer_data_status = layer.layer_data_status,
						  layer_perspective_year = layer.layer_perspective_year,
						  layer_filename = layer.layer_filename,
						  layer_filepath = layer.layer_filepath,
						  layer_description = layer.layer_description,
						  layer_delete_year = layer.layer_delete_year,
						  layer_delete_reason = layer.layer_delete_reason
						  
					  }).ToList();
			return View("LayersList_Partial", _layer);

		}
	}
}
