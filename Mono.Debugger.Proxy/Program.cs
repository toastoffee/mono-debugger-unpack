// See https://aka.ms/new-console-template for more information

using Mono.Debugger.Unpack;

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class SocketProxy
{
    static async Task Main(string[] args)
    {
        var handler = DebuggerPacketParamsHandlerGetter.GetPacketParamsHandler(CommandSet.VirtualMachine, 11, DebuggerPacketType.Command);
        Console.WriteLine(handler == null);
        
        // 启动清屏监听任务
        _ = Task.Run(KeyClearLoop);

        // 代理监听的端口
        int proxyPort = 12345;
        // 目标Socket的地址和端口
        string targetIp = "127.0.0.1";
        int targetPort = 56628;

        // 创建代理监听器
        TcpListener proxyListener = new TcpListener(IPAddress.Any, proxyPort);
        proxyListener.Start();
        Console.WriteLine($"Proxy listening on port {proxyPort}...");

        while (true)
        {
            // 等待客户端连接
            TcpClient client = await proxyListener.AcceptTcpClientAsync();
            Console.WriteLine("Client connected to proxy!");

            // 连接到目标Socket
            TcpClient targetClient = new TcpClient(targetIp, targetPort);
            Console.WriteLine("Connected to target!");

            // 启动双向数据转发
            _ = ForwardData(client, targetClient, "Client -> Target");
            _ = ForwardData(targetClient, client, "Target -> Client");
        }
    }

    static async Task ForwardData(TcpClient source, TcpClient destination, string label)
    {
        byte[] buffer = new byte[1024];
        NetworkStream sourceStream = source.GetStream();
        NetworkStream destinationStream = destination.GetStream();

        try
        {
            while (true)
            {
                // 读取数据
                int bytesRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0) break; // 连接关闭


                var debuggerPacket = DebuggerPacket.ConvertFrom(buffer, bytesRead);
                debuggerPacket.LogPacket();

                
                // 转发数据
                await destinationStream.WriteAsync(buffer, 0, bytesRead);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            source.Close();
            destination.Close();
        }
    }

    static byte[] ReplaceStringInBuffer(byte[] buffer, string stringA, string stringB)
    {
        // 将 buffer 转换为字符串
        string data = Encoding.UTF8.GetString(buffer);

        // 替换字符串
        data = data.Replace(stringA, stringB);

        // 将字符串转换回 byte[]
        return Encoding.UTF8.GetBytes(data);
    }

    static void KeyClearLoop()
    {
        while (true)
        {
            var keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.C)
            {
                Console.Clear();
                Console.WriteLine("[Console cleared, press 'C' again to clear.]");
            }
        }
    }
}