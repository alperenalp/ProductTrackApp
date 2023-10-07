using ProductTrackApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTrackApp.Business.DTOs.Responses
{
    public class ProductDisplayResponse
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Category { get; set; }
        public string ProductCode { get; set; }
        public bool Status { get; set; }
        public int? EmployeeId { get; set; }
    }
}
