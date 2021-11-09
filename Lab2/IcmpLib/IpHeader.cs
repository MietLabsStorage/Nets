using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace IcmpLib
{
    public class IpHeader
    {
        public byte VerIhl { get; set; } // Длина заголовка (4 бита)  (измеряется в словах по 32 бита) + Номер версии протокола (4 бита)
        public byte Tos { get; set; } // Тип сервиса 
        public ushort Tlen { get; set; } // Общая длина пакета 
        public ushort Id { get; set; } // Идентификатор пакета
        public ushort FlagsFo { get; set; } // Управляющие флаги (3 бита) + Смещение фрагмента (13 бит)
        public byte Ttl { get; set; } // Время жизни пакета
        public byte Proto { get; set; } // Протокол верхнего уровня 
        public ushort Crc { get; set; } // CRC заголовка
        public uint SrcAddr { get; set; } = 16777343; // IP-адрес отправителя
        public uint DstAddr { get; set; } = 16777343; // IP-адрес получателя

        public byte[] Rest { get; set; }

        public static int TypeSize => (8 + 8 + 16 + 16 + 16 + 8 + 8 + 16 + 32 + 32) / 8;

        public byte[] Blob()
        {
            var blob = new List<byte>();
            blob.Add(VerIhl);
            blob.Add(Tos);
            blob.AddRange(BitConverter.GetBytes(Tlen));
            blob.AddRange(BitConverter.GetBytes(Id));
            blob.AddRange(BitConverter.GetBytes(FlagsFo));
            blob.Add(Ttl);
            blob.Add(Proto);
            blob.AddRange(BitConverter.GetBytes(Crc));
            blob.AddRange(BitConverter.GetBytes(SrcAddr));
            blob.AddRange(BitConverter.GetBytes(DstAddr));
            return blob.ToArray();
        }

        public IpHeader(){}

        public IpHeader(byte[] byBuffer, int nReceived)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream(byBuffer, 0, nReceived);
                BinaryReader binaryReader = new BinaryReader(memoryStream);

                VerIhl = binaryReader.ReadByte();
                Tos = binaryReader.ReadByte();
                Tlen = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                Id = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                FlagsFo = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                Ttl = binaryReader.ReadByte();
                Proto = binaryReader.ReadByte();
                Crc = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                SrcAddr = (uint)(binaryReader.ReadInt32());
                DstAddr = (uint)(binaryReader.ReadInt32());
                var byHeaderLength = VerIhl;
                byHeaderLength <<= 4;
                byHeaderLength >>= 4;
                byHeaderLength *= 4;

                Array.Copy(byBuffer, byHeaderLength, Rest, 0, Tlen - byHeaderLength);
            }
            catch (Exception)
            {
                // ignored
            }
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

        public static byte[] SendIp(Socket s, IpHeader iph, byte[] data, int dataLength, ushort dstPortRaw)
        {
            //sockaddr_in target;
            byte headerLength;
            uint packetLength;
            //memset(&target, 0, sizeof (target));
            //target.sin_family = AF_INET; 
            //target.sin_addr.s_addr = iph.dst_addr; 
            //target.sin_port = dstPortRaw; 

            // Вычисление длины и заголовка пакета 
            headerLength = (byte) TypeSize;
            packetLength = (uint) (headerLength + dataLength);

            // Установка CRC. 
            iph.Crc = 0;

            // Заполнение некоторых полей заголовка IP
            iph.VerIhl = 0; //RS_IP_VERSION;

            // Если длина пакета не задана, то длина пакета 
            // приравнивается к длине заголовка 
            if ((iph.VerIhl & 0x0F) == 0) iph.VerIhl = (byte) (iph.VerIhl | (0x0F & (headerLength / 4)));

            var buffer = new byte[(int) packetLength];
            for (var i = 0; i < (int) packetLength; i++)
            {
                buffer[i] = 0x00;
            }

            // Копирование заголовка пакета в буфер ( CRC равно 0). 
            for (var i = 0; i < headerLength; i++)
            {
                buffer[i] = iph.Blob()[i];
            }

            // Копирование данных в буфер 
            /* if (data) memcpy(buffer + headerLength, data,
                 dataLength);*/
            if (data != null && dataLength != 0)
            {
                for (var i = headerLength; i < headerLength + dataLength; i++)
                {
                    buffer[i] = data[i - headerLength];
                }
            }

            //Вычисление CRC. 
            iph.Crc = SolveControlSum(buffer.Select(_ => (ushort) _).ToArray(), (int) packetLength);

            // Копирование заголовка пакета в буфер (CRC посчитана). 
            for (var i = 0; i < headerLength; i++)
            {
                buffer[i] = iph.Blob()[i];
            }

            return buffer;
        }
    }
}