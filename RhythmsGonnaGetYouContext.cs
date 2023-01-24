using System;


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
    public DbSet<Band> Bands { get; set; };

}
