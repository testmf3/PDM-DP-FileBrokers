using System;
using System.Collections.Generic;
using System.Text;

namespace ARM.PDM.Service.Model
{
     public class DataStructCreatorConfig
    {
        public Configset configset { get; set; }
        public string[] exclude { get; set; }
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
    }

    public class Configset
    {
        public Datastructure DataStructure { get; set; }
    }

    public class Datastructure
    {
        public string Level { get; set; }
        public string[] Value { get; set; }
        public Items Items { get; set; }
    }

    public class Items
    {
        public string Level { get; set; }
        public string[] Value { get; set; }
        public Items Items_ { get; set; }
    }

    public class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string Microsoft { get; set; }
        public string MicrosoftHostingLifetime { get; set; }
    }
}




