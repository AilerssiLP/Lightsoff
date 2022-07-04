using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LightsOff.Entity;

namespace LightsOff.Service
{
    internal class LightsOffDbContext : DbContext
    {
        public DbSet<Score> Scores { get; set; }

        public DbSet<Rating> Rating { get; set; }

        public DbSet<Comment> Comment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=LightsOff;Trusted_Connection=True;");
        }
    }
}