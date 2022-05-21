using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TripperAPI.Services.ApiDataServices
{
    public abstract class ApiDataService
    {
        internal static string ReplaceCommasWithDots(double valueToEdit) => valueToEdit.ToString().Replace(',', '.');
        internal async Task<T> GetDataFromApiAsync<T>(string jsonUrl)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            using (var client = new HttpClient())
            {
                response = await client.GetAsync(jsonUrl);
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            var deserializedContent = JsonConvert.DeserializeObject<T>(responseContent);

            return deserializedContent;
        }

    }
}
