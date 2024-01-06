// See https://aka.ms/new-console-template for more information
using imohsenb.iso8583.builders;
using imohsenb.iso8583.entities;
using imohsenb.iso8583.enums;
using imohsenb.iso8583.utils;
using Newtonsoft.Json;
using SocketClient;
using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Serialization;


Console.WriteLine("Hello, World! Client");


try
{
    TcpClient client = new TcpClient("127.0.0.1", 8888);
    Console.WriteLine("Connected to the server...");

    NetworkStream stream = client.GetStream();

    var requestInfo = new RequestInfo();    

    // Create an ISO 8583 message
    ISOMessage iso = ISOMessageBuilder.Packer(VERSION.V1987)
                .NetworkManagement()
                .MTI(MESSAGE_FUNCTION.Request, MESSAGE_ORIGIN.Acquirer)
                .ProcessCode(requestInfo.processCode)
                .SetField(FIELDS.F11_STAN, requestInfo.stanId)
                .SetField(FIELDS.F22_EntryMode, requestInfo.entryMode)
                .SetField(FIELDS.F24_NII_FunctionCode, requestInfo.niiFunctionCode)
                .SetField(FIELDS.F25_POS_ConditionCode, requestInfo.posConditionCode)
                .SetField(FIELDS.F41_CA_TerminalID, requestInfo.caTerminalID)
                .SetField(FIELDS.F42_CA_ID, requestInfo.caId)
                .GenerateMac(data =>
                {
                    return StringUtil.HexStringToByteArray(requestInfo.macCode);
                })
                .SetHeader(requestInfo.headerCode)
                .Build();

    var message = iso.ToString();

    byte[] data = Encoding.ASCII.GetBytes(message);

    stream.Write(data, 0, data.Length);
    Console.WriteLine($"Sent data: {message}");

    // Receive response from the server
    byte[] responseBuffer = new byte[1024];
    int bytesRead = stream.Read(responseBuffer, 0, responseBuffer.Length);

    string responseData = Encoding.ASCII.GetString(responseBuffer, 0, bytesRead);
    Console.WriteLine($"Received response: {responseData}");

    // Close the connection
    client.Close();

}
catch (Exception e)
{
    Console.WriteLine($"Exception: {e.Message}");
}


