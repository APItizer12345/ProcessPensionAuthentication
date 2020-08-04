using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ProcessPension
{
    public class PensionDetailCall
    {
        
        
        public HttpResponseMessage CallPensionDetail(string aadhar)
        {
            PensionDetailCall banktype = new PensionDetailCall();
            ClientInput res = new ClientInput();
            HttpResponseMessage response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44341/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    response = client.GetAsync("api/PensionerDetail/" + aadhar).Result;
                }
                catch(Exception e)
                { response = null; }
            }
            return response;
        }



        public ClientInput GetClientInfo(string aadhar)
        {
            ClientInput res = new ClientInput();
            HttpResponseMessage response = CallPensionDetail(aadhar);
            string responseValue = response.Content.ReadAsStringAsync().Result;
            res = JsonConvert.DeserializeObject<ClientInput>(responseValue);

            return res;
        }


        public ValueforCalCulation GetCalculationValues(string aadhar)
        {
            ValueforCalCulation res = new ValueforCalCulation();
            HttpResponseMessage response = CallPensionDetail(aadhar);
            string responseValue = response.Content.ReadAsStringAsync().Result;
            res = JsonConvert.DeserializeObject<ValueforCalCulation>(responseValue);
            return res;
        }

    }
}
