using lab_01.DA.dbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_01.DA.dbContext
{
    public interface dbContextFactory
    {
        AppDbContext get_db_context();
    }
}
