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
            LoadRuleTree();
        }

        private void LoadRuleTree()
        {
            TreeNode rootNode = new TreeNode() { Text = $"{specialtyName}模拟试卷出题规则" };
            ruleTree.Nodes.Add(rootNode);

            if (rule == null || rule.CourseRules == null || rule.CourseRules.Count == 0)
                return;

            foreach (var course in rule.CourseRules.OrderBy(c => c.No))
            {
                TreeNode ccNode = new TreeNode() { Text = course.ToString(), Tag = course };
                rootNode.Nodes.Add(ccNode);

                if (course.KnowRules != null)
                {
                    foreach (var know in course.KnowRules.OrderBy(k => k.No))
                    {
                        TreeNode knowNode = new TreeNode() { Text = know.ToString(), Tag = know };
                        ccNode.Nodes.Add(knowNode);
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
