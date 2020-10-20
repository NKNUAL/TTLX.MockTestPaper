using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
    public partial class FrmEditRecord : Form
    {
        public FrmEditRecord()
        {
            InitializeComponent();
        }

        public FrmEditRecord(List<QuestionRule> rules) : this()
        {
            this._rules = rules;
        }


        List<QuestionRule> _rules;
        List<EditPaperRecord> _records;

        List<EditPaperRecord_Nurse> _records_nurse;
        QuestionRule_Nurse _rule_nurse;

        public delegate List<EditPaperRecord> FuncRecord(string userId, int specialtyId, out string message);
        public delegate List<EditPaperRecord_Nurse> FuncRecord_Nurse(string userId, int specialtyId, out string message);

        private void FrmEditRecord_Load(object sender, EventArgs e)
        {
            if (Global.Instance.CurrentSpecialtyID == (int)SpecialtyType.SU)
            {
                cbRules.Visible = false;
                lblCbRule.Visible = false;

                _rule_nurse = WebApiController.Instance.GetAllRules_Nurse(Global.Instance.LexueID, out _);

            }
            else
            {
                cbRules.ItemHeight = 30;

                cbRules.Items.AddRange(_rules.ToArray());

                cbRules.Visible = true;
                lblCbRule.Visible = true;

            }

            GetPaperRecord(null);

        }



        private void GetPaperRecord(string ruleNo)
        {
            if (Global.Instance.CurrentSpecialtyID == (int)SpecialtyType.SU)
            {

                FuncRecord_Nurse func = WebApiController.Instance.GetLocalPaperRecord_Nurse;

                func.BeginInvoke(Global.Instance.LexueID, Global.Instance.CurrentSpecialtyID, out string message, BindRecord_Nurse, func);
            }
            else
            {
                FuncRecord func = WebApiController.Instance.GetLocalPaperRecord;

                func.BeginInvoke(Global.Instance.LexueID, Global.Instance.CurrentSpecialtyID, out string message, BindRecord, func);
            }

        }

        private void BindRecord(IAsyncResult asyncResult)
        {
            var func = asyncResult.AsyncState as FuncRecord;

            _records = func.EndInvoke(out string message, asyncResult);

            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show("获取编辑记录失败：" + message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Action action = delegate ()
            {
                InitRecord(_records);
            };

            dgRecord.Invoke(action);

        }

        private void BindRecord_Nurse(IAsyncResult asyncResult)
        {
            var func = asyncResult.AsyncState as FuncRecord_Nurse;

            _records_nurse = func.EndInvoke(out string message, asyncResult);

            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show("获取编辑记录失败：" + message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Action action = delegate ()
            {
                InitRecord(_records_nurse);
            };

            dgRecord.Invoke(action);

        }

        private void InitRecord(List<EditPaperRecord> records)
        {

            lblCount.Text = $"共 {records.Count} 条记录。";

            #region 添加表头
            if (dgRecord.Columns.Count == 0)
            {
                dgRecord.Columns.Add(new DataGridViewColumn
                {
                    HeaderText = "序号",
                    CellTemplate = new DataGridViewTextBoxCell(),
                    FillWeight = 5
                });
                dgRecord.Columns.Add(new DataGridViewColumn
                {
                    HeaderText = "操作时间",
                    CellTemplate = new DataGridViewTextBoxCell(),
                    FillWeight = 15
                });
                dgRecord.Columns.Add(new DataGridViewColumn
                {
                    HeaderText = "单选题数量",
                    CellTemplate = new DataGridViewTextBoxCell(),
                    FillWeight = 15
                });
                dgRecord.Columns.Add(new DataGridViewColumn
                {
                    HeaderText = "多选题数量",
                    CellTemplate = new DataGridViewTextBoxCell(),
                    FillWeight = 15
                });
                dgRecord.Columns.Add(new DataGridViewColumn
                {
                    HeaderText = "判断题数量",
                    CellTemplate = new DataGridViewTextBoxCell(),
                    FillWeight = 15
                });
                dgRecord.Columns.Add(new DataGridViewColumn
                {
                    HeaderText = "操作",
                    CellTemplate = new DataGridViewTextBoxCell(),
                    FillWeight = 15
                });
                dgRecord.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgRecord.EnableHeadersVisualStyles = false;
                dgRecord.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                dgRecord.RowTemplate.Height = 30;
                dgRecord.AllowUserToAddRows = false;
                dgRecord.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgRecord.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            #endregion

            dgRecord.Rows.Clear();
            foreach (var que in records)
            {
                int index = dgRecord.Rows.Add();
                dgRecord.Rows[index].Tag = que;
                dgRecord.Rows[index].Cells[0].Value = index + 1;
                dgRecord.Rows[index].Cells[1].Value = que.EditDate;

                dgRecord.Rows[index].Cells[2].Value =
                     que.DicQuestions.Sum(q => q.Value.Values.Sum(a => a.Count(s => s.QueType == 1)));
                dgRecord.Rows[index].Cells[3].Value =
                     que.DicQuestions.Sum(q => q.Value.Values.Sum(a => a.Count(s => s.QueType == 2)));
                dgRecord.Rows[index].Cells[4].Value =
                     que.DicQuestions.Sum(q => q.Value.Values.Sum(a => a.Count(s => s.QueType == 3)));

                dgRecord.Rows[index].Cells[5].Value = "继续出题";
                dgRecord.Rows[index].Cells[5].Style.ForeColor = Color.Blue;

            }
        }

        private void InitRecord(List<EditPaperRecord_Nurse> records)
        {

            lblCount.Text = $"共 {records.Count} 条记录。";

            #region 添加表头
            if (dgRecord.Columns.Count == 0)
            {
                dgRecord.Columns.Add(new DataGridViewColumn
                {
                    HeaderText = "序号",
                    CellTemplate = new DataGridViewTextBoxCell(),
                    FillWeight = 5
                });
                dgRecord.Columns.Add(new DataGridViewColumn
                {
                    HeaderText = "操作时间",
                    CellTemplate = new DataGridViewTextBoxCell(),
                    FillWeight = 15
                });
                dgRecord.Columns.Add(new DataGridViewColumn
                {
                    HeaderText = "A1数量",
                    CellTemplate = new DataGridViewTextBoxCell(),
                    FillWeight = 15
                });
                dgRecord.Columns.Add(new DataGridViewColumn
                {
                    HeaderText = "A2数量",
                    CellTemplate = new DataGridViewTextBoxCell(),
                    FillWeight = 15
                });
                dgRecord.Columns.Add(new DataGridViewColumn
                {
                    HeaderText = "A3数量",
                    CellTemplate = new DataGridViewTextBoxCell(),
                    FillWeight = 15
                });
                dgRecord.Columns.Add(new DataGridViewColumn
                {
                    HeaderText = "操作",
                    CellTemplate = new DataGridViewTextBoxCell(),
                    FillWeight = 15
                });
                dgRecord.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgRecord.EnableHeadersVisualStyles = false;
                dgRecord.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                dgRecord.RowTemplate.Height = 30;
                dgRecord.AllowUserToAddRows = false;
                dgRecord.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgRecord.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            #endregion

            dgRecord.Rows.Clear();
            foreach (var que in records)
            {
                int index = dgRecord.Rows.Add();
                dgRecord.Rows[index].Tag = que;
                dgRecord.Rows[index].Cells[0].Value = index + 1;
                dgRecord.Rows[index].Cells[1].Value = que.EditDate;

                foreach (var item in _rule_nurse.A_)
                {
                    if (item.TypeId == (int)NurseRuleType.A1)
                    {
                        var model = que.A_.Find(x => x.TypeId == item.TypeId);
                        int count = (model?.Questions?.Count) ?? 0;
                        dgRecord.Rows[index].Cells[2].Value = count;
                    }
                    else if (item.TypeId == (int)NurseRuleType.A2)
                    {
                        var model = que.A_.Find(x => x.TypeId == item.TypeId);
                        int count = (model?.Questions?.Count) ?? 0;
                        dgRecord.Rows[index].Cells[3].Value = count;
                    }
                    else if (item.TypeId == (int)NurseRuleType.A3)
                    {
                        var models = que.A_.FindAll(x => x.TypeId == item.TypeId);
                        int count = 0;

                        if (models == null)
                        {
                            count = 0;
                        }
                        else
                        {
                            foreach (var A3 in models)
                            {
                                if (((A3.Questions?.Count) ?? 0) == item.SubQueCount)
                                    count++;
                            }
                        }
                        dgRecord.Rows[index].Cells[4].Value = count;
                    }
                }

                dgRecord.Rows[index].Cells[5].Value = "继续出题";
                dgRecord.Rows[index].Cells[5].Style.ForeColor = Color.Blue;

            }
        }


        private void dgRecord_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 5)
            {
                if (Global.Instance.CurrentSpecialtyID == (int)SpecialtyType.SU)
                {
                    var que = dgRecord.Rows[e.RowIndex].Tag as EditPaperRecord_Nurse;
                    PutQuestionNurseModel putQuestions = new PutQuestionNurseModel
                    {
                        A_ = que.A_,
                        UserId = Global.Instance.LexueID,
                        UserName = Global.Instance.UserName,
                        RuleNo = que.RuleNo
                    };

                    FrmQuestion_Nurse frmQuestion_Nurse = new FrmQuestion_Nurse(_rule_nurse, putQuestions, que.PGuid);

                    frmQuestion_Nurse.ShowDialog();
                    Thread.Sleep(100);

                    GetPaperRecord(null);
                }
                else
                {

                    var que = dgRecord.Rows[e.RowIndex].Tag as EditPaperRecord;
                    PutQuestionModel putQuestions = new PutQuestionModel
                    {
                        RuleNo = que.RuleNo,
                        UserId = Global.Instance.LexueID,
                        UserName = Global.Instance.UserName,
                        Courses = new List<PutQuestionCourseModel>()
                    };

                    var rule = _rules.Find(r => r.RuleNo == que.RuleNo);

                    foreach (var item_copurse in que.DicQuestions)
                    {
                        var course = putQuestions.Courses.Find(c => c.CourseNo == item_copurse.Key);
                        if (course == null)
                        {
                            course = new PutQuestionCourseModel
                            {
                                CourseNo = item_copurse.Key,
                                Knows = new List<PutQuestionKnowModel>()
                            };
                            putQuestions.Courses.Add(course);
                        }

                        foreach (var item_know in item_copurse.Value)
                        {
                            var know = course.Knows.Find(k => k.KnowNo == item_know.Key);
                            if (know == null)
                            {
                                know = new PutQuestionKnowModel
                                {
                                    KnowNo = item_know.Key,
                                    Questions = new List<QuestionsInfoModel>()
                                };
                                course.Knows.Add(know);
                            }

                            know.Questions.AddRange(item_know.Value);
                        }
                    }

                    FrmQuestion frmQuestion = new FrmQuestion(rule, putQuestions, que.PGuid);
                    frmQuestion.ShowDialog();

                    Thread.Sleep(100);
                    GetPaperRecord(null);
                }


            }
        }

        private void cbRules_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRules.SelectedItem is QuestionRule rule && _records != null)
            {
                var records = _records.FindAll(r => r.RuleNo == rule.RuleNo);
                InitRecord(records);
            }
        }
    }
}
