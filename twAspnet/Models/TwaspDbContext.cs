using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace twAspnet.Models
{
    public class TwaspDbContext : DbContext
    {
        public TwaspDbContext(DbContextOptions options)
        : base(options) { }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Favorite> Favorite { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite("Data Source=Twasp.db");
    }
}