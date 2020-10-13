using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTLX.Common;

namespace TTLX.Controller.Model
{
    public class RuleSetHelper
    {
        public QuestionsType QueType { get; set; }
        public int MaxQueCount { get; set; }
        public List<RuleSetHelper_Course> Courses { get; set; }


        public bool Check()
        {
            return Courses.Sum(c => c.SetCount) <= MaxQueCount;
        }
    }

    public class RuleSetHelper_Course
    {
        public string CourseNo { get; set; }
        public int SetCount { get; set; }
        public List<RuleSetHelper_Know> Knows { get; set; }
    }

    public class RuleSetHelper_Know
    {
        public string KnowNo { get; set; }
        public string KnowName { get; set; }
        public int SetCount { get; set; }
        public override string ToString()
        {
            return KnowName + $"({SetCount})";
        }
    }
}
