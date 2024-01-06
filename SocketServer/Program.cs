// See https://aka.ms/new-console-template for more information
using System.Net.Sockets;
using System.Net;
using System.Text;
using imohsenb.iso8583.entities;
using imohsenb.iso8583.builders;
using SocketServer;
using NetCore8583;
using imohsenb.iso8583.enums;
using System.Text.Json.Serialization;
using System.Net.Http.Json;
using System.Xml;
using Newtonsoft.Json;

Console.WriteLine("Hello, World! Server");


TcpListener server = null;

try
{
    // Set the TcpListener on a specific port
    int port = 8888;
    IPAddress localAddr = IPAddress.Parse("127.0.0.1");

    // TcpListener
    server = new TcpListener(localAddr, port);

    // Start listening for client requests
    server.Start();

    Console.WriteLine($"Server started on {localAddr}:{port}");

    // Enter the listening loop
    while (true)
    {
        Console.WriteLine("Waiting for a connection...");

        // Perform a blocking call to accept requests
        TcpClient client = server.AcceptTcpClient();
        Console.WriteLine($"Connected to {((IPEndPoint)client.Client.RemoteEndPoint).Address}");

        // Handle the client in a separate thread or asynchronously
        HandleClient(client);
    }
}
catch (Exception e)
{
    Console.WriteLine($"Exception: {e.Message}");
}
finally
{
    // Stop listening for new clients
    server?.Stop();
}

static void HandleClient(TcpClient client)
{
    NetworkStream stream = client.GetStream();
    byte[] buffer = new byte[1024];
    int bytesRead;

    try
    {
        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            var requestInfo = new RequestInfo();

            ISOMessage iso = ISOMessageBuilder.Unpacker()
                .SetMessage(data)
                .Build();

            if (data == iso.ToString())
            {
                requestInfo.headerCode= data.Substring(0,10);
            }

            requestInfo.processCode = iso.GetStringField(FIELDS.F3_ProcessCode, true);

            // Extract header and data from the serialized ISO 8583 message

            requestInfo.stanId = iso.GetStringField(FIELDS.F11_STAN, true);
            requestInfo.entryMode = iso.GetStringField(FIELDS.F22_EntryMode, true);
            requestInfo.niiFunctionCode = iso.GetStringField(FIELDS.F24_NII_FunctionCode, true);
            requestInfo.posConditionCode = iso.GetStringField(FIELDS.F25_POS_ConditionCode, true);
            requestInfo.caTerminalID = iso.GetStringField(FIELDS.F41_CA_TerminalID, true);
            requestInfo.caId = iso.GetStringField(FIELDS.F42_CA_ID, true);
   

            Console.WriteLine($"Received data: {data}");

            var dataReturn= JsonConvert.SerializeObject(requestInfo); 
            //data = data + " Good Morning !!";
            Console.WriteLine($"parse  data: {dataReturn}");


            // Echo the data back to the client
            byte[] responseBuffer = Encoding.ASCII.GetBytes(data);
            stream.Write(responseBuffer, 0, responseBuffer.Length);
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"Exception: {e.Message}");
    }
    finally
    {
        client.Close();
    }
}