using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservas.ServiceLayer.Support
{
    public class BusinessException : Exception
    {
        public int Code { get; set; }
    }

    public class Response<T>
    {
        public T Data { get; set; }

        public int Code { get; set; }

        public string Message { get; set; }

        public bool HasError => Code > 0;
    }
}
