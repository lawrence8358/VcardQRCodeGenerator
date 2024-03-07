using Newtonsoft.Json;
using System;

namespace VcardQRCodeGenerator.Model
{
    public class ConfigModel
    {
        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("fields")]
        public IEnumerable<ConfigItmeModel> Fields { get; set; }
    }

    public class ConfigItmeModel
    {
        [JsonProperty("excel")]
        public string Excel { get; set; }

        [JsonProperty("vcard")]
        public string VCard { get; set; }

        [JsonProperty("key")]
        public bool Key { get; set; }
    }

    public class CardModel
    {
        public string Lang { get; set; }

        public string Content { get; set; }

        public string FileName { get; set; }
    }
}
