using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.DomainNew.ViewModels
{
    public class EmployeeView
    {

        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Имя является обязательным полем.")]
        [Display(Name ="名前")]
        [StringLength(maximumLength:200, MinimumLength =2, ErrorMessage = "В имени должно быть от 2 до 200 символов")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Фамилия является обязательным полем.")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Возраст является обязательным полем.")]
        [Display(Name = "Возраст")]
        public int Age { get; set; }
    }
}
