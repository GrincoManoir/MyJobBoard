using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Web.Common.Dto
{
    public class ErrorResponse
    {
        public List<string> Errors { get; set; }
        public ErrorResponse(params string[] errors)
        {
            Errors = [.. errors];
        }
    }
}
