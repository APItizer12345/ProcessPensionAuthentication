using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProcessPension.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessPensionController : ControllerBase
    {
        ClientInput client = new ClientInput {
            clientName = "Sahil",
            aadharNumber = "111122223333",
            panDetail = "BCFPN1234F",
            family = 1
        };
        [HttpGet]
        public ProcessPensionGetOutput GetInfoByAadhar()

        {
             
            PensionDetailCall getDetail = new PensionDetailCall();
            ClientInput pensionDetailCall = new ClientInput();
            pensionDetailCall.clientName = getDetail.GetPensionerName(client.aadharNumber);
            pensionDetailCall.aadharNumber = getDetail.GetPensionerAadharDetail(client.aadharNumber);
            pensionDetailCall.panDetail = getDetail.GetPensionerPanDetail(client.aadharNumber);
            pensionDetailCall.family = getDetail.GetPensionerFamilyOrSelf(client.aadharNumber);
            int bankType = getDetail.GetPensionerBankType(client.aadharNumber);
            int bankServiceCharge;
            if (bankType == 1)
                bankServiceCharge = 500;
            else
                bankServiceCharge = 550;


            ProcessPensionGetOutput infoToSend = new ProcessPensionGetOutput
            {
                aadhar = client.aadharNumber,
                pensionAmount = CalculatePensionAmount(client.aadharNumber),
                serviceCharge = bankServiceCharge
            };

            string message = "";

            if (pensionDetailCall.family == client.family)
            {
                // message = new HttpResponseMessage(HttpStatusCode.NotFound);
                message =  "User Found";
            }
            else
            {
                message =  "User Not Found";
            }
               // message = new HttpResponseMessage(HttpStatusCode.NotFound);
            
            
            return  infoToSend;
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
            if (status == 21)
                return "Calculated Value Wrong";
            else
                return "Success";
        }


        public double CalculatePensionAmount(string aadhar)
        {
            PensionDetailCall getDetails = new PensionDetailCall();
            int pensionType = getDetails.GetPensionerFamilyOrSelf(aadhar);
            double pensionAmount;
            //int salary = getDetails.GetPensionerSalary(aadhar);
            //int allowances = getDetails.GetPensionerAllowances(aadhar);



            if (pensionType == 1)
                pensionAmount = (0.8 * getDetails.GetPensionerSalary(aadhar)) + getDetails.GetPensionerAllowances(aadhar);
            else
                pensionAmount = (0.5 * getDetails.GetPensionerSalary(aadhar)) + getDetails.GetPensionerAllowances(aadhar);



            if (getDetails.GetPensionerBankType(aadhar) == 1)
                pensionAmount = pensionAmount + 500;
            else
                pensionAmount = pensionAmount + 550;

            return pensionAmount;

        }

    }


    

     public class ProcessPensionGetOutput
    {
        public string aadhar { get; set; }
        public double pensionAmount { get; set; }
        public int serviceCharge { get; set; }
    }
}
