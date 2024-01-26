using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransfer.Core.DTOs
{
    public class CustomResponse<T> where T: class
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public ErrorDto Error { get; set; }

        public static CustomResponse<T> Success(T data, int statusCode)
        {
            return new CustomResponse<T> { Data = data, StatusCode = statusCode };
        }

        public static CustomResponse<T> Success(int statusCode)
        {
            return new CustomResponse<T> { Data = default, StatusCode = statusCode};
        }
        public static CustomResponse<T> Fail(ErrorDto errorDto, int statusCode)
        {
            return new CustomResponse<T>
            {
                Error = errorDto,
                StatusCode = statusCode
            };
        }
        public static CustomResponse<T> Fail(string errorMessage, int statusCode)
        {
            var errorDto = new ErrorDto(errorMessage);
            return new CustomResponse<T> { Error = errorDto, StatusCode = statusCode};
        }
    }
}
