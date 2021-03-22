using System;
using Entities;

namespace Exceptions
{
    public class BussinessException : Exception
    {
        public int ExceptionId { get; }
        public string Details { get; set; }
        public AppMessage ApplicationMassage { get; set; }

        public BussinessException(int exId, Exception exception)
        {
            ExceptionId = exId;
            Details = exception.Message;
        }

        public BussinessException()
        {
        }
    }
}