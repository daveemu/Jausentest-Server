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


    }
}
