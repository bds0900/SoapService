/*
File: Case.cs
Project: Assignment3
Programmer: Doosan Beak
First Version: 2019-10-15
Descrption: This file contains TextService class and a method that support service ot convert string to upper case or lower case
*/
using System.Web.Services;

namespace SoapService
{

    [WebService(Namespace = "http://localhost/TextService/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TextService : System.Web.Services.WebService
    {


        /*
        Function : CaseConvert
        DESCRIPTION : This method converts string into upper case or lower case base on flag number
        PARAMETERS : string incoming
                     uint flag
        RETURNS : string: converted string
        */
        [WebMethod]
        public string CaseConvert(string incoming, uint flag)
        {
            var fault = new SoapFault();
            string result = "";
            string message = "";
            
            if (flag == 1)
            {
                result = incoming.ToUpper();
            }
            else if (flag == 2)
            {
                result = incoming.ToLower();
            }
            else
            {
                message = "Flag error ---> 1 for upper case, 2 for lower case";

                fault.myThrow(message);
            }
            
        
            return result;
        }
        
    }


}
