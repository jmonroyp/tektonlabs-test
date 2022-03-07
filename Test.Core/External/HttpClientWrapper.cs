using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Test.Core.External
{
    public static class HttpClientWrapper<T>
    {
        public static async Task<T> Get(string url)
        {
            T result = default(T);
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(new Uri(url)).Result;

                response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<T>(x.Result);
                });
            }

            return result;
        }

        public static async Task<T> PostRequest(string apiUrl, T postObject)
        {
            T result = default(T);;

            using (var client = new HttpClient())
            {

                var response =
                await client.PostAsync(apiUrl, new StringContent(JsonConvert.SerializeObject(postObject))).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<T>(x.Result);

                });
            }

            return result;
        }

        public static async Task PutRequest(string apiUrl, T putObject)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PutAsync(apiUrl, new StringContent(JsonConvert.SerializeObject(putObject))).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
            }
        }
    }
}