using System;

namespace UdpServer
{
    public static class PacketGenerator
    {
        private static readonly Random Rand = new Random();
        public static Packet GenerateData()
        {
            var structExRand = new Packet(Convert.ToByte(DateTime.Now.Minute), Convert.ToByte(Rand.Next(0, 4)), 
                Convert.ToInt16(Rand.Next(-50,51)), Convert.ToInt32(Rand.Next(0, 255)));
            return structExRand;
        }
    }
}
