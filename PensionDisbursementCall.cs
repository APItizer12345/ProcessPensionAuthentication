using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProcessPension
{
    //public class PensionDisbursementCall
    //{
    //    public int CallPensionDetail(string aadharNumber)
    //    {
    //        int statusCode;
    //        HttpResponseMessage response = new HttpResponseMessage();
    //        using (var client = new HttpClient())
    //        {
    //            client.BaseAddress = new Uri("https://localhost:44312/");
    //            var data = new StringContent("application/json");
    //            client.DefaultRequestHeaders.Clear();
    //            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    //            try
    //            {
    //                response = client.PostAsync("api/PensionDisbursement",data).Result;
    //            }
    //            catch(Exception e)
    //            { response=null;}
    //        }
    //        if (response!=null)
    //        {
    //            string status = response.Content.ReadAsStringAsync().Result;
                
    //        }
    //        else
    //           statusCode = 0;
    //        return statusCode;
    //    }
    //}
}
