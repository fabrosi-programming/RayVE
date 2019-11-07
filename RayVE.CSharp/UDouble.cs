using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayVE
{
    public struct UDouble
    {
        private double _value;

        public UDouble(double value)
            => _value = value < 0 ? 0 : value;

        public static implicit operator double(UDouble u)
            => u._value;

        public static implicit operator UDouble(double d)
            => new UDouble(d);
    }
}
