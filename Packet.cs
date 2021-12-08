using System.Runtime.InteropServices;

namespace UdpServer
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Packet
    {
        [MarshalAs(UnmanagedType.I1)]
        public byte Minute;

        [MarshalAs(UnmanagedType.I1)]
        public readonly byte Color;

        [MarshalAs(UnmanagedType.I2)]
        public readonly short Result1;

        [MarshalAs(UnmanagedType.I4)]
        public readonly int Result2;

        public Packet(byte minute, byte color, short result1, int result2)
        {
            Minute = minute;
            Color = color;
            Result1 = result1;
            Result2 = result2;
        }
    }
}
