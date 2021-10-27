using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameCatalog.Models {
    public class ViewModel {
        public GameModel Game { get; set; }
        public IEnumerable<GenreModel> Genres { get; set; }
        public IEnumerable<PlatformModel> Platforms { get; set; }
        //public IEnumerable<string> Title { get; set; }
        //public IEnumerable<string> Developer { get; set; }
        //public IEnumerable<string> ReleaseDate { get; set; }
        //public IEnumerable<SelectListItem> Genres { get; set; }
        //public IEnumerable<SelectListItem> Platforms { get; set; }
    }
}