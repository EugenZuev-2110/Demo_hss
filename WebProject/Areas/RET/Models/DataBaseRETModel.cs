using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models.RET
{
    [Table("DictTemplates", Schema = "ret")]
    public class DictTemplates
    {
        [Key]
        public int type_id { get; set; }
        public string? template_name { get; set; }
        public string? data_table_name { get; set; }
        public string? template_path { get; set; }

    }
    [Table("TemplateRET_K", Schema = "ret")]
    public class TemplateRET_K
    {
        public int Id { get; set; }
        public int hss_id { get; set; }
        public int ret_coef_id { get; set; }
        public decimal? a { get; set; }
        public decimal? b { get; set; }
        public decimal? k { get; set; }

    }

}