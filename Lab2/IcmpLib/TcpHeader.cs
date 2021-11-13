using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IcmpLib
{
    public class TcpHeader
    {
        public UInt16 src_port;    // Порт отправителя
        public UInt16 dst_port;    // Порт получателя 
        public UInt32 seq_n;     // Номер очереди
        public UInt32 ack_n;     // Номер подтверждения
        public byte offset;       // Смещение данных (4 бита) 
                                  // + Зарезервировано (4 бита)
        public byte flags;        // Зарезервировано (2 бита) 
                                  // + Флаги (6 бит)
        public UInt16 win;     // Размер окна
        public UInt16 crc;     // Контрольная сумма заголовка
        public UInt16 padding; // Дополнение до 20 байт

        public TcpHeader() { }

        public TcpHeader(byte[] byBuffer, int nReceived)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream(byBuffer, 0, nReceived);
                BinaryReader binaryReader = new BinaryReader(memoryStream);

                src_port = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                dst_port = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                seq_n = (uint)(binaryReader.ReadInt32());
                ack_n = (uint)(binaryReader.ReadInt32());
                offset = binaryReader.ReadByte();
                flags = binaryReader.ReadByte();
                win = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                crc = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                padding = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
