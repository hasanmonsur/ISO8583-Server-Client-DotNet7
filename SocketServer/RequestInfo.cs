using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SocketServer
{
    public class RequestInfo
    {
        public RequestInfo()
        {
            
        }

        public string headerCode { get; set; }
        public string macCode { get; set; }
        public string processCode { get; set; }

        public string stanId { get; set; }

        public string entryMode { get; set; }

        public string niiFunctionCode { get; set; }

        public string posConditionCode { get; set; }

        public string caTerminalID { get; set; }

        public string caId { get; set; }

    }
        
}
