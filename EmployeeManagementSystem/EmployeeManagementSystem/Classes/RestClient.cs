using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Classes
{
    public static class RestClient<T> where T : class
    {
        private static readonly string apiKey = ConfigurationManager.AppSettings["apiKey"];
        private static readonly string requestHeader = ConfigurationManager.AppSettings["requestHeader"];
        /// <summary>
        /// For getting the data from a rest api
        /// </summary>
        /// <param name="url">API Url</param>
        /// <returns>A Task with result object of type T</returns>
        
        public static async Task<string> Get(string apiUrl)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(apiUrl))
                    {
                        client.DefaultRequestHeaders.Add(requestHeader, string.Format("{0}", apiKey));
                        using (HttpContent content = res.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            if (data != null)
                            {
                                return data;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return string.Empty;
        }

        /// <summary>
        /// For creating a new item over a rest api using POST
        /// </summary>
        /// <param name="apiUrl">API Url</param>
        /// <param name="postObject">The object to be created</param>
        /// <returns>A Task with created item</returns>
        public static async Task<string> PostRequest(string apiUrl, string apiKey, Dictionary<string, string> postObject)
        {
            try
            {
                var input = new FormUrlEncodedContent(postObject);
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add(requestHeader, string.Format("{0}", apiKey));
                    using (HttpResponseMessage res = await client.PostAsync(apiUrl, input).ConfigureAwait(false))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            if (data != null)
                            {
                                return data;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return string.Empty;
        }

        /// <summary>
        /// For updating an existing item over a rest api using PUT
        /// </summary>
        /// <param name="apiUrl">API Url</param>
        /// <param name="putObject">The object to be edited</param>
        public static async Task<string> PutRequest(string apiUrl, StringContent putObject)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add(requestHeader, string.Format("{0}", apiKey));
                    using (HttpResponseMessage res = await client.PutAsync(apiUrl, putObject).ConfigureAwait(false))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            if (data != null)
                            {
                                return data;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return string.Empty;
        }

        /// <summary>
        /// For deleting an existing item over a rest api using DELETE
        /// </summary>
        /// <param name="apiUrl">API Url</param>
        /// <param name="putObject">The object to be edited</param>
        public static async Task<string> DeleteRequest(string apiUrl)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add(requestHeader, string.Format("{0}", apiKey));
                    using (HttpResponseMessage res = await client.DeleteAsync(apiUrl).ConfigureAwait(false))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            if (data != null)
                            {
                                return data;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return string.Empty;
        }
    }
}


