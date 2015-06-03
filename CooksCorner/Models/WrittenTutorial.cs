using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CooksCorner.Models
{
    public class WrittenTutorial
    {
        // ScaffoldColumn(false) means we don't want to use this in scafolding
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DisplayName("What kind of Coocking")]
        public int GenreId { get; set; }

        [ScaffoldColumn(false)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(160)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }

        /*
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        */
        public virtual Genre Genre { get; set; }
        public virtual UserProfile User { get; set; }
    }
}