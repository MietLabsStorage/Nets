using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IcmpLib
{
    public class UdpHeader
    {
        public UInt16 SrcPort { get; set; }    // номер порта отправителя 
        public UInt16 DstPort { get; set; }    // номер порта получателя 
        public UInt16 Length { get; set; }  // длина датаграммы 
        public UInt16 Crc { get; set; }		// контрольная сумма заголовка

        public UdpHeader() { }

        public UdpHeader(byte[] byBuffer, int nReceived)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream(byBuffer, 0, nReceived);
                BinaryReader binaryReader = new BinaryReader(memoryStream);

                SrcPort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                DstPort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                Length = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                Crc = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
