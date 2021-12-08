using System.Threading;
using UdpServer;

namespace UdpService
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var myUdpClient = new Client(8000);
            var myUdpServer = new Server(8000, "127.0.0.1");
            var threadServerReceive = new Thread(myUdpClient.Receive);
            threadServerReceive.Start();
            myUdpServer.SendRequest();
        }
    }
}
