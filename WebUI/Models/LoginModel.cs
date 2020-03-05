using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Mail alanı boş bırakılmaz")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Mail adresi düzgün biçimde olmalıdır")]
        public string Mail { get; set; }
        [Required(ErrorMessage ="Şifre alanı boş bırakılamaz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
