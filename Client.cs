using System;
using System.Net;
using System.Net.Sockets;
using UdpService;
using System.Runtime.InteropServices;

namespace UdpServer
{
    public class Client : IClient
    {
        private UdpClient Receiver { get; set; }

        public Client(int listeningPort)
        {
            this.Receiver = new UdpClient(listeningPort);
        }   

        public void Receive()
        {
            IPEndPoint ipEndPoint = null;
            try
            {
                while (true)
                {
                    var data = Receiver.Receive(ref ipEndPoint);
                    var packet = BytesToPacket(data);
                    packet.Minute = packet.Minute % 2 == 0 ? (byte) 0 : (byte) 1;
                    if (packet.Color == 0)
                        Console.ForegroundColor = ConsoleColor.Black;
                    else if (packet.Color == 1)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (packet.Color == 2)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Принято: Минута: {packet.Minute} Результат 1: {packet.Result1} Результат 2: {packet.Result2}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Receiver.Close();
            }
        }

        private static Packet BytesToPacket(byte[] bytesPacket)
        {
            var bytesPacketSize = Marshal.SizeOf(typeof(Packet));
            var ptr = Marshal.AllocHGlobal(bytesPacketSize);
            try
            {
                Marshal.Copy(bytesPacket, 0, ptr, bytesPacketSize);
                return (Packet)Marshal.PtrToStructure(ptr, typeof(Packet));
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }
}