using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTLX.Common;
using TTLX.Controller;
using TTLX.Controller.ResposeModel;

namespace TTLX.MockTestPaper_V2
{
    public partial class FrmRuleManager : Form
    {

        List<QuestionRule> _rules;

        public FrmRuleManager()
        {
            InitializeComponent();
        }



        private void btnAddRule_Click(object sender, EventArgs e)
        {
            //FrmAddRule frmAddRule = new FrmAddRule();

            //if (frmAddRule.ShowDialog() == DialogResult.OK)
            //{
            //    Reset();
            //}

            FrmAddRule2 frmAddRule = new FrmAddRule2();
            frmAddRule.ShowDialog();
        }

        private void cbRules_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRules.SelectedItem is QuestionRule rule)
            {
                lblSpecialtyName.Text = Global.Instance.CurrentSpecialtyName;
                lblRuleName.Text = rule.RuleName;
                tbRuleDesc.Text = rule.RuleDesc;
                LoadRuleTree(rule);
            }
        }

        private void LoadRuleTree(QuestionRule rule)
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
            if (danxuanCount > 0)
                text += $"多选题{duoxuanCount}道。";
            if (danxuanCount > 0)
                text += $"判断题{panduanCount}道。";

            TreeNode rootNode = new TreeNode() { Text = text };
            ruleTree.Nodes.Add(rootNode);

            foreach (var course in rule.CourseRules.OrderBy(c => c.CourseNo))
            {
                TreeNode ccNode = new TreeNode() { Text = course.ToString() + $"({course.QueCount})" };
                rootNode.Nodes.Add(ccNode);

                string courseText = "";

                if (course.DanxuanCount > 0)
                    courseText += $"单选题{course.DanxuanCount}道；";
                if (course.DuoxuanCount > 0)
                    courseText += $"多选题{course.DuoxuanCount}道；";
                if (course.PanduanCount > 0)
                    courseText += $"判断题{course.PanduanCount}道；";

                ccNode.Nodes.Add(courseText);

                if (course.KnowRules != null)
                {
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

                        ccNode.Nodes.Add(knowNode);
                    }
                }
            }
        }

        private void FrmRuleManager_Load(object sender, EventArgs e)
        {
            this.Text = $"出题规则管理-{Global.Instance.CurrentSpecialtyName}";

            ruleTree.ItemHeight = 30;
            cbRules.ItemHeight = 30;
            Reset();
        }

        private void Reset()
        {
            _rules = CacheController.Instance(Global.Instance.CurrentSpecialtyID.ToString()).GetRules();

            cbRules.Items.Clear();
            cbRules.Items.AddRange(_rules.ToArray());

            lblSpecialtyName.Text = "";
            lblRuleName.Text = "";
            tbRuleDesc.Text = "";

            ruleTree.Nodes.Clear();
        }


        private void btnDelRule_Click(object sender, EventArgs e)
        {
            if (cbRules.SelectedItem is QuestionRule rule)
            {

                if (MessageBox.Show($"确定删除【{rule.RuleName}】规则吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    bool resultBool = WebApiController.Instance.DelRule(Global.Instance.LexueID, rule.RuleNo, out bool success, out string message);

                    if (resultBool)
                    {
                        if (success)
                        {
                            MessageBox.Show("删除成功！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _rules.Remove(rule);
                            Reset();
                            return;
                        }
                        else
                        {
                            MessageBox.Show($"删除失败！{message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"删除失败！{message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }
            }
        }

        private void btnEditRule_Click(object sender, EventArgs e)
        {
            if (cbRules.SelectedItem is QuestionRule rule)
            {
                //FrmAddRule frmAddRule = new FrmAddRule(rule);
                //if (frmAddRule.ShowDialog() == DialogResult.OK)
                //{
                //    Reset();
                //}
            }

        }
    }
}
