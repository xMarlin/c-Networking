using System.Net.Sockets;

namespace Networking.Connectivity;

public class NetworkClient
{
    public NetworkClient()
    {
    }
    public NetworkClient(Socket socket, NetworkServer server, int bufferSize)
    {
        Socket = socket;
        Server = server;
        Buffer = new byte[bufferSize];
    }

    public Socket Socket { get; set; }
    public NetworkServer Server { get; set; }
    public byte[] Buffer { get; set; }

    public void BeginReceive()
    {
        try
        {
            Socket.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, EndReceive, null);
        }
        catch (Exception)
        {
            Disconnect();
        }
    }
    public void EndReceive(IAsyncResult result)
    {
        try
        {
            var length = Socket.EndReceive(result, out var error);

            if (error != SocketError.Success)
                return;

            if (length > 0)
            {
                var received = new byte[length];
                Array.Copy(Buffer, received, length);
                Server.OnReceive(this, received);
            }
            else
            {
                Disconnect();
            }
        }
        catch (Exception)
        {
            Disconnect();
        }
    }


    public void BeginSend(byte[] buffer)
    {
        try
        {
            Socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, EndSend, null);
        }
        catch (Exception)
        {
            Disconnect();
        }
    }
    private void EndSend(IAsyncResult result)
    {
        try
        {
            Socket.EndSend(result, out _);
        }
        catch (Exception)
        {
            Disconnect();
        }
    }
    
    public void Disconnect()
    {
        Server.OnDisconnect.Invoke(this);
        Socket.Disconnect(false);
        Socket.Close();
    }
}