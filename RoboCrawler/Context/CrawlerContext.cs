using Microsoft.EntityFrameworkCore;

public class CrawlerContext : DbContext
{
    public DbSet<Log> Logs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=PC03LAB2511\\SENAI; database=scrap_db; User Id=sa; Password=senai.123");
    }
}
