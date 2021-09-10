namespace Server
{
    public class Connection
    {
        public string Ip { get; set; }
        public int Port { get; set; }
        public int LimitConectors { get; set; }

        public string ConnectionString => $"{Ip}:{Port}";
    }
}