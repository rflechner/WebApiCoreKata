using Microsoft.EntityFrameworkCore;
using WebApiCoreKata.Persistence.SqlServer.Dto;

namespace WebApiCoreKata.Persistence.SqlServer
{
  public class KataContext : DbContext
  {
    public DbSet<CustomerDto> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(@"Server=BIG_ROMCY\SQLEXPRESS;Database=WebApiCoreKata;Trusted_Connection=True;");
    }
  }
}