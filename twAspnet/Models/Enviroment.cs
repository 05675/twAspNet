using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace twAspnet.Models
{
    public class Enviroment
    {
        public int Id { get; set; }
        public string Akey { get; set; }
        public string ASecretKey { get; set; }
        public string AToken { get; set; }
        public string ATokenSecret { get; set; }
    }
}
