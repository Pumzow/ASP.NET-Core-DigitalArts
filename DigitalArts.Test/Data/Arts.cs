using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalArts.Test.Data
{
    public static class Arts
    {
        public static IEnumerable<Arts> TenArts
            => IEnumerable.Range(0, 10).Select(async => new Arts { });
    }
}
