/*
File: Case.cs
Project: Assignment3
Programmer: Doosan Beak
First Version: 2019-10-15
Descrption: This file contains Resolve IP Class and a method that support service to resolve the ip address
*/
using System;
using System.Text.RegularExpressions;
using System.Web.Services;
using System.Xml.Serialization;

namespace ResolveService
{

    [WebService(Namespace = "http://localhost/ResolveIP/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Resolve : System.Web.Services.WebService
    {

        /*
        Function : GetInfo
        DESCRIPTION : This method act as wrapper method, calls P2GeoSoapClient service  
        PARAMETERS : string ipAddress
        RETURNS : IPInfo: a struct that contains ip infomation
        */
        [XmlInclude(typeof(IPInfo))]
        [WebMethod]
        public IPInfo GetInfo(string ipAddress)
        {
            var fault = new SoapFault();
            string message = "";
            //Match pattern for IP address    
            string Pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
            //Regular Expression object    
            Regex check = new Regex(Pattern);

            //check to make sure an ip address was provided    
            if (string.IsNullOrEmpty(ipAddress))
            {
                message = "Enpty input ---> please provide an ip address";
                fault.myThrow(message);
            }
            else if (!check.IsMatch(ipAddress, 0))
            {
                message = "Ip format error ---> your ip is in wrong format";
                fault.myThrow(message);
            }

            IPInfo info = new IPInfo();

            GEO.P2GeoSoapClient geo = new GEO.P2GeoSoapClient("IP2GeoSoap");
            var ipInfo = geo.ResolveIP(ipAddress, "0");

            info.City = ipInfo.City;
            info.Country = ipInfo.Country;
            info.Latitude = Convert.ToDecimal(ipInfo.Latitude);
            info.Longitude = Convert.ToDecimal(ipInfo.Longitude);
            info.Organization = ipInfo.Organization;
            info.StateProvince = ipInfo.StateProvince;

            return info;
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        //[System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlTypeAttribute(Namespace = "http://localhost/webservice/")]
        public struct IPInfo
        {
            public string City;
            public string StateProvince;
            public string Country;
            public string Organization;
            public decimal Latitude;
            public decimal Longitude;

        }
    }
}
