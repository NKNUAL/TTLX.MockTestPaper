using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTLX.Controller.ResposeModel
{
    class HttpResultModel
    {
        public bool success { get; set; }

        public string message { get; set; }

        public dynamic data { get; set; }
    }
}
