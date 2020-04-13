/*
File: Loan.cs
Project: Assignment3
Programmer: Doosan Beak
First Version: 2019-10-15
Descrption: This file contains Loan service class and a method that support service to calculate monthly payment
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;

namespace LoanService
{
    /// <summary>
    /// Summary description for Loan
    /// </summary>
    [WebService(Namespace = "http://localhost/VinniesLoanService/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Loan : System.Web.Services.WebService
    {

        /*
        Function : LoanPayment
        DESCRIPTION : This method gets principle, rate and payments as parameters and calculate out loan payment for per month 
        PARAMETERS : float principle
                     float rate
                     uint payments
        RETURNS : float: loan payment
        */
        [WebMethod]
        public float LoanPayment(float principle, float rate, uint payments)
        {
            float resualt = 0;
            var fault = new SoapFault();
            string message = "";
            string Pattern = @"^[0-9]*(?:\.[0-9]*)?$";
            Regex check = new Regex(Pattern);

            if (payments == 0)
            {
                message = "Invalid Input ---> Payments cannot be 0";
                fault.myThrow(message);
            }
            // rate could be 0
            if (rate == 0)
            {
                //message = "Invalid Input ---> Rate cannot be 0";
                //fault.myThrow(message);
                return (float)Math.Round(principle / payments, 2);
            }
            // check positive float number
            if (check.IsMatch(principle.ToString(), 0))
            {
                if (check.IsMatch(rate.ToString(), 0))
                {
                    //check positive integer number
                    if (new Regex(@"^[0-9]+$").IsMatch(payments.ToString(), 0))
                    {
                        resualt = (float)(Math.Round((rate + rate / (Math.Pow(1 + rate, payments) - 1)) * principle, 2));
                    }
                    else
                    {
                        message = "Payments Error ---> Please enter positive integer number only";
                        fault.myThrow(message);
                    }
                }
                else
                {
                    message = "Rate Error ---> Please enter positive number only";
                    fault.myThrow(message);
                }
            }
            else
            {
                message = "Principle Error ---> Please enter positive number only";
                fault.myThrow(message);
            }

            return resualt;
        }
    }
}
