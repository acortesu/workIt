using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Entities;

namespace Exceptions
{
    public class ExceptionManager
    {
        public string PATH;

        private static ExceptionManager _instance;

        private static readonly Dictionary<int, AppMessage>
            Messages = new Dictionary<int, AppMessage>();


        private ExceptionManager()
        {
            LoadMessages();
            PATH = ConfigurationManager.AppSettings.Get("LOG_PATH");
        }

        public static ExceptionManager GetInstance()
        {
            return _instance ?? (_instance = new ExceptionManager());
        }

        /// <summary>
        /// Get message from either business exception or any another exception
        /// to then process it 
        /// </summary>
        /// <param name="ex"></param>
        public void Process(Exception ex)
        {
            var bex = new BussinessException();


            if (ex.GetType() == typeof(BussinessException))
            {
                bex = (BussinessException) ex;
                bex.Details = GetMessage(bex).Message;
            }
            else
            {
                bex = new BussinessException(0, ex);
            }

            ProcessBussinesException(bex);
        }

        private void ProcessBussinesException(BussinessException bex)
        {
            var today = DateTime.Now.ToString("yyyy-MM-dd_hh");
            var logFileName = PATH + today + "_" + "log.txt";

            var message = bex.Details + "\n" + bex.StackTrace + "\n";

            //if (bex.InnerException!=null)
            //    message += bex.InnerException.Message + "\n" + bex.InnerException.StackTrace;

            // Creates a stream writer from file either already exits
            // or create it if it doesn't
            using (StreamWriter w = File.AppendText(logFileName))
            {
                // Write exception in the file,
                // also wraps message with other log details
                Log(message, w);
            }

            bex.ApplicationMassage = GetMessage(bex);

            throw bex;
        }

        /// <summary>
        /// Using exception ID get exception message from messages list
        /// if not assigns a default message
        /// </summary>
        /// <param name="bex"></param>
        /// <returns></returns>
        public AppMessage GetMessage(BussinessException bex)
        {
            var bexId = bex.ExceptionId;
            var appMessage = new AppMessage
            {
                Message = "Message not found!"
            };

            if (Messages.ContainsKey(bexId))
                appMessage = Messages[bexId];

            return appMessage;
        }

        // TODO: Load messages from DB
        private void LoadMessages()
        {
            Messages.Add(3, new AppMessage {Id = 3, Message = "Cliente ya esta registrado"});
            //var crudMessages = new AppMessagesCrudFactory();

            //var lstMessages = crudMessages.RetrieveAll<ApplicationMessa   e>();

            //foreach(var appMessage in lstMessages)
            //{
            //    messages.Add(appMessage.Id, appMessage);
            //}
        }

        private void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
        }
    }
}