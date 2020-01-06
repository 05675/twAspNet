using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace twAspnet.Models
{
    public class Favorite
    {
        public string Id { get; set; }
        public string Comment { get; set; }
        public DateTime Favoritedate { get; set; }
        public string Tweet { get; set; }
        public string Name { get; set; }
        public string ScreenName { get; set; }
        public string UrlId { get; set; }
        public string CreatedAt { get; set; }
    }
}
