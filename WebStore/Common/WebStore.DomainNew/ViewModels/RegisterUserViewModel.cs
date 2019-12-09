using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.DomainNew.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required, MaxLength(length:255)]
        public string UserName { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare(otherProperty: nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
