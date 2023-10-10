using System.ComponentModel;

namespace ProductTrackApp.WebApp.Models
{
    public class OrderDisplayVM
    {
        public int Id { get; set; }
        [DisplayName("Ordered Product")]
        public string Product { get; set; }
        [DisplayName("User's Full Name")]
        public string UserName { get; set; }
        [DisplayName("User's Email")]
        public string UserEmail { get; set; }
    }
}
