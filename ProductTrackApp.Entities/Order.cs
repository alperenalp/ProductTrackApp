using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTrackApp.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool Status { get; set; }

        public Product? Product { get; set; }
        public int? ProductId{ get; set; }
        public User? User { get; set; }
        public int? UserId { get; set; }
    }
}
