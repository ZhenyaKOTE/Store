using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Store.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Поле Email является обезательным")]
        [EmailAddress(ErrorMessage = "Проверьте правильность введенного Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле имени является обезательным")]
        [MinLength(5, ErrorMessage = "Длина Имени должно быть не мение 5 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле пароля является обезательным")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Длина пароля должна быть не мение 8 символов")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле подтверждения пароля обезательное")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }


    }
}