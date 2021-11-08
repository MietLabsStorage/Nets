using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace IcmpLib
{
    public class IcmpHeader
    {
        public byte Type { get; set; } = 0;

        public byte Code { get; set; } = 0;

        public ushort ControlSum { get; set; }

        public byte[] Rest { get; set; } = {0, 0, 0, 0};

        public static int TypeSize => (8 + 8 + 16 + 4 * 8) / 8;

        public byte[] Blob()
        {
            var blob = new List<byte>();
            blob.Add(Type);
            blob.Add(Code);
            blob.AddRange(BitConverter.GetBytes(ControlSum));
            blob.AddRange(Rest);
            return blob.ToArray();
        }

        public static ushort SolveControlSum(ushort[] buffer, int length)
        {
            ulong crc = 0;
            // Вычисление CRC 
            var ptr = 0;
            while (length > 1)
            {
                crc += buffer[ptr++];
                length -= sizeof(ushort);
            }

            if (length != 0) crc += (byte) buffer[ptr];

            // Закончить вычисления 
            crc = (crc >> 16) + (crc & 0xffff);
            crc += crc >> 16;

            return (ushort) ~crc;
        }

        public static byte[] SendIcmp(Socket s, IpHeader iph, IcmpHeader icmph, byte[] data)
        {
            var dataLength = 0;

            // Вычисление длин пакета и заголовка.
            var headerLength = (byte) TypeSize;
            var packetLength = (uint) (headerLength + dataLength);
            icmph.ControlSum = 0;

            var buffer = new byte[(int) packetLength];
            for (var i = 0; i < (int) packetLength; i++)
            {
                buffer[i] = 0x00;
            }

            // Копирование заголовка пакета в буфер ( CRC равно 0).
            for (var i = 0; i < headerLength; i++)
            {
                buffer[i] = icmph.Blob()[i];
            }

            // Вычисление CRC.
            icmph.ControlSum = SolveControlSum(buffer.Select(_ => (ushort) _).ToArray(), (int) packetLength);

            // Копирование заголовка пакета в буфер (CRC посчитана).
            for (var i = 0; i < headerLength; i++)
            {
                buffer[i] = icmph.Blob()[i];
            }

            var result = IpHeader.SendIp(s, iph, buffer, (int) packetLength, 0);

            return result;
        }
    }
}