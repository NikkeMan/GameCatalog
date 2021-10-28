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
            Name = "";
        }

        public PlatformModel(int platformID, string name) {
            PlatformID = platformID;
            Name = name;
        }
    }
}