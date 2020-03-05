using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ApiResult<T>
    {
        public ApiResult()
        {

        }

        public T Data { get; set; }
        public bool Success { get; }
        public string Message { get; }
    }
}
