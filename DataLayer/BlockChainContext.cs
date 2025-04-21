using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ModelLayer;

namespace DataLayer
{
    public class BlockChainContext : DbContext 
    {   
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            var connStr = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(connStr, ServerVersion.AutoDetect(connStr)); 
        }

        public DbSet<TokenModel> token { get; set; }
    }
}