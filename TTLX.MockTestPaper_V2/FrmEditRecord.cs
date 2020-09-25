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


        private void FrmEditRecord_Load(object sender, EventArgs e)
        {
            cbRules.Items.AddRange(_rules.ToArray());

            GetPaperRecord(null);
        }


        private void GetPaperRecord(string ruleNo)
        {
            Func<string, List<EditPaperRecord>> func = QuestionController.Instance.GetPaperRecords;

            func.BeginInvoke(ruleNo, BindRecord, func);
        }

        private void BindRecord(IAsyncResult asyncResult)
        {
            var func = asyncResult.AsyncState as Func<string, List<EditPaperRecord>>;

            var records = func.EndInvoke(asyncResult);

            Action action = delegate ()
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

                }
            };

            dgRecord.Invoke(action);

        }

        private void dgRecord_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 5)
            {
                var que = dgRecord.Rows[e.RowIndex].Tag as EditPaperRecord;
                PutQuestionModel putQuestions = new PutQuestionModel();
                putQuestions.RuleNo = que.RuleNo;
                putQuestions.UserId = Global.Instance.LexueID;
                putQuestions.UserName = Global.Instance.UserName;

                var rule = _rules.Find(r => r.RuleNo == que.RuleNo);

                foreach (var item in que.DicQuestions)
                {

                }

                FrmQuestion frmQuestion = new FrmQuestion(rule, putQuestions, que.PGuid);
                frmQuestion.ShowDialog();

            }
        }

        private void cbRules_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRules.SelectedItem is QuestionRule)
            {
                var rule = cbRules.SelectedItem as QuestionRule;

                GetPaperRecord(rule.RuleNo);
            }
        }
    }
}
