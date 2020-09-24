using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTLX.Controller.ResposeModel;

namespace TTLX.MockTestPaper_V2
{
    public partial class FrmQuestion : Form
    {


        private FrmQuestion()
        {
            InitializeComponent();
        }

        private QuestionRule _rule;

        public FrmQuestion(QuestionRule rule) : this()
        {
            this._rule = rule;
        }

        private void FrmQuestion_Load(object sender, EventArgs e)
        {
            this.lblRuleName.Text = _rule.RuleName;
            this.tbRuleDesc.Text = _rule.RuleDesc;

            LoadRuleTree();
        }

        private void LoadRuleTree()
        {
            if (_rule == null || _rule.CourseRules == null || _rule.CourseRules.Count == 0)
                return;

            foreach (var course in _rule.CourseRules.OrderBy(c => c.No))
            {
                TreeNode ccNode = new TreeNode() { Text = course.ToString(), Tag = course };
                ruleTree.Nodes.Add(ccNode);

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

        private void ruleTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is )
            { 
            
            }
        }
    }
}
