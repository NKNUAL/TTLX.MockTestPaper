using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTLX.Version
{
    public class HttpVersionResult
    {
        /// <summary>
        ///是否需要更新
        /// </summary>
        public bool CheckState { get; set; }
        public string PackageUrl { get; set; }
        public string Version { get; set; }
    }
}
