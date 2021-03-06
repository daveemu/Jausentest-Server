﻿using Jausentest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace Jausentest.Infrastructure
{
    public class JausentestContext : DbContext
    {

        public DbSet<BeislEntity> Beisl { get; set; }
        public DbSet<TagEntity> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<BeislEntity>()
                .HasMany(b => b.Tags)
                .WithMany(t => t.Beisl)
                .UsingEntity(x => x.ToTable("BeislTags"))
                .OwnsOne(a => a.Address);

            modelBuilder
                .Entity<TagEntity>()
                .HasKey(t => t.Name);
        }

        public JausentestContext(DbContextOptions<JausentestContext> options) : base(options)
        {

        }


    }
}
