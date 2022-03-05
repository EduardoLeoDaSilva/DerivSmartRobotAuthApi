using Newtonsoft.Json;

namespace AuthControl.Models
{
    // RootChangePlan myDeserializedClass = JsonConvert.DeserializeObject<RootChangePlan>(myJsonResponse);
    public class ProductChangePlan
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class User
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }

    public class SubscriptionChangePlan
    {
        [JsonProperty("subscriber_code")]
        public string subscriber_code { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("product")]
        public Product Product { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }

    public class Offer
    {
        [JsonProperty("key")]
        public string Key { get; set; }
    }

    public class PlanChanged
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("offer")]
        public Offer Offer { get; set; }

        [JsonProperty("current")]
        public bool Current { get; set; }
    }

    public class DataChangePlan
    {
        [JsonProperty("switch_plan_date")]
        public long SwitchPlanDate { get; set; }

        [JsonProperty("subscription")]
        public SubscriptionChangePlan Subscription { get; set; }

        [JsonProperty("plans")]
        public List<PlanChanged> Plans { get; set; }
    }

    public class RootChangePlan
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("creation_date")]
        public long CreationDate { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("data")]
        public DataChangePlan Data { get; set; }
    }

}
