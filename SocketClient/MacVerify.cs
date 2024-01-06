using imohsenb.iso8583.crypto;
using imohsenb.iso8583.utils;

namespace SocketClient
{
    public class MacVerify
    {
        //string _message1 = "4E6F77206973207468652074696D6520666F7220616C6C20";
        //string _message2 = "4E6F77206973207468652074696D6520666F7220616C6C20ABCD";

        string _key1 = "0123456789ABCDEF";
        string _key2 = "FEDCBA9876543210";

        public bool VeiryAnsi99Pad(string message)
        {
            var data = StringUtil.HexStringToByteArray(message);
            var key = StringUtil.HexStringToByteArray(_key1);
            var expected = StringUtil.HexStringToByteArray("70A30640CC76DD8B");
            var mac = new Ansi99MacGenerator(key).Generate(data);

            return (expected== mac);
        }

       public bool VeiryAnsi99NoPad(string message)
        {
            var data = StringUtil.HexStringToByteArray(message);
            var key = StringUtil.HexStringToByteArray(_key1);
            var expected = StringUtil.HexStringToByteArray("36611DBB2D0AC1E6");
            var mac = new Ansi99MacGenerator(key).Generate(data);

            return (expected == mac);
        }


        public bool VeiryAnsi919Pad(string message)
        {
            var data = StringUtil.HexStringToByteArray(message);
            var key1 = StringUtil.HexStringToByteArray(_key1);
            var key2 = StringUtil.HexStringToByteArray(_key2);
            var expected = StringUtil.HexStringToByteArray("A1C72E74EA3FA9B6");
            var mac = new Ansi919MacGenerator(key1, key2).Generate(data);

            return (expected == mac);
        }
        
        public bool VeiryAnsi919NoPad(string message)
        {
            var data = StringUtil.HexStringToByteArray(message);
            var key1 = StringUtil.HexStringToByteArray(_key1);
            var key2 = StringUtil.HexStringToByteArray(_key2);
            var expected = StringUtil.HexStringToByteArray("1C050879D95816B8");
            var mac = new Ansi919MacGenerator(key1, key2).Generate(data);

            return (expected == mac);
        }
    }
}
