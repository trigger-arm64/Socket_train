using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Socket_Server
{
    internal class server
    {
        static void Main(string[] args)
        {   //创建socket实例
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


            //用户输入ip地址和端口号
            Console.WriteLine("请输入服务端IP：\n");
            IPAddress ipAddress = IPAddress.Parse(Console.ReadLine());
            Console.WriteLine("请输入服务端Port：\n");
            int Port = int.Parse(Console.ReadLine());
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, Port);


            //socket绑定ip地址和端口号
            socket.Bind(localEndPoint);


            Console.WriteLine("正在等待链接...");
            socket.Listen(10);

            int couter = 0;
            while (true)
            {
                // 接受连接
                Socket handler = socket.Accept();
                if(couter<1)
                Console.WriteLine($"Connected: {handler.RemoteEndPoint}");

                couter++;

                // 读取数据
                byte[] buffer = new byte[1024];
                int bytesRec = handler.Receive(buffer);
                string data = Encoding.ASCII.GetString(buffer, 0, bytesRec);
                Console.WriteLine($"Received: {data}");

                // 发送数据
                    byte[] msg = Encoding.ASCII.GetBytes(Console.ReadLine());
                    handler.Send(msg);
              
                // 关闭连接
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
        }
    }
}
