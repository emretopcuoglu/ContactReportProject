using System;
using System.Collections.Generic;
using System.Text;

namespace ContactReport.Core
{
    public class ApiResult<T> : ApiResult
    {
        public ApiResult()
        {
        }

        public ApiResult(string message, bool success)
            : base(message, success)
        {
        }

        public ApiResult(string message, bool success, T data)
            : base(message, success)
        {
            Data = data;
        }

        public T Data { get; set; }
    }

    public class ApiResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public ApiResult()
        {
        }

        public ApiResult(string message, bool success)
        {
            Success = success;
            Message = message;
        }
    }
}
