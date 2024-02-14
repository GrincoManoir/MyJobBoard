using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application
{
    public class RequestResult<TData>
    {
        public bool IsSuccess { get; init; }
        public TData? Data { get; init; }
        public List<string> Errors { get; init; }
        public ApplicationResult ApplicationResult { get; set; }
        private RequestResult()
        {
            Errors = new List<string>();
        }
        public static RequestResult<TData> Success(TData data, ApplicationResult applicationResult = ApplicationResult.OK)
        {
            return new RequestResult<TData>
            {
                IsSuccess = true,
                Data = data,
                ApplicationResult = applicationResult
            };

        }

        public static RequestResult<TData> Success()
        {
            return new RequestResult<TData>
            {
                IsSuccess = true,
                ApplicationResult = ApplicationResult.NO_CONTENT
            };

        }

        public static RequestResult<TData> Failure(string error, ApplicationResult applicationResult = ApplicationResult.BAD_REQUEST)
        {
            var requestResult = new RequestResult<TData>
            {
                IsSuccess = false,
                Data = default,
                ApplicationResult = applicationResult
            };
            requestResult.Errors.Add(error);
            return requestResult;

        }
    }

    public enum ApplicationResult
    {
        OK,
        PARTIAL,
        BAD_REQUEST,
        NOT_FOUND,
        CONFLICT,
        NO_CONTENT
    }
}
