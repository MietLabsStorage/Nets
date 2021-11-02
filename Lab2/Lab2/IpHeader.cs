using System;

namespace Lab2
{
    public class IpHeader
    {
        byte VerIhl { get; set; } // Длина заголовка (4 бита)  (измеряется в словах по 32 бита) + Номер версии протокола (4 бита)
        byte Tos { get; set; }  // Тип сервиса 
        UInt16 Tlen { get; set; } // Общая длина пакета 
        UInt16 Id { get; set; } // Идентификатор пакета
        UInt16 FlagsFo { get; set; } // Управляющие флаги (3 бита) + Смещение фрагмента (13 бит)
        byte Ttl { get; set; } // Время жизни пакета
        byte Proto { get; set; } // Протокол верхнего уровня 
        UInt16 Crc { get; set; } // CRC заголовка
        UInt32 SrcAddr { get; set; } // IP-адрес отправителя
        UInt32 DstAddr { get; set; } // IP-адрес получателя

    }
}