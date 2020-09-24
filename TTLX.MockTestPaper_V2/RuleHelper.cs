using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTLX.Controller.ResposeModel;

namespace TTLX.MockTestPaper_V2
{
    public class RuleHelper
    {
        /// <summary>
        /// 添加规则
        /// </summary>
        /// <param name="courseNo"></param>
        /// <param name="knowNo"></param>
        /// <param name="knowQueCount"></param>
        public static void AddRule(QuestionRule rule, string courseNo, string courseName, string knowNo, string knowName, int knowQueCount)
        {
            if (rule.CourseRules == null)
                rule.CourseRules = new List<SubRule>();

            var course = rule.CourseRules.Find(c => c.No == courseNo);
            if (course == null)
            {
                course = new SubRule
                {
                    No = courseNo,
                    Name = courseName,
                    KnowRules = new List<SubRule>(),
                };
                rule.CourseRules.Add(course);
            }

            var know = course.KnowRules.Find(k => k.No == knowNo);

            if (know == null)
            {
                know = new SubRule
                {
                    No = knowNo,
                    Name = knowName
                };
                course.KnowRules.Add(know);
            }

            know.QueCount = knowQueCount;
            course.QueCount = course.KnowRules.Sum(k => k.QueCount);
        }

        /// <summary>
        /// 移除规则
        /// </summary>
        public static void DelRule(QuestionRule rule, string courseNo, string knowNo)
        {
            if (rule.CourseRules == null)
                return;

            var course = rule.CourseRules.Find(c => c.No == courseNo);

            if (course != null)
            {
                var know = course.KnowRules.Find(k => k.No == knowNo);

                if (know != null)
                    course.KnowRules.Remove(know);
            }
        }

        /// <summary>
        /// 获取科目下的知识点
        /// </summary>
        /// <param name="courseNo"></param>
        /// <returns></returns>
        public static List<SubRule> GetKnowRules(QuestionRule rule, string courseNo)
        {
            if (rule.CourseRules == null)
                return new List<SubRule>();

            var course = rule.CourseRules.Find(c => c.No == courseNo);

            if (course == null)
                return new List<SubRule>();

            return course.KnowRules ?? new List<SubRule>();

        }
    }
}
