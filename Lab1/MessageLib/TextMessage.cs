using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLib
{
    public class TextMessage
    {
        [JsonProperty("Ip")]
        public string Ip { get; set; }

        [JsonProperty("Port")]
        public int Port { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("FileName")]
        public string FileName { get; set; }

        [JsonProperty("File")]
        public string File { get; set; }

        [JsonProperty("Users")]
        public List<String> Users { get; set; }

        [JsonProperty("IsSystemMes")]
        public bool IsSystemMes { get; set; }
    }
}
