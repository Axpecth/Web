using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.DataModel
{
    public class MobilyaBilgi
    {
        public List<KampanyaliUrunler> kampanyaliUrunlers { get; set; }
        public List<Kataloglar> kataloglars { get; set; }
        public List<Urunler> urunlers { get; set; }
    }
}