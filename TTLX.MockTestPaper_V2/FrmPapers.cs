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
    public partial class FrmPapers : Form
    {
        public FrmPapers()
        {
            InitializeComponent();
        }

        public delegate List<QuestionRule> FuncRules(string userId, out string message);
        public delegate List<MockPaperInfo> FuncPapers(string userId, string ruleNo, out string message);

        private void FrmPapers_Load(object sender, EventArgs e)
        {
            LoadRule();
        }


        private void LoadRule()
        {
            ruleTree.Nodes.Clear();

            FuncRules func = WebApiController.Instance.GetAllRules;

            func.BeginInvoke(Global.Instance.LexueID, out string message, BindRules, func);
        }


        private void BindRules(IAsyncResult asyncResult)
        {
            var func = asyncResult.AsyncState as FuncRules;

            var rules = func.EndInvoke(out string message, asyncResult);

            if (rules == null)
            {
                MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            Action action = delegate ()
            {

                foreach (var rule in rules)
                {
                    TreeNode rootNode = new TreeNode() { Text = rule.RuleName, Tag = rule };
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
            };

            ruleTree.Invoke(action);
        }

        private void btnAddRule_Click(object sender, EventArgs e)
        {
            FrmAddRule frmAddRule = new FrmAddRule();

            if (frmAddRule.ShowDialog() == DialogResult.OK)
            {
                LoadRule();
            }
        }

        private void ruleTree_DoubleClick(object sender, EventArgs e)
        {
            //MessageBox.Show("双击");
        }

        private void ruleTree_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("单击");
        }

        private void btnShowPaper_Click(object sender, EventArgs e)
        {
            if (ruleTree.SelectedNode == null)
            {
                MessageBox.Show("请您选择规则!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            TreeNode node = ruleTree.SelectedNode;
            while (node.Tag is SubRule)
            {
                node = node.Parent;
            }

            if (node.Tag is QuestionRule rule)
            {

                FuncPapers func = WebApiController.Instance.GetPapers;

                func.BeginInvoke(Global.Instance.LexueID, rule.RuleNo, out string message, BindPapers, func);
            }


        }


        private void BindPapers(IAsyncResult asyncResult)
        {
            var func = asyncResult.AsyncState as FuncPapers;

            var papers = func.EndInvoke(out string message, asyncResult);

            if (papers == null)
            {
                MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Action action = delegate ()
            {

                #region 添加表头
                if (dgPapers.Columns.Count == 0)
                {
                    dgPapers.Columns.Add(new DataGridViewColumn
                    {
                        HeaderText = "试卷ID",
                        CellTemplate = new DataGridViewTextBoxCell(),
                        FillWeight = 1
                    });
                    dgPapers.Columns.Add(new DataGridViewColumn
                    {
                        HeaderText = "序号",
                        CellTemplate = new DataGridViewTextBoxCell(),
                        FillWeight = 5
                    });
                    dgPapers.Columns.Add(new DataGridViewColumn
                    {
                        HeaderText = "专业",
                        CellTemplate = new DataGridViewTextBoxCell(),
                        FillWeight = 15
                    });
                    dgPapers.Columns.Add(new DataGridViewColumn
                    {
                        HeaderText = "试卷名称",
                        CellTemplate = new DataGridViewTextBoxCell(),
                        FillWeight = 15
                    });
                    dgPapers.Columns.Add(new DataGridViewColumn
                    {
                        HeaderText = "单选题数量",
                        CellTemplate = new DataGridViewTextBoxCell(),
                        FillWeight = 15
                    });
                    dgPapers.Columns.Add(new DataGridViewColumn
                    {
                        HeaderText = "多选题数量",
                        CellTemplate = new DataGridViewTextBoxCell(),
                        FillWeight = 15
                    });
                    dgPapers.Columns.Add(new DataGridViewColumn
                    {
                        HeaderText = "判断题数量",
                        CellTemplate = new DataGridViewTextBoxCell(),
                        FillWeight = 15
                    });
                    dgPapers.Columns.Add(new DataGridViewColumn
                    {
                        HeaderText = "创建人",
                        CellTemplate = new DataGridViewTextBoxCell(),
                        FillWeight = 15
                    });
                    dgPapers.Columns.Add(new DataGridViewColumn
                    {
                        HeaderText = "创建规则",
                        CellTemplate = new DataGridViewTextBoxCell(),
                        FillWeight = 15
                    });
                    dgPapers.Columns.Add(new DataGridViewColumn
                    {
                        HeaderText = "创建时间",
                        CellTemplate = new DataGridViewTextBoxCell(),
                        FillWeight = 15
                    });
                    dgPapers.Columns[0].Visible = false;
                    dgPapers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgPapers.EnableHeadersVisualStyles = false;
                    dgPapers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                    dgPapers.RowTemplate.Height = 30;
                    dgPapers.AllowUserToAddRows = false;
                }
                #endregion

                dgPapers.Rows.Clear();
                foreach (var paper in papers)
                {
                    int index = dgPapers.Rows.Add();
                    dgPapers.Rows[index].Cells[0].Value = paper.PaperId;
                    dgPapers.Rows[index].Cells[1].Value = index + 1;
                    dgPapers.Rows[index].Cells[2].Value = paper.SpecialtyName;
                    dgPapers.Rows[index].Cells[3].Value = paper.PaperName;
                    dgPapers.Rows[index].Cells[4].Value = paper.DanxuanNum;
                    dgPapers.Rows[index].Cells[5].Value = paper.DuoxuanNum;
                    dgPapers.Rows[index].Cells[6].Value = paper.PanduanNum;
                    dgPapers.Rows[index].Cells[7].Value = paper.CreateUserName;
                    dgPapers.Rows[index].Cells[8].Value = paper.RuleName;
                    dgPapers.Rows[index].Cells[9].Value = paper.PaperCreateDate;
                }
            };
            dgPapers.Invoke(action);
        }

        private void btnQuestion_Click(object sender, EventArgs e)
        {

            if (ruleTree.SelectedNode == null)
            {
                MessageBox.Show("请您选择规则!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            TreeNode node = ruleTree.SelectedNode;
            while (node.Tag is SubRule)
            {
                node = node.Parent;
            }

            if (node.Tag is QuestionRule rule)
            {
                FrmQuestion frmQuestion = new FrmQuestion(rule);
                frmQuestion.ShowDialog();
            }


        }
    }
}
