using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Web_Proxy.Models
{   public class IPInfo
    {       
        public string name { get; set; }
        public string DisplayCaption { get; internal set; }
        public string IPAddress { get; internal set; }
        public OperationalStatus OptStatus { get; internal set; }
    }
}
