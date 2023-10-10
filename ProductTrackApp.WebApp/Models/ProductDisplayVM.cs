using System.ComponentModel;

namespace ProductTrackApp.WebApp.Models
{
    public class ProductDisplayVM
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Category { get; set; }
        public string ProductCode { get; set; }
        public bool Status { get; set; }
        [DisplayName("Warehouse Employee")]
        public string EmployeeName { get; set; }
    }
}
