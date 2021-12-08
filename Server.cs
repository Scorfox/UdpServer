using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using UdpServer;

namespace UdpService
{
    public class Server : IServer
    {
        private UdpClient Sender { get; }
        private IPEndPoint IpEndPoint { get; }

        public Server(int serverPort, string serverAddress)
        {
            Sender = new UdpClient();
            IpEndPoint = new IPEndPoint(IPAddress.Parse(serverAddress), serverPort);
        }

        public void SendRequest()
        {
            try
            {
                while (true)
                {
                    var packet = PacketGenerator.GenerateData();
                    var byteMessage = SerializePacket(packet);
                    Sender.Send(byteMessage, byteMessage.Length, IpEndPoint);
                    Thread.Sleep(10000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Sender.Close();
            }
        }

        private static byte[] SerializePacket(Packet packet)
        {
            var packetSize = Marshal.SizeOf(packet);
            var packetData = new byte[packetSize];
            var handle = GCHandle.Alloc(packetData, GCHandleType.Pinned);
            Marshal.StructureToPtr(packet, handle.AddrOfPinnedObject(), false);
            handle.Free();
            return packetData;
        }
    }
}