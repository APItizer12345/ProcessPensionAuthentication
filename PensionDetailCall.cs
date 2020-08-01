using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProcessPension
{
    public class PensionDetailCall
    {
        public HttpResponseMessage CallPensionDetail(string aadhar)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44341/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                response = client.GetAsync("api/PensionerDetail/" + aadhar).Result;
            }
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            else
             response = null;
            return response;
        }
        public string GetPensionerName(string aadhar)
        {
            HttpResponseMessage response = CallPensionDetail(aadhar);
            string name = response.Content.ReadAsStringAsync().Result;
            dynamic details = JObject.Parse(name);
            return details.name;
        }
        public string GetPensionerPanDetail(string aadhar)
        {
            HttpResponseMessage response = CallPensionDetail(aadhar);
            string name = response.Content.ReadAsStringAsync().Result;
            dynamic details = JObject.Parse(name);
            return details.pan;
        }
        public string GetPensionerAadharDetail(string aadhar)
        {
            HttpResponseMessage response = CallPensionDetail(aadhar);
            string name = response.Content.ReadAsStringAsync().Result;
            dynamic details = JObject.Parse(name);
            return details.aadharNumber;
        }
        public int GetPensionerFamilyOrSelf(string aadhar)
        {
            HttpResponseMessage response = CallPensionDetail(aadhar);
            string name = response.Content.ReadAsStringAsync().Result;
            dynamic details = JObject.Parse(name);
            return details.pensionType;
        } 
        public int GetPensionerBankType(string aadhar)
        {
            HttpResponseMessage response = CallPensionDetail(aadhar);
            string name = response.Content.ReadAsStringAsync().Result;
            dynamic details = JObject.Parse(name);
            return details.bankType;
        }
        public int GetPensionerSalary(string aadhar)
        {
            HttpResponseMessage response = CallPensionDetail(aadhar);
            string name = response.Content.ReadAsStringAsync().Result;
            dynamic details = JObject.Parse(name);
            return details.salaryEarned;
        }
        public int GetPensionerAllowances(string aadhar)
        {
            HttpResponseMessage response = CallPensionDetail(aadhar);
            string name = response.Content.ReadAsStringAsync().Result;
            dynamic details = JObject.Parse(name);
            return details.allowances;
        }

    }
}
