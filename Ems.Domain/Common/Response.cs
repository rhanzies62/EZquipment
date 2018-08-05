using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Domain.Common
{
    public class Response
    {
        public Response()
        {
            Status = (int)ResponseType.Undefined;

            Message = String.Empty;
        }

        public Response(ResponseType statusCode)
        {
            Status = (int)statusCode;
        }

        public Response(ResponseType statusCode, string message) : this(statusCode)
        {
            Message = message;
        }

        public Response(ResponseType statusCode, string message, dynamic data) : this(statusCode,message)
        {
            Data = data;
        }

        public int Status { get; set; }

        public string Message { get; set; }

        public ResponseType StatusType
        {
            set
            {
                this.Status = (int)value;
            }
            get
            {
                return (ResponseType)Status;
            }
        }

        public bool IsSuccess
        {
            get
            {
                return ((ResponseType)Status) == ResponseType.Success;
            }
        }

        public dynamic Data { get; set; }
    }

    public enum ResponseType
    {
        Undefined = 0,

        Error = 1,

        Information = 2,

        Warning = 3,

        Success = 4,

        Critical = 5
    }
}
