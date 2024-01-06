using imohsenb.iso8583.utils;

namespace SocketServer
{

    public class StringUtilConvert
    {

        //byte[] sampleBytes = new byte[] { 96, 0, 7, 128 };
        //string sampleHex = "60000780";

        public byte[] HexToByte(string sampleHex)
        {
            byte[] b = StringUtil.HexStringToByteArray(sampleHex);
            
            return b;
        }

        public string ByteToHex(byte[] sampleBytes)
        {
            string hex = StringUtil.FromByteArray(sampleBytes);
            
            return hex;
        }
    }
}
