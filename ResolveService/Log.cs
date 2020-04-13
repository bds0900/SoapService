/*
File: Log.cs
Project: Assignment3
Programmer: Doosan Beak
First Version: 2019-10-15
Descrption: This file contains Log Class and methods that support program to loggin saop fault message
*/
using System;
using System.IO;

namespace ResolveService
{
    public class Logging
    {
        private static readonly object mutex = new object();
        private static Logging logger = null;
        public StreamWriter writer = null;

        /*
        Function : GetLogger
        DESCRIPTION : This method get logger by using singleton 
        PARAMETERS : no return
        RETURNS : no return
        */
        public static Logging GetLogger()
        {
            lock (mutex)
            {
                if (logger == null)
                {
                    // create logging instance
                    logger = new Logging();
                }
            }
            return logger;
        }


        /*
        Function : WriteLog
        DESCRIPTION : This method open the wriet log in log file
        PARAMETERS : string message
        RETURNS : no return
        */
        public void WriteLog(String msg)
        {
            DateTime dateTime = DateTime.Now;
            String strDate = dateTime.ToString("yyyy-MM-dd HH:mm:ss");

            // need mutex
            OpenLogFile();
            this.writer.WriteLine($"{strDate} {msg}");
            this.writer.Flush();
            CloseLogFile();
        }

        /*
        Function : OpenLogFile
        DESCRIPTION : This method open the writer stream and create log file
        PARAMETERS : no parameter
        RETURNS : no return
        */
        public void OpenLogFile()
        {
            DateTime dateTime = DateTime.Today;
            String strDate = dateTime.ToString("yyyy-MM-dd");
            //String logFileName = Environment.CurrentDirectory + "/DB/"+$"soa.{strDate}.log";
            //String logFileName = $"C:\\Users\\dbeak9336/soa.server.{strDate}.log";
            String logFileName = $"C:\\inetpub\\wwwroot/soa.server.{strDate}.log";


            if (this.writer == null)
            {
                this.writer = new StreamWriter(logFileName, true);
                this.writer.AutoFlush = true;
            }
        }
        /*
        Function : CloseLogFile
        DESCRIPTION : This method cloe the writer stream
        PARAMETERS : no parameter
        RETURNS : no return
        */
        public void CloseLogFile()
        {
            if (this.writer != null)
            {
                this.writer.Close();
                this.writer = null;
            }
        }
    }
}
