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
        public pgSqlDbContextFactory(IConfiguration config)
        {
            this.config = config;
        }
        public AppDbContext get_db_context()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseNpgsql(config.GetSection("PostgreSQL").GetSection("ConnectionString").Value);
            return new AppDbContext(builder.Options);
        }

    }
}
