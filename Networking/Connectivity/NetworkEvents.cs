namespace Networking.Connectivity;

public class NetworkEvents
{
    public delegate void NetworkConnection(NetworkClient client);
    public delegate void NetworkReceive(NetworkClient client, byte[] buffer);
}