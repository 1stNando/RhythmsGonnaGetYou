using System;
using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
    class RhythmsGonnaGetYouContext : DbContext
    {
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        //     optionsBuilder.UseLoggerFactory(loggerFactory);

        //     optionsBuilder.UseNpgsql("server=localhost;database=RhythmsGonnaGetYouDB");
    }
    //DbContext provided by EF Core that allows connection
    //between database models to tables. 
    public DbSet<Band> Bands { get; set; }

    //Bellow we need to add the models for each remaining tables...

}
