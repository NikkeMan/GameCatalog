using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameCatalog.Models {
    public class ViewModel {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Developer { get; set; }
        [Required]
        [DisplayName ("Release Date")]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public IEnumerable<SelectListItem> Genres { get; set; }
        [Required]
        public IEnumerable<SelectListItem> Platforms { get; set; }
        public int[] SelectedGenres { get; set; }
        public int[] SelectedPlatforms { get; set; }
    }
}