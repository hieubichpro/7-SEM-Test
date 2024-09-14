using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace lab_01.DA.dbContext.InMemory
{
    public class InMemoryDbContextFactory : dbContextFactory
    {
        public string name { get; set; }
        public InMemoryDbContextFactory() 
        {
            name = Guid.NewGuid().ToString();
        }
        public AppDbContext get_db_context()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase(name);
            return new AppDbContext(builder.Options);
        }
    }
}
