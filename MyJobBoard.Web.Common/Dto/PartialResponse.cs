using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Web.Common.Dto
{
    public class PartialResponse<T> : Response<T> where T : class
    {
        public string AppliedRange { get; set; }
        public bool IsFullCollection { get; set; }

    }
}
