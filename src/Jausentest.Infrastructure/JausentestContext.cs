using Jausentest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace Jausentest.Infrastructure
{
    public class JausentestContext : DbContext
    {

        public DbSet<BeislEntity> Beisl { get; set; }
        public DbSet<TagEntity> Tags { get; set; }

        public JausentestContext(DbContextOptions<JausentestContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseMySql("server=localhost;user=root;password=geb1305,,;database=jause",
        //        new MySqlServerVersion(new Version(8, 0, 23)),
        //        mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend))
        //        .EnableSensitiveDataLogging()
        //        .EnableDetailedErrors();
        //}

    }
}
