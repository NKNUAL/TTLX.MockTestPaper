using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TTLX.Controller.ResposeModel;

namespace TTLX.MockTestPaper_V2
{
    public partial class FrmRulePreview : Form
    {
        public FrmRulePreview()
        {
            InitializeComponent();
        }

        string specialtyName;
        string ruleName;
        string ruleDesc;
        QuestionRule rule;

        public FrmRulePreview(string specialtyName, string ruleName, string ruleDesc, QuestionRule rule) : this()
        {
            this.specialtyName = specialtyName;
            this.ruleName = ruleName;
            this.ruleDesc = ruleDesc;
            this.rule = rule;
        }

        private void FrmRulePreview_Load(object sender, EventArgs e)
        {
            lblSpecialty.Text = specialtyName;
            lblRuleName.Text = ruleName;
            tbRuleDesc.Text = ruleDesc;
            ruleTree.ItemHeight = 30;
            LoadRuleTree();
        }

        private void LoadRuleTree()
        {

            ruleTree.Nodes.Clear();
            if (rule == null || rule.CourseRules == null || rule.CourseRules.Count == 0)
                return;

            int totalQueCount = rule.CourseRules.Sum(c => c.QueCount);
            int danxuanCount = rule.CourseRules.Sum(c => c.DanxuanCount);
            int duoxuanCount = rule.CourseRules.Sum(c => c.DuoxuanCount);
            int panduanCount = rule.CourseRules.Sum(c => c.PanduanCount);

            string text = $"共{totalQueCount}道题目。";
            if (danxuanCount > 0)
                text += $"单选题{danxuanCount}道。";
            if (duoxuanCount > 0)
                text += $"多选题{duoxuanCount}道。";
            if (panduanCount > 0)
                text += $"判断题{panduanCount}道。";

            TreeNode rootNode = new TreeNode() { Text = text };
            ruleTree.Nodes.Add(rootNode);

            foreach (var course in rule.CourseRules.OrderBy(c => c.CourseNo))
            {
                TreeNode ccNode = new TreeNode() { Text = course.CourseName + $"({course.QueCount})" };
                rootNode.Nodes.Add(ccNode);

                string courseText = "";

                if (course.DanxuanCount > 0)
                    courseText += $"单选题{course.DanxuanCount}道；";
                if (course.DuoxuanCount > 0)
                    courseText += $"多选题{course.DuoxuanCount}道；";
                if (course.PanduanCount > 0)
                    courseText += $"判断题{course.PanduanCount}道；";

                ccNode.Nodes.Add(courseText);

                if (course.KnowRules != null && course.KnowRules.Count > 0)
                {
                    TreeNode knowDetail = new TreeNode() { Text = "知识点明细" };
                    ccNode.Nodes.Add(knowDetail);

                    foreach (var know in course.KnowRules.OrderBy(k => k.KnowNo))
                    {
                        TreeNode knowNode = new TreeNode() { Tag = know };
                        string knowText = know.KnowName + $"({know.DanxuanCount ?? 0 + know.DuoxuanCount ?? 0 + know.PanduanCount ?? 0})";
                        if (know.DanxuanCount != null && know.DanxuanCount > 0)
                            knowNode.Nodes.Add($"单选题{know.DanxuanCount}道");
                        if (know.DuoxuanCount != null && know.DuoxuanCount > 0)
                            knowNode.Nodes.Add($"多选题{know.DuoxuanCount}道");
                        if (know.PanduanCount != null && know.PanduanCount > 0)
                            knowNode.Nodes.Add($"判断题{know.PanduanCount}道");
                        knowNode.Text = knowText;
                        knowDetail.Nodes.Add(knowNode);
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
