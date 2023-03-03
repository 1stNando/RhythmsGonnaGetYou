using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RhythmsGonnaGetYou
{
    class RhythmsGonnaGetYouContext : DbContext
    {
        //Bellow we need to add the models for each remaining tables...        
        public DbSet<Band> Bands { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }

        //DbContext provided by EntityFramework Core that allows connection
        //between database models to tables. 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=localhost;database=RhythmsGonnaGetYouDB");
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            optionsBuilder.UseLoggerFactory(loggerFactory);
        }
    }
}