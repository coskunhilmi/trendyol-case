using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Responses
{
    public class ResponseWrapper<T>
    {
        public bool Success { get; }
        public string Message { get; }
        public T Data { get; }
        public ResponseWrapper(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
