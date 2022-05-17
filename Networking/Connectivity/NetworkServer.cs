using System.Net;
using System.Net.Sockets;

namespace Networking.Connectivity;

public class NetworkServer
{
    public NetworkServer(string ip, int port, 
        NetworkEvents.NetworkConnection onConnect,
        NetworkEvents.NetworkConnection onDisconnect, 
        NetworkEvents.NetworkReceive onReceive)
    {
        EndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        OnConnect = onConnect;
        OnDisconnect = onDisconnect;
        OnReceive = onReceive;
        Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket.Bind(EndPoint);
        Socket.Listen(5);
        BeginAccept();
    }

    public Socket Socket { get; set; }
    public EndPoint EndPoint { get; set; }

    public NetworkEvents.NetworkConnection OnConnect;
    public NetworkEvents.NetworkConnection OnDisconnect;
    public NetworkEvents.NetworkReceive OnReceive;

    public void BeginAccept()
    {
        Socket.BeginAccept(EndAccept, null);
    }

    public void EndAccept(IAsyncResult result)
    {
        try
        {
            var clientSocket = Socket.EndAccept(result);
            var client = new NetworkClient(clientSocket, this, 1024);
            OnConnect.Invoke(client);
            BeginAccept();
        }
        catch (Exception)
        {
            BeginAccept();
        }
    }
}