using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTLX.Common.Model
{
    public class QuetionSerializeModel
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
        public Dictionary<string, Dictionary<string, QuestionsModel>> DicQuestions { get; set; }
    }

    public class QuestionsModel
    {
        public string No { get; set; }
        public string QueContent { get; set; }
        public int QueType { get; set; }
        public string Option0 { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Option5 { get; set; }
        public string ResolutionTips { get; set; }
        public string Anwser { get; set; }
        public byte[] NameImg { get; set; }
        public byte[] Option0Img { get; set; }
        public byte[] Option1Img { get; set; }
        public byte[] Option2Img { get; set; }
        public byte[] Option3Img { get; set; }
        public byte[] Option4Img { get; set; }
        public byte[] Option5Img { get; set; }
    }
}
