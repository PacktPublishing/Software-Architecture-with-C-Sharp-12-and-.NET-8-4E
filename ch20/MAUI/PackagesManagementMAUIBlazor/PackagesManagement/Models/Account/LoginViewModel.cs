using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PackagesManagement.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "user name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public string Password { get; set; }

        [Display(Name = "remember me")]
        public bool RememberMe { get; set; }
    }
}
