using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_01.DA.dbContext.PostgreSQL
{
    public class pgSqlDbContextFactory : dbContextFactory
    {
        public IConfiguration config  { get; set; }
        public string connectionStr;
        //public AppDbContext context;
        public pgSqlDbContextFactory(IConfiguration config)
        {
            this.config = config;
        }
        public pgSqlDbContextFactory(string connectionStr)
        {
            this.connectionStr = connectionStr;
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseNpgsql(connectionStr);
            var context = new AppDbContext(builder.Options);
            context.Database.EnsureDeleted();

            context.Database.EnsureCreated();
            context.Database.Migrate();

        }
        //public AppDbContext get_
        public AppDbContext get_db_context()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            if (config != null)
            {
                builder.UseNpgsql(config.GetSection("PostgreSQL").GetSection("ConnectionString").Value);
            }
            else
            {
                builder.UseNpgsql(connectionStr);
            }
            return new AppDbContext(builder.Options);
        }
        public void printName()
        {

        }
    }
}
