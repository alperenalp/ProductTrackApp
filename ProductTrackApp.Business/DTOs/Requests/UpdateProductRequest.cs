using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTrackApp.Business.DTOs.Requests
{
    public class UpdateProductRequest
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Brand.")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "Please enter Model.")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Please enter Category.")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Please enter ProductCode.")]
        public string ProductCode { get; set; }
        public int EmployeeId { get; set; }

    }
}
