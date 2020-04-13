/*
File: SoapFault.cs
Project: Assignment3
Programmer: Doosan Beak
First Version: 2019-10-15
Descrption: This file contains SoapFault class and a method that support program to throw soap fault message.
*/
using System.Web.Services;
using System.Web.Services.Protocols;

namespace ResolveService
{
    public class SoapFault : WebService
    {
        private Logging log = Logging.GetLogger();
        /*
        Function : CreateWebRequest
        DESCRIPTION : This method makes soap message and send request
                       this code is from Microsoft Docs
                       https://docs.microsoft.com/en-us/dotnet/api/system.web.services.protocols.soapexception?view=netframework-4.8
        PARAMETERS : int WebService
                     int method
                     string[] input
        RETURNS : HttpWebRequest: request object which will get response from serivce
        */
        [WebMethod]
        public void myThrow(string message)
        {

            // Build the detail element of the SOAP fault.
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            //System.Xml.XmlNode node = doc.CreateNode(XmlNodeType.Element, SoapException.DetailElementName.Name, SoapException.DetailElementName.Namespace);


            // Build specific details for the SoapException.
            // Add first child of detail XML element.
            //System.Xml.XmlNode details = doc.CreateNode(XmlNodeType.Element, "mySpecialInfo1", "http://localhost/webservice/");
            //System.Xml.XmlNode detailsChild = doc.CreateNode(XmlNodeType.Element, "childOfSpecialInfo", "http://localhost/webservice/");
            //details.AppendChild(detailsChild);


            // Add second child of detail XML element with an attribute.
            //System.Xml.XmlNode details2 = doc.CreateNode(XmlNodeType.Element, "mySpecialInfo2", "http://localhost/webservice/");
            //XmlAttribute attr = doc.CreateAttribute("t", "attrName", "http://localhost/webservice/");
            //attr.Value = "attrValue";
            //details2.Attributes.Append(attr);

            // Append the two child elements to the detail node.
            //node.AppendChild(details);
            //node.AppendChild(details2);

            log.WriteLog(message);
            //Throw the exception.    
            SoapException se = new SoapException(message, SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri);

            throw se;

        }
    }
}