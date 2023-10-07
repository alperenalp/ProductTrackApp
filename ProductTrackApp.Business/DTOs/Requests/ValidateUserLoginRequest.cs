using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTrackApp.Business.DTOs.Requests
{
    public class ValidateUserLoginRequest
    {
        [Required(ErrorMessage = "Please enter your username.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        [MinLength(3, ErrorMessage = "Please at least 3 number.")]
        public string Password { get; set; }
    }
}
