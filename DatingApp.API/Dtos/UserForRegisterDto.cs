using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        [StringLength(maximumLength: 8, MinimumLength = 4, ErrorMessage = "Passwd between 4 and 8")]
        public string Password { get; set; }
    }
}
