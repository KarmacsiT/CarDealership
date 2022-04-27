using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipDB.WpfClient
{
    class ApiClient
    {
        private static HttpClient HttpClient = new HttpClient(); //Always needs to be static

        public async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await HttpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();

                var entities = JsonConvert.DeserializeObject<T>(responseString);

                return entities;
            }

            throw new Exception("The request was not successfull");
        }

        public async Task<T> GetAsyncWithID<T>(int ID, string endpoint)
        {
            var response = await HttpClient.GetAsync($"{endpoint}/{ID}");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();

                var entities = JsonConvert.DeserializeObject<T>(responseString);

                return entities;
            }

            throw new Exception("The request was not successfull");
        }

        public async Task<T> GetAsyncWithString<T>(string extension, string endpoint)
        {
            var response = await HttpClient.GetAsync($"{endpoint}/{extension}");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();

                var entities = JsonConvert.DeserializeObject<T>(responseString);

                return entities;
            }

            throw new Exception("The request was not successfull");
        }
        public async Task DeleteAsync(int id, string endpoint)
        {
            var response = await HttpClient.DeleteAsync($"{endpoint}/{id}");

            response.EnsureSuccessStatusCode();
        }

        public async Task PutAsync<T>(T entity, string endpoint)
        {

            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

            var response = await HttpClient.PutAsync(endpoint, content);

            response.EnsureSuccessStatusCode();
        }

        public async Task PostAsync<T>(T entity, string endpoint)
        {
            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

            var response = await HttpClient.PostAsync(endpoint, content);

            response.EnsureSuccessStatusCode();
        }
    }
}
