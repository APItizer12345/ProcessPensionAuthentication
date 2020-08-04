using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

//try interface
//exception handling



namespace ProcessPension.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessPensionController : ControllerBase
    {
        public static ClientInput client = new ClientInput {
            clientName = "Sahil",
            aadharNumber = "111122223333",
            panDetail = "BCFPN1234F",
            dateOfBirth = new DateTime(1998,03,01),
            family = 2
        };


        public static PensionDetailCall getDetail = new PensionDetailCall();
        public static HttpResponseMessage response = getDetail.CallPensionDetail(client.aadharNumber);
        public static string responseValue = response.Content.ReadAsStringAsync().Result;
        public static dynamic details = JObject.Parse(responseValue);

        [HttpGet]
        public MVCClientOutput GetInfoByAadhar()

        {
            
            ClientInput pensionDetailCall = new ClientInput();

            try
            {
                pensionDetailCall.clientName = details.name;
            }
            catch (Exception e)
            {
                MVCClientOutput mvc = new MVCClientOutput();
                mvc.Name = "";
                mvc.panDetail = "";
                mvc.pensionAmount = 0;
                mvc.dateOfBirth = new DateTime(2000, 01, 01);
                mvc.message = new HttpResponseMessage(HttpStatusCode.NoContent);
                mvc.serviceCharge = 0;
                mvc.aadhar = "***";

                return mvc;
            }

            pensionDetailCall.aadharNumber = details.aadharNumber;
            pensionDetailCall.panDetail = details.pan;
            pensionDetailCall.family = details.pensionType;
            pensionDetailCall.dateOfBirth = details.date_of_birth;
            int bankType = details.bankType;

            int bankServiceCharge;
            if (bankType == 1)
                bankServiceCharge = 500;
            else
                bankServiceCharge = 550;



            MVCClientOutput mvcClientOutput = new MVCClientOutput();

            if (pensionDetailCall.family == client.family)
            {
                mvcClientOutput.Name = details.name;
                mvcClientOutput.panDetail = details.pan;
                mvcClientOutput.pensionAmount = CalculatePensionAmount();
                mvcClientOutput.dateOfBirth = details.date_of_birth;
                mvcClientOutput.message = new HttpResponseMessage(HttpStatusCode.OK);
                mvcClientOutput.serviceCharge = bankServiceCharge;
                mvcClientOutput.aadhar = details.aadharNumber;
            }
            else
            {
                mvcClientOutput.Name = "";
                mvcClientOutput.panDetail = "";
                mvcClientOutput.pensionAmount = 0;
                mvcClientOutput.dateOfBirth = new DateTime(2000,01,01);
                mvcClientOutput.message = new HttpResponseMessage(HttpStatusCode.NotFound);
                mvcClientOutput.serviceCharge = 0;
                mvcClientOutput.aadhar = "****";
            }

            return mvcClientOutput;
        }

        [HttpPost]
        public string PensionDisbursmentCall()
        {
            
            PensionDisbursementCall statusCode = new PensionDisbursementCall();
            int status = statusCode.CallPensionDetail();
            for(int i =0;i<3;i++)
            {
                if (status == 21)
                    status = statusCode.CallPensionDetail();
            }
             if (status == 10)
                return "Successs";
            else
                return "Unexpected Error Occured";
        }


        public double CalculatePensionAmount()
        {
            
            int pensionType = details.bankType;                                                  
            double pensionAmount;
            

            double salary = details.salaryEarned;
            double allowances = details.allowances;

            if (pensionType == 1)
                pensionAmount = (0.8 * salary) + allowances;
            else
                pensionAmount = (0.5 * salary) + allowances;



            if (details.bankType == 1)
                pensionAmount = pensionAmount + 500;
            else
                pensionAmount = pensionAmount + 550;

            return pensionAmount;

        }

    }


    public class MVCClientOutput
    {
        public string Name { get; set; }
        public double pensionAmount { get; set; }
        public string panDetail { get; set; }
        public string aadhar { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int family { get; set; }
        public HttpResponseMessage message { get; set; }
        public int serviceCharge { get; set; }
    }

     
}
