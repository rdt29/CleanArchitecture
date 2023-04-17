using Newtonsoft.Json;

namespace CleanArchitecture.PaymentData
{
    public class PaymentIntentCreateRequest
    {
        [JsonProperty("items")]
        public Item[] Items { get; set; }
    }
}
