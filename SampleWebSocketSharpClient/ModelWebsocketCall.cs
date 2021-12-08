using Newtonsoft.Json;

namespace SampleWebSocketSharpClient
{
    public class ModelWebsocketCall
    {
        [JsonProperty("event")]
        public string Event { get; set; }
        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
