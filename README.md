# ISO-8583 .Net CORE 7 using socket programming
use server & client socket programming for data handsheck. also use ISO8583 message generate & parse.

### Usage

# HELP from ISO-8583 .Net Lib
https://github.com/imohsenb/ISO8583-Message-Client-DotNet.git


#### ISO8583 Message Packer and Unpakcer with ISOClient for communication with iso server 


## ISOMessage
#### Create and pack an ISO message
To create an ISO message you must use ISOMessageBuilder which produce iso message for you:

```csharp

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
				
                
```

#### Unpack a buffer and parse it to ISO message
For unpacking a buffer received from server you need to use `ISOMessageBuilder.Unpacker()`:

```csharp
ISOMessage isoMessage = ISOMessageBuilder.Unpacker()
                                    .SetMessage(SAMPLE_HEADER + SAMPLE_MSG)
                                    .Build();
```
#### Working with ISOMessage object
ISOMessage object has multiple method provide fields, message body, header and ...
for security reason they will return byte array exept `.ToString` and `.GetStringField` method, because Strings stay alive in memory until a garbage collector will come to collect that. but you can clear byte or char arrays after use and calling garbage collector is not important anymore.
If you use Strings, taking memory dumps will be dangerous.
```csharp
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
```
## ISOClient
#### Sending message to iso server
Sending message to iso server and received response from server can be done with ISOClient in many ways:
```csharp
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
```



License
-------

