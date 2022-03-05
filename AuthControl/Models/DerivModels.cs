using Newtonsoft.Json;

namespace AuthControl.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Trading
    {
    }

    public class AccountList
    {
        [JsonProperty("account_type")]
        public string AccountType { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("is_disabled")]
        public int IsDisabled { get; set; }

        [JsonProperty("is_virtual")]
        public int IsVirtual { get; set; }

        [JsonProperty("landing_company_name")]
        public string LandingCompanyName { get; set; }

        [JsonProperty("loginid")]
        public string Loginid { get; set; }

        [JsonProperty("trading")]
        public Trading Trading { get; set; }
    }

    public class BRL
    {
        [JsonProperty("fractional_digits")]
        public int FractionalDigits { get; set; }
    }

    public class LocalCurrencies
    {
        [JsonProperty("BRL")]
        public BRL BRL { get; set; }
    }

    public class Authorize
    {
        [JsonProperty("account_list")]
        public List<AccountList> AccountList { get; set; }

        [JsonProperty("balance")]
        public int Balance { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("fullname")]
        public string Fullname { get; set; }

        [JsonProperty("is_virtual")]
        public int IsVirtual { get; set; }

        [JsonProperty("landing_company_fullname")]
        public string LandingCompanyFullname { get; set; }

        [JsonProperty("landing_company_name")]
        public string LandingCompanyName { get; set; }

        [JsonProperty("local_currencies")]
        public LocalCurrencies LocalCurrencies { get; set; }

        [JsonProperty("loginid")]
        public string Loginid { get; set; }

        [JsonProperty("preferred_language")]
        public string PreferredLanguage { get; set; }

        [JsonProperty("scopes")]
        public List<string> Scopes { get; set; }

        [JsonProperty("trading")]
        public Trading Trading { get; set; }

        [JsonProperty("upgradeable_landing_companies")]
        public List<string> UpgradeableLandingCompanies { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }
    }

    public class EchoReq
    {
        [JsonProperty("authorize")]
        public string Authorize { get; set; }
    }

    public class AuthorizationResponse
    {
        [JsonProperty("authorize")]
        public Authorize Authorize { get; set; }

        [JsonProperty("echo_req")]
        public EchoReq EchoReq { get; set; }

        [JsonProperty("msg_type")]
        public string MsgType { get; set; }
    }



}
