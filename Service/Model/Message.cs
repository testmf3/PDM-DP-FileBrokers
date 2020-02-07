using System;
using System.Collections.Generic;
using System.Text;

namespace ARM.PDM.Service.Model
{
    public class Message
    {
        public string applicationName { get; set; }
        public string date { get; set; }
        public int data { get; set; }
        public string sProject { get; set; }
        public string sStage { get; set; }
    }
}

