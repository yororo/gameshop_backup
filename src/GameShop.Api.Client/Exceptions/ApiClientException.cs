using System;
using System.Net;

namespace GameShop.Api.Client.Exceptions
{
    public class ApiClientException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        public ApiClientException()
            : base()
        {
        }

        public ApiClientException(string message)
            : base(message)
        {
        }
        public ApiClientException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public ApiClientException(string message, HttpStatusCode statusCode, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }


        public ApiClientException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}