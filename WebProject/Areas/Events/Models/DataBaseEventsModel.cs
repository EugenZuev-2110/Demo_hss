using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models.Events
{
    [Table("Sources", Schema = "events")]
    public class Sources
    {

        public int Id { get; set; }
        public Int16? year { get; set; }
        public int? last_num { get; set; }
        public string? unom { get; set; }
        public string? ip_num { get; set; }
        public long? equip_id { get; set; }
        public bool? is_city_own { get; set; }
        public int? source_num { get; set; }
        public Int16? obj_code { get; set; }
        public Int16? purpose_code { get; set; }
        public Int16? type_code { get; set; }
        public Int16? sfinance_code { get; set; }
        public Int16? start_realize_year { get; set; }
        public Int16? end_realize_year { get; set; }
        public double? power_before { get; set; }
        public double? power_after { get; set; }
        public string? event_name { get; set; }
        public double? cost_before { get; set; }
        public int user_id { get; set; }
        public Int16 status { get; set; }
        public DateTime create_date { get; set; }
        public DateTime? edit_date { get; set; }

    }
    [Table("Networks", Schema = "events")]
    public class Networks
    {

        public int Id { get; set; }
        public Int16? net_type { get; set; }
        public Int16? year { get; set; }
        public int? last_num { get; set; }
        public string? unom { get; set; }
        public string? ip_num { get; set; }
        public bool? is_city_own { get; set; }
        public int? sys { get; set; }
        public int? source_num { get; set; }
        public int? tso_code { get; set; }
        public Int16? obj_code { get; set; }
        public Int16? purpose_code { get; set; }
        public Int16? type_code { get; set; }
        public Int16? sfinance_code { get; set; }
        public Int16? start_realize_year { get; set; }
        public Int16? end_realize_year { get; set; }
        public string? address_start { get; set; }
        public string? address_end { get; set; }
        public double? length_before { get; set; }
        public double? length_after { get; set; }
        public double? diameter_before { get; set; }
        public double? diameter_after { get; set; }
        public int? reg_id { get; set; }
        public int? year_of_laying { get; set; }
        public double? cap_invest { get; set; }
        public int user_id { get; set; }
        public Int16 status { get; set; }
        public DateTime create_date { get; set; }
        public DateTime? edit_date { get; set; }

    }
    [Table("ClosedScheme", Schema = "events")]
    public class ClosedScheme
    {
        public int Id { get; set; }
        public Int16? year { get; set; }
        public bool? is_city_own { get; set; }
        public int? last_num { get; set; }
        public string? unom { get; set; }
        public string? ip_num { get; set; }
        public int? source_num { get; set; }
        public int? tso_code { get; set; }
        public Int16? obj_code { get; set; }
        public Int16? purpose_code { get; set; }
        public Int16? type_code { get; set; }
        public Int16? sfinance_code { get; set; }
        public Int16? start_realize_year { get; set; }
        public Int16? end_realize_year { get; set; }
        public string? address_start { get; set; }
        public string? address_end { get; set; }
        public int? reg_id { get; set; }
        public string? event_name { get; set; }
        public double? cap_invest { get; set; }
        public int user_id { get; set; }
        public Int16 status { get; set; }
        public DateTime create_date { get; set; }
        public DateTime? edit_date { get; set; }

    }
    [Table("DictEventsTypes", Schema = "events")]
    public class DictEventsTypes
    {
        public int Id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }
    [Table("DictObjectsCodes", Schema = "events")]
    public class DictObjectsCodes
    {
        public int Id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }
    [Table("DictPurposeCodes", Schema = "events")]
    public class DictPurposeCodes
    {
        public int Id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }
    [Table("DictSFinanceCodes", Schema = "events")]
    public class DictSFinanceCodes
    {
        public int Id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }
    [Table("YearsView", Schema = "events")]
    public class YearsView
    {
        public short year { get; set; }
    }
}