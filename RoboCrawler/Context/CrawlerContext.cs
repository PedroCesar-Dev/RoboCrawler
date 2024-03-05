using Microsoft.EntityFrameworkCore;

public class CrawlerContext : DbContext
{
    public DbSet<Log> Logs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"DataSource=SQL9001.site4now.net; Initial Catalog=db_aa5b20_apialmoxarifado; User id=db_aa5b20_apialmoxarifado_admin; Password=master@123;") ;
    }
}
