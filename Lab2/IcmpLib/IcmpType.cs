namespace IcmpLib
{
    /// <summary>
    ///     тип ICMP пакета
    /// </summary>
    public enum IcmpType
    {
        IcmpEchoReply = 0,
        IcmpUnreachable = 3,
        IcmpQuench = 4,
        IcmpRedirect = 5,
        IcmpEcho = 8,
        IcmpTime = 11,
        IcmpParameter = 12,
        IcmpTimestamp = 13,
        IcmpTimestampReply = 14,
        IcmpInformation = 15,
        IcmpInformationReply = 16
    }
}