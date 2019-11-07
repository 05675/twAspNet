using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using twAspnet.Models;

namespace twAspnet.Models
{
    public class OptionsBuilder : DbContext
    {
        public OptionsBuilder(DbContextOptions<OptionsBuilder> options)
            : base(options)
        {
            var option = new DbContextOptionsBuilder<TwaspDbContext>();
            var connectionString = "Twasp.db";
            option.UseSqlite(connectionString);
            using var context = new TwaspDbContext(option.Options);
            var users = context.Enviroment;
        }
        
    }
}
