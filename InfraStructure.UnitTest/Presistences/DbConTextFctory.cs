using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Infraction.Presistences;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.UnitTest.Presistences
{
    public class DbConTextFctory
    {
        public static Databasese Create()
        {
            DbContextOptions<Databasese> options = new DbContextOptionsBuilder<Databasese>()
    .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var context = new Databasese(options);
            return context;
        }
    }
}
