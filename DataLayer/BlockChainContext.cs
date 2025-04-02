using Microsoft.EntityFrameworkCore;
using ModelLayer;
using System.Threading.Tasks;

namespace DataLayer
{
    public class BlockChainContext : DbContext 
    {
        private const string MYCONN = "server=127.0.0.1; port=3333; uid=root; pwd=root; database=BlockChainInfo;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var mySQVersion = new MySqlServerVersion(new Version(10, 4, 17));
            optionsBuilder.UseMySql(MYCONN, mySQVersion); 
        }

        public DbSet<TokenModel> token { get; set; }
    }
}