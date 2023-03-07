using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFtest2.Class
{
    public class Weather
    {
        [JsonProperty("name")]
        public string Location { get; set; }

        [JsonProperty("weather")]
        public WeatherInfo[] WeatherInfo { get; set; }

        [JsonProperty("main")]
        public MainInfo MainInfo { get; set; }

        [JsonProperty("wind")]
        public WindInfo WindInfo { get; set; }
    }

    public class WeatherInfo
    {
        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class MainInfo
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("pressure")]
        public double Pressure { get; set; }

        [JsonProperty("humidity")]
        public double Humidity { get; set; }
    }

    public class WindInfo
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }
    }

    public static class WeatherService
    {
        private const string ApiKey = "784dce9177341e93f289398169ac9fa4";
        private const string ApiUrl = "http://api.openweathermap.org/data/2.5/weather?lang=cz";

        public static Weather GetWeather(string city)
        {
            var client = new RestClient(ApiUrl);
            var request = new RestRequest(Method.GET);
            request.AddParameter("q", city);
            request.AddParameter("appid", ApiKey);
            request.AddParameter("units", "metric");
            var response = client.Execute(request);
            var content = response.Content;
            var weather = JsonConvert.DeserializeObject<Weather>(content);
            return weather;
        }
    }
}





