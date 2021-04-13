using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public enum AsservissementMode
    {
        Disabled,
        Polar,
        Independant,
    }

    public class StateData
    {
        public UInt32 timestamp { get; set; }
        public float unprocessedValue { get; set; }
    }
}
