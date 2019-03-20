using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Data.Models.Data
{
    class AbacasXDbInitializer : DropCreateDatabaseIfModelChanges<AbacasXDbContext>
    {

    }
}
