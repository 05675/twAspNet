using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace twAspnet.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public int Twid { get; set; }
        public string Comment { get; set; }
        public DateTime Favoritedate { get; set; }
    }
}
