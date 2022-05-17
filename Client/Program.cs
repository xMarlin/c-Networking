using System.Net;
using System.Net.Sockets;

namespace Client;

class Program
{
    static void Main(string[] args)
    {
        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect("192.168.1.8", 9001);
        if (socket.Connected)
        {
            var buffer = new byte[60];
            Random.Shared.NextBytes(buffer);
            socket.Send(buffer, SocketFlags.None);
            Console.ReadKey();
            socket.Send(buffer, SocketFlags.None);
            Console.ReadKey();
        }
    }
}