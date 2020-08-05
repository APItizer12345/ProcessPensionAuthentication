using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace ProcessPension.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessPensionController : ControllerBase
    {

        [Route("[action]")]
        [HttpPost]
        public MVCClientOutput GetClient(ClientInput _client)
        {


            ClientInput client = new ClientInput();
            client.name = _client.name;
            client.aadharNumber = _client.aadharNumber;
            client.pan = _client.pan;
            client.dateOfBirth = _client.dateOfBirth;
            client.pensionType = _client.pensionType;

            PensionDetailCall pension = new PensionDetailCall();
            ClientInput pensionDetail = pension.GetClientInfo(client.aadharNumber);






            if (pensionDetail == null)
            {
                MVCClientOutput mvc = new MVCClientOutput();
                mvc.name = "";
                mvc.pan = "";
                mvc.pensionAmount = 0;
                mvc.dateOfBirth = new DateTime(2000, 01, 01);
                mvc.message = new HttpResponseMessage(HttpStatusCode.NoContent);
                mvc.bankType = 1;
                mvc.aadharNumber = "***";
                mvc.status = 1;

                return mvc;
            }

            
            int bankType = pension.GetCalculationValues(client.aadharNumber).bankType;


            int bankServiceCharge;
            if (bankType == 1)
                bankServiceCharge = 500;
            else
                bankServiceCharge = 550;



            
            int pensionType = pension.GetCalculationValues(client.aadharNumber).pensionType;
            double pensionAmount;


            double salary = pension.GetCalculationValues(client.aadharNumber).salaryEarned;
            double allowances = pension.GetCalculationValues(client.aadharNumber).allowances;

            if (pensionType == 1)
                pensionAmount = (0.8 * salary) + allowances;
            else
                pensionAmount = (0.5 * salary) + allowances;



            if (pension.GetCalculationValues(client.aadharNumber).bankType == 1)
                pensionAmount = pensionAmount + 500;
            else
                pensionAmount = pensionAmount + 550;






            int statusCode;
           








            MVCClientOutput mvcClientOutput = new MVCClientOutput();

            if (client.pan.Equals(pensionDetail.pan)&&client.name.Equals(pensionDetail.name)&& client.pensionType.Equals(pensionDetail.pensionType)&& client.dateOfBirth.Equals(pensionDetail.dateOfBirth))
            {
                mvcClientOutput.name = pensionDetail.name;
                mvcClientOutput.pan = pensionDetail.pan;
                mvcClientOutput.pensionAmount = pensionAmount;
                mvcClientOutput.dateOfBirth = pensionDetail.dateOfBirth.Date;
                mvcClientOutput.pensionType = pension.GetCalculationValues(client.aadharNumber).pensionType;
                mvcClientOutput.message = new HttpResponseMessage(HttpStatusCode.OK);
                mvcClientOutput.bankType = bankType;
                mvcClientOutput.aadharNumber = pensionDetail.aadharNumber;
                mvcClientOutput.status = 0;

            }
            else
            {
                mvcClientOutput.name = "";
                mvcClientOutput.pan = "";
                mvcClientOutput.pensionAmount = 0;
                mvcClientOutput.dateOfBirth = new DateTime(2000, 01, 01);
                mvcClientOutput.pensionType = pension.GetCalculationValues(client.aadharNumber).pensionType;
                mvcClientOutput.message = new HttpResponseMessage(HttpStatusCode.NotFound);
                mvcClientOutput.bankType = 1;
                mvcClientOutput.aadharNumber = "****";
                mvcClientOutput.status = 0;
            }



            HttpResponseMessage response = new HttpResponseMessage();
            using (var client1 = new HttpClient())
            {
                client1.BaseAddress = new Uri("https://localhost:44312/");
                StringContent content = new StringContent(JsonConvert.SerializeObject(mvcClientOutput), Encoding.UTF8, "application/json");
                client1.DefaultRequestHeaders.Clear();
                client1.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    response = client1.PostAsync("api/PensionDisbursement", content).Result;
                }
                catch (Exception e)
                { response = null; }
            }
            if (response != null)
            {
                string status = response.Content.ReadAsStringAsync().Result;
                statusCode = Int32.Parse(status);
            }
            else
                statusCode = 0;

            mvcClientOutput.status = statusCode;


            return mvcClientOutput;
        }
        

        //[HttpGet]
        //public MVCClientOutput GetInfoByAadhar()

        //{
        //    ClientInput client = new ClientInput
        //    {
        //        name = "Sahil",
        //        aadharNumber = "111122223333",
        //        pan = "BCFPN1234F",
        //        dateOfBirth = new DateTime(1998, 03, 01),
        //        pensionType = 2
        //    };


        //PensionDetailCall pension = new PensionDetailCall();
        //    ClientInput pensionDetail = pension.GetClientInfo(client.aadharNumber);


        //    if (pensionDetail == null)
        //    {
        //        MVCClientOutput mvc = new MVCClientOutput();
        //        mvc.name = "";
        //        mvc.pan = "";
        //        mvc.pensionAmount = 0;
        //        mvc.dateOfBirth = new DateTime(2000, 01, 01);
        //        mvc.message = new HttpResponseMessage(HttpStatusCode.NoContent);
        //        mvc.bankType = 0;
        //        mvc.aadhar = "***";

        //        return mvc;
        //    }

        //    //pensionDetailCall.aadharNumber = details.aadharNumber;
        //    //pensionDetailCall.pan = details.pan;
        //    //pensionDetailCall.pensionType = details.pensionType;
        //    //pensionDetailCall.dateOfBirth = details.date_of_birth;
        //    int bankType = pension.GetCalculationValues(client.aadharNumber).bankType;
            

        //    int bankServiceCharge;
        //    if (bankType == 1)
        //        bankServiceCharge = 500;
        //    else
        //        bankServiceCharge = 550;

        //    int pensionType = pension.GetCalculationValues(client.aadharNumber).pensionType;
        //    double pensionAmount;


        //    double salary = pension.GetCalculationValues(client.aadharNumber).salaryEarned;
        //    double allowances = pension.GetCalculationValues(client.aadharNumber).allowances;

        //    if (pensionType == 1)
        //        pensionAmount = (0.8 * salary) + allowances;
        //    else
        //        pensionAmount = (0.5 * salary) + allowances;



        //    if (pension.GetCalculationValues(client.aadharNumber).bankType == 1)
        //        pensionAmount = pensionAmount + 500;
        //    else
        //        pensionAmount = pensionAmount + 550;

        //    MVCClientOutput mvcClientOutput = new MVCClientOutput();

        //    if (client.pan.Equals(pensionDetail.pan))
        //    {
        //        mvcClientOutput.name = pensionDetail.name;
        //        mvcClientOutput.pan = pensionDetail.pan;
        //        mvcClientOutput.pensionAmount =pensionAmount;
        //        mvcClientOutput.dateOfBirth = pensionDetail.dateOfBirth.Date;
        //        mvcClientOutput.pensionType= pension.GetCalculationValues(client.aadharNumber).pensionType;
        //        mvcClientOutput.message = new HttpResponseMessage(HttpStatusCode.OK);
        //        mvcClientOutput.bankType = bankType;
        //        mvcClientOutput.aadhar = pensionDetail.aadharNumber;

        //    }
        //    else
        //    {
        //        mvcClientOutput.name = "";
        //        mvcClientOutput.pan = "";
        //        mvcClientOutput.pensionAmount = 0;
        //        mvcClientOutput.dateOfBirth = new DateTime(2000, 01, 01);
        //        mvcClientOutput.pensionType = pension.GetCalculationValues(client.aadharNumber).pensionType;
        //        mvcClientOutput.message = new HttpResponseMessage(HttpStatusCode.NotFound);
        //        mvcClientOutput.bankType = 0;
        //        mvcClientOutput.aadhar = "****";
               
        //    }

        //    return mvcClientOutput;
        //}

        //[HttpPost]
        //public string PensionDisbursmentCall(ClientInput _client)
        //{
            




        //    PensionDisbursementCall statusCode = new PensionDisbursementCall();
        //    int status = statusCode.CallPensionDetail(_client.aadharNumber);
        //    for(int i =0;i<3;i++)
        //    {
        //        if (status == 21)
        //            status = statusCode.CallPensionDetail(_client.aadharNumber);
        //    }
        //     if (status == 10)
        //        return "Successs";
        //    else
        //        return "Unexpected Error Occured";
        //}


      

    }


    public class MVCClientOutput
    {
        public string name { get; set; }
        public double pensionAmount { get; set; }
        public string pan { get; set; }
        public string aadharNumber { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int pensionType { get; set; }
        public HttpResponseMessage message { get; set; }
        public int bankType { get; set; }
        public int status { get; set; }
    }

     
}
