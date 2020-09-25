using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTLX.Controller.ResposeModel;

namespace TTLX.Controller.Model
{
    public class EditPaperRecord
    {
        public string RuleNo { get; set; }
        public string PGuid { get; set; }
        public string EditDate { get; set; }
        public Dictionary<string, Dictionary<string, List<QuestionsInfoModel>>> DicQuestions { get; set; }
    }
}
