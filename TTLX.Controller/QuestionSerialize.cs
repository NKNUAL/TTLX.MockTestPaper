using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTLX.Controller.ResposeModel;

namespace TTLX.Controller
{
    public class QuestionSerialize
    {
        /// <summary>
        /// 所属规则
        /// </summary>
        public string RuleNo { get; set; }
        /// <summary>
        /// 是否上传到了服务器
        /// </summary>
        public bool IsUpload { get; set; }
        /// <summary>
        /// 保存的题目
        /// </summary>
        public Dictionary<string, Dictionary<string, QuestionsInfoModel>> DicQuestions { get; set; }



        public static QuestionSerialize DeSerializeClass(string ruleNo)
        {
            string base_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "papers_serialize");
            if (!Directory.Exists(base_path))
                Directory.CreateDirectory(base_path);

            string[] files = Directory.GetFiles(base_path, $"{ruleNo}_*.paper");

            string file_path = Path.Combine(base_path, $"{ruleNo}_{Guid.NewGuid.ToString()}")


        }
    }
}
