using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CooksCorner.Models
{
   // [Bind(Exclude = "VideoId")]
    public class Video
    {
        // ScaffoldColumn(false) means we don't want to use this in scafolding
        [ScaffoldColumn(false)]
        public int VideoId { get; set; }

        [DisplayName("What kind of Coocking")]
        public int GenreId { get; set; }

        [ScaffoldColumn(false)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "An Video Title is required")]
        [StringLength(160)]
        public string Title { get; set; }

        [Required(ErrorMessage = "An Video Description is required")]
        [StringLength(500)]
        public string Description { get; set; }
        
        [DisplayName("Video URL")]
        [Required(ErrorMessage = "An Video Url is required")]
        [StringLength(200)]
        [AllowHtml]
        public string VideortUrl { get; set; }

        /*
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        */

        public virtual Genre Genre { get; set; }
        public virtual UserProfile User { get; set; }
                        
    }
}