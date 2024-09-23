using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Areas.DictionaryTables.Models
{
	[Keyless]
	public class CoefCorrectionClimateViewModel
	{
		public short coef_id { get; set; }
		public string? coef_name_html { get; set; }
	}
	[Keyless]
	public class CoefCorrectionClimatePerspectiveViewModel
	{
		public short coef_id { get; set; }
		public int data_status { get; set; }
		public int? perspective_year { get; set; }
		public decimal? coef_value { get; set; }
	}
	[Keyless]
	public class CoefCorrectionClimateMainViewModel
	{
		public List<CoefCorrectionClimateViewModel> coefCorrection { get; set; }
		public List<CoefCorrectionClimatePerspectiveViewModel> coefCorrectionPerspective { get; set; }
	}

	[Table("CoefCorrection", Schema = "dictionary")]
	public class CoefCorrection
	{
		public short coef_id { get; set; }
		public string? coef_name { get; set; }
		public string? coef_name_html { get; set; }
	}

	[Table("CoefCorrection_Perspective", Schema = "dictionary")]
	public class CoefCorrection_Perspective
	{
		public short coef_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? coef_value { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}

	[Table("CoefInflationary", Schema = "dictionary")]
	public class CoefInflationary
	{
		public short coef_id { get; set; }
		public string? coef_name { get; set; }
		public string? coef_name_html { get; set; }
	}

	[Table("CoefInflationary_Perspective", Schema = "dictionary")]
	public class CoefInflationary_Perspective
	{
		public short coef_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? coef_value { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}
}
