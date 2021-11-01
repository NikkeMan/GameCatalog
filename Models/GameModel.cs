using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameCatalog.Models {
    public class GameModel {
        public int GameID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Developer { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName ("Release Date")]
        [DisplayFormat (DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime ReleaseDate { get; set; }
        [Required]
        [DisplayName("Genres")]
        public string GameGenres { get; set; }
        [Required]
        [DisplayName("Platforms")]
        public string GamePlatforms { get; set; }
        public IEnumerable<SelectListItem> Genres { get; set; }
        public IEnumerable<SelectListItem> Platforms { get; set; }
        [Required]
        [DisplayName("Genres")]
        public List<int> SelectedGenres { get; set; }
        [Required]
        [DisplayName("Platforms")]
        public List<int> SelectedPlatforms { get; set; }

        public GameModel() {
            GameID = -1;
            Title = "";
            Developer = "";
            ReleaseDate = DateTime.Now;
        }
    }
}