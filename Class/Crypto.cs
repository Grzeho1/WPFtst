using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace WPFtest2.Class
{
    public class Crypto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("current_price")]
        public decimal CurrentPrice { get; set; }

        [JsonProperty("market_cap")]
        public long MarketCap { get; set; }

        [JsonProperty("market_cap_rank")]
        public int MarketCapRank { get; set; }

        [JsonProperty("fully_diluted_valuation")]
        public long FullyDilutedValuation { get; set; }

        [JsonProperty("total_volume")]
        public decimal TotalVolume { get; set; }

        [JsonProperty("high_24h")]
        public decimal High24h { get; set; }

        [JsonProperty("low_24h")]
        public decimal Low24h { get; set; }

        [JsonProperty("price_change_24h")]
        public decimal PriceChange24h { get; set; }

        [JsonProperty("price_change_percentage_24h")]
        public decimal PriceChangePercentage24h { get; set; }

        [JsonProperty("market_cap_change_24h")]
        public long MarketCapChange24h { get; set; }

        [JsonProperty("market_cap_change_percentage_24h")]
        public decimal MarketCapChangePercentage24h { get; set; }

        [JsonProperty("circulating_supply")]
        public long CirculatingSupply { get; set; }

        [JsonProperty("total_supply")]
        public long TotalSupply { get; set; }

        [JsonProperty("max_supply")]
        public long MaxSupply { get; set; }

        [JsonProperty("ath")]
        public decimal Ath { get; set; }

        [JsonProperty("ath_change_percentage")]
        public decimal AthChangePercentage { get; set; }

        [JsonProperty("ath_date")]
        public string AthDate { get; set; }

        [JsonProperty("atl")]
        public decimal Atl { get; set; }

        [JsonProperty("atl_change_percentage")]
        public decimal AtlChangePercentage { get; set; }

        [JsonProperty("atl_date")]
        public string AtlDate { get; set; }

        [JsonProperty("roi")]
        public object Roi { get; set; }

        [JsonProperty("last_updated")]
        public string LastUpdated { get; set; }

        [JsonProperty("price_change_percentage_24h_in_currency")]
        public decimal PriceChangePercentage24hInCurrency { get; set; }



    }

    
    public static class cryptoService
    {

        public static List<Crypto> GetCrypto()
        {

            var client = new RestClient("https://coingecko.p.rapidapi.com/coins/markets?vs_currency=usd&price_change_percentage=24h&page=1&sparkline=false&per_page=100&ids=bitcoin&order=market_cap_desc");
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-RapidAPI-Key", "ba8503ed89mshb8552a75c0d64e6p15ae15jsnf2f3fd19c74e");
            request.AddHeader("X-RapidAPI-Host", "coingecko.p.rapidapi.com");
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            var cryptoList = JsonConvert.DeserializeObject<List<Crypto>>(content);
            return cryptoList;
        }
    }
}
