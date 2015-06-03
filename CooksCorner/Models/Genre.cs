using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CooksCorner.Models
{
    public  class Genre
    {
        public int GenreId { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage =  "Category Description is required")]
        public string Description { get; set; }

        public List<Video> Videos { get; set; }
        public List<WrittenTutorial> WrittenTutorials { get; set; }
    }
}