using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameCatalog.Models {
    public class GenreModel {
        public int GenreID { get; set; }
        public string Name { get; set; }

        public GenreModel() {
            GenreID = -1;
            Name = "GenreName";
        }

        public GenreModel(int genreID, string name) {
            GenreID = genreID;
            Name = name;
        }
    }
}