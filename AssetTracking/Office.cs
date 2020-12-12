using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace AssetTracking
{
    public class Office
    {
     
        static HttpClient client = new HttpClient();
        public string Location { get; private set; }
        private readonly string localCurrencyCode;
        private readonly string UrlForCurrencyRates = "https://api.exchangeratesapi.io/latest?base=USD";
        private IEnumerable<Asset> assets;
        public IEnumerable<Asset> Assets => this.assets.OrderBy(a => a.PurchaseDate);
        public void WriteAsset()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine(this.Location);
            Console.BackgroundColor = ConsoleColor.Black;
            foreach (var a in this.Assets)
                a.PrintToConsole(this.localCurrencyCode, GetConversionRateForLocalCurrency().Result);
        }

        public async Task<decimal> GetConversionRateForLocalCurrency()
        {
            HttpResponseMessage response = await client.GetAsync(UrlForCurrencyRates);
            var content = response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(content.Result);
            var rates = JsonConvert.DeserializeObject<Dictionary<string, decimal>>(data["rates"].ToString());
            var rate = rates[localCurrencyCode];
            return rate;
        }

        public Office(string location, IEnumerable<Asset> assets, string localCurrencyCode)
        {
            this.Location = location;
            this.assets = assets;
            this.localCurrencyCode = localCurrencyCode;
        }
        public Office()
        {
         
        }
    }
}