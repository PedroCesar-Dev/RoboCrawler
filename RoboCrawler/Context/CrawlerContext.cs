using Microsoft.EntityFrameworkCore;

public class CrawlerContext : DbContext
{
    public DbSet<Log> Logs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=PC03LAB2512; Database=Webscraping; User Id=sa; Password=senai.123;") ;
    }
}
