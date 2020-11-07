using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualBasic.CompilerServices;

namespace StoreTemplate.ViewModels.Identity
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required] 
        public string EMail { get; set; }

        [BindNever] 
        public string ReturnUrl { get; set; }
    }
}
