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
using TTLX.Controller;
using TTLX.Controller.Model;
using TTLX.Controller.RequestModel;
using TTLX.Controller.ResposeModel;

namespace TTLX.MockTestPaper_V2
{
    public partial class FrmEditRecord : Form
    {
        private FrmEditRecord()
        {
            InitializeComponent();
        }

        public FrmEditRecord(List<QuestionRule> rules) : this()
        {
            this._rules = rules;
        }

        List<QuestionRule> _rules;
        List<EditPaperRecord> _records;

        public delegate List<EditPaperRecord> FuncRecord(string userId, int specialtyId, out string message);

        private void FrmEditRecord_Load(object sender, EventArgs e)
        {
            cbRules.ItemHeight = 30;

            cbRules.Items.AddRange(_rules.ToArray());

            GetPaperRecord(null);
        }


        private void GetPaperRecord(string ruleNo)
        {
            FuncRecord func = WebApiController.Instance.GetLocalPaperRecord;

            func.BeginInvoke(Global.Instance.LexueID, Global.Instance.CurrentSpecialtyID, out string message, BindRecord, func);
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


        private void dgRecord_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 5)
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
