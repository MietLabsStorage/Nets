using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Lab2
{
    public class IcmpHeader
    {
        public byte Byte { get; set; }

        public byte Code { get; set; }

        public UInt16 ControlSum { get; set; }

        public static int TypeSize => (8 + 8 + 16) / 8;

        public byte[] Blob()
        {
            var blob = new List<byte>();
            blob.Add(Byte);
            blob.Add(Code);
            blob.AddRange(BitConverter.GetBytes(ControlSum));
            return blob.ToArray();
        }

        public static UInt16 SolveControlSum(UInt16[] buffer, int length)
        {
            UInt64 crc = 0;
            // Вычисление CRC 
            var ptr = 0;
            while (length > 1)
            {
                crc += buffer[ptr++];
                length -= sizeof(UInt16);
            }

            if (length != 0)
            {
                crc += (byte) buffer[ptr];
            }

            // Закончить вычисления 
            crc = (crc >> 16) + (crc & 0xffff);
            crc += (crc >> 16);

            return (UInt16) (~crc);
        }

        public static byte[] SendIcmp(Socket s, IpHeader iph, IcmpHeader icmph, byte[] data)
        {
            int dataLength = 0;

            // Вычисление длин пакета и заголовка.
            byte headerLength = (byte) IcmpHeader.TypeSize;
            UInt32 packetLength = (uint) ((int) headerLength + dataLength);
            icmph.ControlSum = 0;

            byte[] buffer = new byte[(int) packetLength];
            for (int i = 0; i < (int) packetLength; i++)
            {
                buffer[i] = (byte) 0x00;
            }

            // Копирование заголовка пакета в буфер ( CRC равно 0).
            for (int i = 0; i < headerLength; i++)
            {
                buffer[i] = (byte) icmph.Blob()[i];
            }

            // Вычисление CRC.
            icmph.ControlSum = IcmpHeader.SolveControlSum(buffer.Select(_ => (UInt16) _).ToArray(), (int) packetLength);

            // Копирование заголовка пакета в буфер (CRC посчитана).
            for (int i = 0; i < headerLength; i++)
            {
                buffer[i] = (byte) icmph.Blob()[i];
            }

            return buffer;
        }
    }
}
