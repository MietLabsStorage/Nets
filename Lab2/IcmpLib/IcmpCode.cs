namespace IcmpLib
{
    /// <summary>
    ///     ICMP коды для ICMP типа ICMP_UNREACHABLE
    /// </summary>
    public enum IcmpUnreachableCode
    {
        IcmpUnreachableNet = 0,
        IcmpUnreachableHost = 1,
        IcmpUnreachableProtocol = 2,
        IcmpUnreachablePort = 3,
        IcmpUnreachableFragmentation = 4,
        IcmpUnreachableSource = 5,
        IcmpUnreachableSize = 8
    }

    /// <summary>
    ///     ICMP коды для ICMP типа ICMP_TIME
    /// </summary>
    public enum IcmpTimeCode
    {
        IcmpTimeTransit = 0,
        IcmpTimeFragment = 1
    }

    /// <summary>
    ///     ICMP коды для ICMP типа ICMP_REDIRECT
    /// </summary>
    public enum IcmpRedirectCode
    {
        IcmpRedirectNetwork = 0,
        IcmpRedirectHost = 1,
        IcmpRedirectServiceNetwork = 2,
        IcmpRedirectServiceHost = 3
    }
}