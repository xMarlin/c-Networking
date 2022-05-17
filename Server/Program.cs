using Networking.Connectivity;

namespace Server;

class Program
{
    public static NetworkServer NetworkServer { get; set; }
    static void Main(string[] args)
    {
        // var array = new byte[60];
        // var wrtr = new BinaryWriter(new MemoryStream(array));
        // wrtr.Write((ushort) (ushort.MaxValue - 10));
        
        
        NetworkServer = new NetworkServer("192.168.1.8", 9001, OnConnect, OnDisconnect, OnReceive);
        while (true)
        {
            Console.ReadKey();
        }
    }

    private static void OnReceive(NetworkClient client, byte[] buffer)
    {
        Console.WriteLine(nameof(OnReceive));
        
        // var rdr = new BinaryReader(new MemoryStream(buffer));
        // var firstUshort = rdr.ReadUInt16();
        // var secUshort = rdr.ReadUInt16();
        // var uInt32 = rdr.ReadUInt32();
        // rdr.BaseStream.Seek(20, SeekOrigin.Begin);
        // uInt32 = rdr.ReadUInt32();
        // rdr.ReadUInt32();
        // uInt32 = rdr.ReadUInt32();
    }

    private static void OnDisconnect(NetworkClient client)
    {
        Console.WriteLine(nameof(OnDisconnect));
    }

    private static void OnConnect(NetworkClient client)
    {
        client.BeginReceive();
        Console.WriteLine(nameof(OnConnect));
    }
}