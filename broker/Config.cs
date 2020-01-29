using System;
using System.Collections.Generic;
using System.Text;

namespace broker
{
    class Config
    {
        public string applicationName { get; set; }
        public string type { get; set; }

        public override string ToString()
        {
            return "\nname: " + applicationName + "\ntype: " + type;
        }
    }
}
