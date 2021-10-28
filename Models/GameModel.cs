using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GameCatalog.Models {
    public class GameModel {
        public int GameID { get; set; }
        public string Title { get; set; }
        public string Developer { get; set; }
        [DisplayName ("Release Date")]
        public string ReleaseDate { get; set; }
        public string Genres { get; set; }
        public string Platforms { get; set; }

        public GameModel() {
            GameID = -1;
            Title = "";
            Developer = "";
            ReleaseDate = DateTime.Now.ToShortDateString();
            Genres = "";
            Platforms = "";
        }

        public GameModel(int gameID, string title, string developer, DateTime releaseDate, string genres, string platforms) {
            GameID = gameID;
            Title = title;
            Developer = developer;
            ReleaseDate = releaseDate.ToShortDateString();
            Genres = genres;
            Platforms = platforms;
        }
    }
}