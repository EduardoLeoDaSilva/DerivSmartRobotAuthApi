
using Newtonsoft.Json;

namespace AuthControl.Models
{
    // RootChangePlan myDeserializedClass = JsonConvert.DeserializeObject<RootChangePlan>(myJsonResponse);
    public class Product
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("has_co_production")]
        public bool HasCoProduction { get; set; }
    }

    public class Affiliate
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Buyer
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }

    public class Producer
    {
        [JsonProperty("name")]
        public string Name{ get; set; }
}

    public class Commission
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
    }

    public class FullPrice
    {
        [JsonProperty("value")]
        public decimal Value { get; set; }
    }

    public class OriginalOfferPrice
    {
        [JsonProperty("currency_value")]
        public string CurrencyValue { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }
    }

    public class Price
    {
        [JsonProperty("value")]
        public double Value { get; set; }
    }

    public class Payment
    {
        [JsonProperty("billet_barcode")]
        public string? BilletBarcode { get; set; }

        [JsonProperty("billet_url")]
        public string? BilletUrl { get; set; }

        [JsonProperty("installments_number")]
        public int? InstallmentsNumber { get; set; }

        [JsonProperty("refusal_reason")]
        public string? RefusalReason { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("pix_qrcode")]
        public string? PixQrcode { get; set; }

        [JsonProperty("pix_code")]
        public string? PixCode { get; set; }

        [JsonProperty("pix_expiration_date")]
        public long? PixExpirationDate { get; set; }
    }


    public class Plan
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
    public class Purchase
    {
        [JsonProperty("approved_date")]
        public long ApprovedDate { get; set; }

        [JsonProperty("full_price")]
        public FullPrice? FullPrice { get; set; }

        [JsonProperty("original_offer_price")]
        public OriginalOfferPrice? OriginalOfferPrice { get; set; }

        [JsonProperty("price")]
        public Price Price { get; set; }

        [JsonProperty("order_date")]
        public string? OrderDate { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("transaction")]
        public string? Transaction { get; set; }

        [JsonProperty("payment")]
        public Payment? Payment { get; set; }
    }


    public class Subscriber
    {
        [JsonProperty("code")]
        public string Code { get; set; }
    }

    public class Subscription
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("plan")]
        public Plan Plan{ get; set; }

    [JsonProperty("subscriber")]
        public Subscriber Subscriber { get; set; }
    }

    public class Data
    {
        [JsonProperty("product")]
        public Product Product { get; set; }

        [JsonProperty("affiliates")]
        public List<Affiliate> Affiliates { get; set; }

        [JsonProperty("buyer")]
        public Buyer Buyer { get; set; }

        [JsonProperty("producer")]
        public Producer Producer { get; set; }

        [JsonProperty("commissions")]
        public List<Commission> Commissions { get; set; }

        [JsonProperty("purchase")]
        public Purchase Purchase { get; set; }

        [JsonProperty("subscription")]
        public Subscription Subscription { get; set; }
    }

    public class OrderEvent
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("creation_date")]
        public int CreationDate { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }


}
