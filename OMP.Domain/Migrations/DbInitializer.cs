using OMP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Domain.Migrations
{
    public class DbInitializer : DropCreateDatabaseAlways<OMPContext>
    {
        protected override void Seed(OMPContext context)
        {
            // seed database here
        }
    }
}
