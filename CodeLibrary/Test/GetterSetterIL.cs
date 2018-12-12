using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibrary.Test
{
    public struct GetterSetterIL
    {

        public GetterSetterIL(string blah)
        {
            Blah = blah;
        }

        public string Blah { get; }

    }
}
