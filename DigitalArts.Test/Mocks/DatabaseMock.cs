using DigitalArts.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalArts.Test.Mocks
{
    public static class DatabaseMock
    {
        public static DigitalArtsDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<DigitalArtsDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new DigitalArtsDbContext(dbContextOptions);
            }
        }
    }
}
