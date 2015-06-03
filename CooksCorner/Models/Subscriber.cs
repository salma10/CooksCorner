using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CooksCorner.Models
{
    public class Subscriber
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Your name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Your Email is requierd")]
        public string Email { get; set; }
    }
}