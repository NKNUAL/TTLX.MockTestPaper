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
using TTLX.Common.Enum;
using TTLX.Controller;
using TTLX.Controller.Model;
using TTLX.Controller.RequestModel;
using TTLX.Controller.ResposeModel;
using TTLX.MockTestPaper_V2.Nurse;

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
            //this.Text = $"我的模拟试卷-[{Global.Instance.CurrentSpecialtyName}]";

            lblUserName.Text = $"欢迎您：{Global.Instance.UserName}";

            if (Global.Instance.CurrentSpecialtyID == (int)SpecialtyType.SU)//护理专业
            {
                ControlsHide();
            }
            else
            {
                ControlsShow();

                LoadRule();
            }

            LoadPapers();
        }

        private void ControlsHide()
        {
            lblCbName.Visible = false;
            cbRules.Visible = false;
            btnRuleManager.Visible = false;
        }

        private void ControlsShow()
        {
            lblCbName.Visible = true;
            cbRules.Visible = true;
            btnRuleManager.Visible = true;
        }


        private void LoadRule()
        {
            cbRules.Items.Clear();

            FuncRules func = WebApiController.Instance.GetAllRules;

            func.BeginInvoke(Global.Instance.LexueID, out string message, BindRules, func);
        }

        private void LoadPapers()
        {
            FuncPapers func = WebApiController.Instance.GetPapers;

            func.BeginInvoke(Global.Instance.LexueID, null, out string message, BindPapers, func);
        }


        List<QuestionRule> rules;

        private void BindRules(IAsyncResult asyncResult)
        {
            var func = asyncResult.AsyncState as FuncRules;

            rules = func.EndInvoke(out string message, asyncResult);

            if (rules == null)
            {
                MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            Action action = delegate ()
            {
                cbRules.Items.Clear();
                cbRules.Items.AddRange(rules.ToArray());
            };

            cbRules.Invoke(action);
        }

        private void btnAddRule_Click(object sender, EventArgs e)
        {
            FrmRuleManager frmRuleManager = new FrmRuleManager();

            frmRuleManager.ShowDialog();

            LoadRule();

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
                    dgPapers.Columns.Add(new DataGridViewColumn
                    {
                        HeaderText = "操作",
                        CellTemplate = new DataGridViewTextBoxCell(),
                        FillWeight = 15
                    });
                    dgPapers.Columns[0].Visible = false;
                    dgPapers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgPapers.EnableHeadersVisualStyles = false;
                    dgPapers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                    dgPapers.RowTemplate.Height = 30;
                    dgPapers.AllowUserToAddRows = false;
                    dgPapers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgPapers.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
                    dgPapers.Rows[index].Cells[8].Tag = paper.RuleNo;
                    dgPapers.Rows[index].Cells[9].Value = paper.PaperCreateDate;
                    dgPapers.Rows[index].Cells[10].Value = "查看试卷";
                    dgPapers.Rows[index].Cells[10].Style.ForeColor = Color.Blue;
                }
            };
            dgPapers.Invoke(action);
        }

        private void btnQuestion_Click(object sender, EventArgs e)
        {
            if (Global.Instance.CurrentSpecialtyID == (int)SpecialtyType.SU)//护理专业
            {
                //获取护理出题专业规则
                var rule_nurse = WebApiController.Instance.GetAllRules_Nurse(Global.Instance.LexueID, out string message);

                FrmQuestion_Nurse frmQuestion_Nurse = new FrmQuestion_Nurse(rule_nurse);
                frmQuestion_Nurse.ShowDialog();
            }
            else
            {
                FrmSelectRule frmSelectRule = new FrmSelectRule(rules);
                if (frmSelectRule.ShowDialog() == DialogResult.OK)
                {
                    if (frmSelectRule.SelectedRule != null)
                    {
                        FrmQuestion frmQuestion = new FrmQuestion(frmSelectRule.SelectedRule);
                        frmQuestion.ShowDialog();
                    }
                }
            }
        }

        private void btnEditRecord_Click(object sender, EventArgs e)
        {
            if (Global.Instance.CurrentSpecialtyID == (int)SpecialtyType.SU)
            {
                FrmEditRecord frmEditRecord = new FrmEditRecord();

                frmEditRecord.ShowDialog();
            }
            else
            {
                if (rules != null)
                {

                    FrmEditRecord frmEditRecord = new FrmEditRecord(rules);

                    frmEditRecord.ShowDialog();
                }
            }




        }

        private void dgPapers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 10)//查看试卷
            {
                string ruleNo = dgPapers.Rows[e.RowIndex].Cells[8].Tag.ToString();

                var rule = rules.Find(r => r.RuleNo == ruleNo);

                PutQuestionModel putQuestions = new PutQuestionModel
                {
                    RuleNo = ruleNo,
                    UserId = Global.Instance.LexueID,
                    UserName = Global.Instance.UserName,
                    Courses = new List<PutQuestionCourseModel>()
                };

                int paperId = int.Parse(dgPapers.Rows[e.RowIndex].Cells[0].Value.ToString());
                var ques = WebApiController.Instance.GetPaperDetails(paperId, out string message);

                if (ques == null)
                {
                    MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (var item_copurse in ques)
                {
                    var course = putQuestions.Courses.Find(c => c.CourseNo == item_copurse.CourseNo);
                    if (course == null)
                    {
                        course = new PutQuestionCourseModel
                        {
                            CourseNo = item_copurse.CourseNo,
                            Knows = new List<PutQuestionKnowModel>()
                        };
                        putQuestions.Courses.Add(course);
                    }

                    foreach (var item_know in item_copurse.Knows)
                    {
                        var know = course.Knows.Find(k => k.KnowNo == item_know.KnowNo);
                        if (know == null)
                        {
                            know = new PutQuestionKnowModel
                            {
                                KnowNo = item_know.KnowNo,
                                Questions = new List<QuestionsInfoModel>()
                            };
                            course.Knows.Add(know);
                        }

                        know.Questions.AddRange(item_know.Questions);
                    }
                }

                FrmQuestion frmQuestion = new FrmQuestion(dgPapers.Rows[e.RowIndex].Cells[3].Value.ToString(), rule, putQuestions);
                frmQuestion.ShowDialog();
            }
        }

        private void cbRules_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRules.SelectedItem is QuestionRule rule)
            {
                //FuncPapers func = WebApiController.Instance.GetPapers;

                //func.BeginInvoke(Global.Instance.LexueID, rule.RuleNo, out string message, BindPapers, func);
            }
        }
    }
}
