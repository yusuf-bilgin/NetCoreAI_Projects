using Microsoft.EntityFrameworkCore;
using NetCoreAI.Project01_ApiDemo.Entities;

namespace NetCoreAI.Project01_ApiDemo.Context
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=YUSUF_BILGIN\\SQLEXPRESS;" +
                "initial catalog = ApiAIDb;" +
                "integrated security = true;" +
                "trust server certificate = true;");
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
