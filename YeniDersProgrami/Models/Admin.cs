using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YeniDersProgrami.Models
{
    public class Admin
    {
        [Required(ErrorMessage = "Lütfen bir ad giriniz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lütfen bir şifre giriniz")]
        [DataType("Password")]
        [MaxLength(6)]
        public string Password { get; set; }
    }
}