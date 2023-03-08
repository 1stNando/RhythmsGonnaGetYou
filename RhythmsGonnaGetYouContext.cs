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

        //DbContext is provided by EntityFramework Core and allows connection
        //between database models to tables postgres.  

        //We need to define a method required by EF Core that will configure our connection
        // to the database.
        //
        // DbContextOptionsBuilder is provided to us. We then tell that object
        // we want to connect to a postgres database named BeatBoxStudio on
        // our local machine.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {   //This is where the postgres database generated in your localhost database!!!!!here its "RhythmsGonnaGetYouDB"!!!!
            optionsBuilder.UseNpgsql("server=localhost;database=RhythmsGonnaGetYouDB");
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            optionsBuilder.UseLoggerFactory(loggerFactory);
        }
    }
}