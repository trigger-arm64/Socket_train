using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Socket_Client
{
    internal class client
    {
        static void Main(string[] args)
        {
            // 创建一个Socket并连接到服务器
            Console.WriteLine("请输入目标端IP地址");
            IPAddress ipAddress = IPAddress.Parse(Console.ReadLine());
            Console.WriteLine("请输入目标端端端口号");
            int Port = int.Parse(Console.ReadLine());
            
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, Port);

            while (true)
            {
                Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sender.Connect(remoteEP);

            
            // 发送数据
            
                Console.WriteLine("向服务端发送数据，发送“quit！退出”：\n");
                string message = Console.ReadLine();
                byte[] msg = Encoding.ASCII.GetBytes(message);
                int bytesSent = sender.Send(msg);
                Console.WriteLine($"Sent: {message}");
                if (message == "quit!")
                {
                    break;
                }

                // 接受数据
                byte[] buffer = new byte[1024];
                int bytesRec = sender.Receive(buffer);
                string data = Encoding.ASCII.GetString(buffer, 0, bytesRec);
                Console.WriteLine($"Received: {data}");

                // 关闭连接
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }

                Console.ReadKey();
        }
    }
}
