using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTLX.Controller.ResposeModel;

namespace TTLX.Controller.Model
{
    public class MockPaperQuestionModel
    {

    }

    public class MockPaperCourseModel
    {
        public string CourseNo { get; set; }
        public int QueCount { get; set; }
    }
    public class MockPaperKnowModel
    {
        public string KnowNo { get; set; }
        public int QueCount { get; set; }
        public List<QuestionsInfoModel> Questions { get; set; }
    }
}
