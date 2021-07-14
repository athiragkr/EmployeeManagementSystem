using EmployeeManagementSystem.Classes;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem.Helper
{
    public class RestHelper : IRestHelper
    {
        private readonly string baseUrl = ConfigurationManager.AppSettings["baseUrl"];
        private readonly string apiKey = ConfigurationManager.AppSettings["apiKey"];
        private readonly string requestHeader = ConfigurationManager.AppSettings["requestHeader"];

        /// <summary>
        /// Method to fetch Employee Details from Rest Api
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAllEmployees()
        {
            try
            {
                var response = await RestClient<Dictionary<string, string>>.Get(baseUrl + "users?page=1");
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
         }

        /// <summary>
        /// Method to post Employee Details to Rest Api
        /// </summary>
        /// <param name="empData"></param>
        /// <returns></returns>
        public async Task<string> Post(Dictionary<string, string> inputData)
        {
            try
            {
                var response = await RestClient<Dictionary<string, string>>.PostRequest(baseUrl + "users", apiKey, inputData);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Method to delete Employee Details from Rest Api
        /// </summary>
        /// <param name="empData"></param>
        /// <returns></returns>
        public async Task<string> Delete(Datum empData)
        {
            try
            {
                var response = await RestClient<StringContent>.DeleteRequest(baseUrl + "users/" + empData.id);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///// <summary>
        ///// Method to update Employee Details from Rest Api
        ///// </summary>
        ///// <param name="empData"></param>
        ///// <returns></returns>
        public async Task<string> Put(Datum empData)
        {
            try
            {
                var inputData = new
                {
                    id = Convert.ToString(empData.id),
                    name = empData.name,
                    email = empData.email,
                    gender = empData.gender,
                    status = empData.status
                };
                var input = new StringContent(JsonConvert.SerializeObject(inputData), Encoding.UTF8, "application/json");
                var response = await RestClient<StringContent>.PutRequest(baseUrl + String.Format("{0}{1}", "users/", empData.id), input);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to get Employee Details by page No. from Rest Api
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetByPageNo(int pageNo)
        {
            try
            {
                var response = await RestClient<Dictionary<string, string>>.Get((String.Format("{0}{1}{2}", baseUrl, "users/?page=", pageNo)));
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DTO GetContent<DTO>(string res)
        {
            DTO dtoObject = JsonConvert.DeserializeObject<DTO>(res);
            return dtoObject;
        }
    }
}
