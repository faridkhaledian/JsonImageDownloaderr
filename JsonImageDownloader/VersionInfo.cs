using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


    namespace JsonImageDownloader
    {
        public class VersionInfo
        {
        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("file")]
        public string File { get; set; }
        }
    }
