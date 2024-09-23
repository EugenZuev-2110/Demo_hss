using Microsoft.EntityFrameworkCore;

namespace WebProject.Models
{
    //[NotMapped]
    [Keyless]
    public class ItemsViewModel
    {
        public int Id { get; set; }
        public string searchText { get; set; }
        public string unom_num { get; set; }
        public byte item_number { get; set; }
        public bool is_complete { get; set; }
        public string? conditional_address { get; set; }
        public string? object_type_name { get; set; }
        public string? object_id { get; set; }
        public string? source_name { get; set; }
        public string? district_name { get; set; }
        public int category_id { get; set; }
    }

    [Keyless]
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string unom_num { get; set; }
        public string? conditional_address { get; set; }
        public int? object_type_id { get; set; }
        public int? source_id { get; set; }
        public string? object_id { get; set; }
        public int? district_id { get; set; }
        public bool ItsExecutorOrAdmin { get; set; }
        public int? category_id { get; set; }

    }
}