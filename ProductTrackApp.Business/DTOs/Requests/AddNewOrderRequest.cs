using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTrackApp.Business.DTOs.Requests
{
    public class AddNewOrderRequest
    {
        public int productId { get; set; }
        public int userId { get; set; }
    }
}
