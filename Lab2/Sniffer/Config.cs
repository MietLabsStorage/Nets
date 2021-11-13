using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniffer
{
    public class Config
    {
        public bool CheckTcp { get; set; }
        public bool CheckUdp { get; set; }
        public bool CheckIcmp { get; set; }
        public bool CheckIp { get; set; }
    }
}
