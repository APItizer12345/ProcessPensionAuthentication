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

    }
}
