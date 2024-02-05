using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyJobBoard.Application;
using MyJobBoard.Web.Common.Dto;

namespace MyJobBoard.Web.Common
{
    public static class RequestResultExtension
    {
        public static ActionResult MapRequestResult<TData,TDto>(this RequestResult<TData> requestResult, TDto? successDto, string actionName = "")
        {
            if (requestResult.IsSuccess)
            {
                if (requestResult.ApplicationResult == ApplicationResult.CREATED)
                {
                    return new CreatedResult(actionName, successDto);
                }

                if(requestResult.ApplicationResult == ApplicationResult.NO_CONTENT)
                {
                    return new NoContentResult();
                }

                return new OkObjectResult(successDto);
            }

            return requestResult.ApplicationResult switch
            {
                ApplicationResult.NOT_FOUND => new NotFoundObjectResult(new ErrorResponse() { Errors = requestResult.Errors }),
                ApplicationResult.CONFLICT => new ConflictObjectResult(new ErrorResponse() { Errors = requestResult.Errors }),
                _ => new BadRequestObjectResult(new ErrorResponse() { Errors = requestResult.Errors })
            };
           
        }
    }
}
