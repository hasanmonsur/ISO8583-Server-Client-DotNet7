using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SocketClient
{
    public class RequestInfo
    {
        public RequestInfo()
        {
            headerCode = "1234567890";
            macCode = "70A30640CC76DD8B";  //"AB6A53FC655F1487"  VeiryAnsi99Pad()
            processCode = "930000";
            stanId = "123456";
            entryMode = "123F";
            niiFunctionCode = "123F";
            posConditionCode = "12";
            caTerminalID = "12345678";
            caId = "123456789012345";
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
