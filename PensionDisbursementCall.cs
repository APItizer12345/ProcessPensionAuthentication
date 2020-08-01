using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProcessPension
{
    public class PensionDisbursementCall
    {
        public int CallPensionDetail()
        {
            int statusCode;
            HttpResponseMessage response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44312/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                response = client.GetAsync("api/PensionDisbursement").Result;
            }
            if (response.IsSuccessStatusCode)
            {
                string status = response.Content.ReadAsStringAsync().Result;
                statusCode = Int32.Parse(status);
            }
            else
               statusCode = 0;
            return statusCode;
        }
    }
}
