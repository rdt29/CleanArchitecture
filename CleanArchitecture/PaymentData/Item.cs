using Newtonsoft.Json;

namespace CleanArchitecture.PaymentData
{
    public class Item
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
