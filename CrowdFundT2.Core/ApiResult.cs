using System;
using System.Collections.Generic;
using System.Text;

namespace CrowdFundT2.Core
{
    public class ApiResult<T>
    {
        public T Data { get; set; }
        public string ErrorText { get; set; }
        public StatusCode ErrorCode { get; set; }
        public bool Success => ErrorCode == StatusCode.OK;

        public static ApiResult<T> Successful(T data)
        {
            return new ApiResult<T>
            {
                ErrorCode = StatusCode.OK,
                Data = data
            };
        }

        public static ApiResult<T> Failed(StatusCode code, string text)
        {
            return new ApiResult<T>
            {
                ErrorCode = code,
                ErrorText = text
            };
        }
    }
}
