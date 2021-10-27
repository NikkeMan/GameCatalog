using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameCatalog.Models {
    public class PlatformModel {
        public int PlatformID { get; set; }
        public string Name { get; set; }

        public PlatformModel() {
            PlatformID = -1;
            Name = "PlatformName";
        }

        public PlatformModel(int genreID, string name) {
            PlatformID = genreID;
            Name = name;
        }
    }
}