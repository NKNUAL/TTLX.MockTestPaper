using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTLX.Controller.Model
{
    public class RuleSimpleModel
    {
        public string RuleNo { get; set; }
        public string RuleName { get; set; }

        public override string ToString()
        {
            return RuleName;
        }
    }
}
