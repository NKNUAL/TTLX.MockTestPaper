using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLX.Controller.ResposeModel
{
    public class MockPaperInfo
    {
        public int PaperId { get; set; }
        public string PaperName { get; set; }
        public string PaperCreateDate { get; set; }
        public string CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public string SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }
        public string RuleNo { get; set; }
        public string RuleName { get; set; }
        public int DanxuanNum { get; set; }
        public int DuoxuanNum { get; set; }
        public int PanduanNum { get; set; }
    }

    public class MockPaperNurseInfo
    {
        public int PaperId { get; set; }
        public string PaperName { get; set; }
        public string PaperCreateDate { get; set; }
        public string CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public string SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }
        public int A1Num { get; set; }
        public int A2Num { get; set; }
        public int A3Num { get; set; }
    }


    public class MockPaperCourseTreeModel
    {
        public string CourseNo { get; set; }
        public string CourseName { get; set; }
        public int QueCount { get; set; }
        public List<MockPaperKnowTreeModel> Knows { get; set; }
    }

    public class MockPaperKnowTreeModel
    {
        public string KnowNo { get; set; }
        public string KnowName { get; set; }
        public int QueCount { get; set; }
        public List<QuestionsInfoModel> Questions { get; set; }
    }



}