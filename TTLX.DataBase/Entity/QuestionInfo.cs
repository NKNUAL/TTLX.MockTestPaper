using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTLX.DataBase.Entity
{
    [Table("QuestionInfo")]
    public class QuestionInfo
    {
        [Key]
        public string QGuid { get; set; }
        public string CourseNo { get; set; }
        public string KnowNo { get; set; }
        public string QueContent { get; set; }
        public int QueType { get; set; }
        public int DifficultLevel { get; set; }
        public string Option0 { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Option5 { get; set; }
        public string ResolutionTips { get; set; }
        public string Answer { get; set; }
        public byte[] NameImg { get; set; }
        public byte[] Option0Img { get; set; }
        public byte[] Option1Img { get; set; }
        public byte[] Option2Img { get; set; }
        public byte[] Option3Img { get; set; }
        public byte[] Option4Img { get; set; }
        public byte[] Option5Img { get; set; }
    }
}
