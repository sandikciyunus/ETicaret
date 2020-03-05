using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="İsim boş bırakılmaz")]
        public string FullName { get; set; }

        [Required(ErrorMessage ="Kullanıcı adı alanı boş bırakılmaz")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Şifre alanı boş bırakılamaz")]
        [MinLength(6,ErrorMessage ="Şifre minimum 6 karakter uzunluğunda olmalıdır")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage ="Şifreyi tekrar girmelisin")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Şifreler uyuşmuyor")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "Mail alanı boş bırakılmaz")]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }
    }
}
