using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CooksCorner.Models
{
    public class Faq
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Question is required")]
        public string Question { get; set; }

        [Required(ErrorMessage = "Answer is required")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Answer { get; set; }
    }
}