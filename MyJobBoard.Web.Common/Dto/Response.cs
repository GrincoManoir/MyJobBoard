using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Web.Common.Dto
{
    public class Response<T> where T : class
    {
        public string Error { get; set; }

        public T Data { get; set; }



    }
}
