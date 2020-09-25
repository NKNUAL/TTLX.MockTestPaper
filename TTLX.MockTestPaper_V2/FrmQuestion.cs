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
using TTLX.Common.Expand;
using TTLX.Controller;
using TTLX.Controller.RequestModel;
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

        private PutQuestionModel _putQuestion;

        private string _p_guid;

        public FrmQuestion(QuestionRule rule) : this()
        {
            this._rule = rule;
            this._putQuestion = new PutQuestionModel
            {
                RuleNo = rule.RuleNo,
                Courses = new List<PutQuestionCourseModel>(),
                UserId = Global.Instance.LexueID,
                UserName = Global.Instance.UserName,
            };
            _p_guid = Guid.NewGuid().GetGuid();
        }

        public FrmQuestion(QuestionRule rule, PutQuestionModel putQuestion, string pGuid) : this()
        {
            this._rule = rule;
            this._putQuestion = putQuestion;
            _p_guid = pGuid;
        }

        private void FrmQuestion2_Load(object sender, EventArgs e)
        {
            this.lblRuleName.Text = _rule.RuleName;
            this.tbRuleDesc.Text = _rule.RuleDesc;

            LoadRuleTree();

            Task.Factory.StartNew(ExeSaveRecord);
        }

        private void ExeSaveRecord()
        {
            QuestionController.Instance.SavePaper(_p_guid, _rule.RuleNo);
        }

        private void LoadRuleTree()
        {
            if (_rule == null || _rule.CourseRules == null || _rule.CourseRules.Count == 0)
                return;

            ruleTree.Nodes.Clear();

            foreach (var course in _rule.CourseRules.OrderBy(c => c.No))
            {
                TreeNode ccNode = new TreeNode() { Text = course.ToString(), Tag = course };
                ruleTree.Nodes.Add(ccNode);

                if (course.KnowRules != null)
                {

                    int hadFinishCount = 0;
                    foreach (var know in course.KnowRules.OrderBy(k => k.No))
                    {
                        TreeNode knowNode = new TreeNode() { Tag = know };
                        ccNode.Nodes.Add(knowNode);

                        var ques = GetQuestionModel(course.No, know.No);

                        if (ques.Count == know.QueCount)
                            knowNode.ForeColor = Color.Blue;
                        else
                            knowNode.ForeColor = Color.Red;
                        hadFinishCount += ques.Count;
                        knowNode.Text = $"{know.Name}（{ques.Count}/{know.QueCount}）";

                    }

                    ccNode.Text = $"{course.Name}（{hadFinishCount}/{course.QueCount}）";
                    if (hadFinishCount == course.QueCount)
                        ccNode.ForeColor = Color.Blue;
                    else
                        ccNode.ForeColor = Color.Red;

                }
            }
        }

        private void ruleTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null || e.Node.Parent == null)
                return;

            if (e.Node.Tag is SubRule && e.Node.Parent.Tag is SubRule)
            {

                var courseRule = e.Node.Parent.Tag as SubRule;
                var knowRule = e.Node.Tag as SubRule;

                InitDgPaper(courseRule, knowRule);

            }
        }


        private void InitDgPaper(SubRule courseRule, SubRule knowRule)
        {
            var ques = GetQuestionModel(courseRule.No, knowRule.No);

            dgvQuestions.Rows.Clear();
            foreach (var item in ques)
            {
                int index = dgvQuestions.Rows.Add();
                dgvQuestions.Rows[index].Cells[0].Tag = courseRule;
                dgvQuestions.Rows[index].Cells[0].Value = courseRule.Name;
                dgvQuestions.Rows[index].Cells[1].Tag = knowRule;
                dgvQuestions.Rows[index].Cells[1].Value = knowRule.Name;
                dgvQuestions.Rows[index].Cells[2].Value = Global.Instance.QueTypeConvertToString(item.QueType);
                dgvQuestions.Rows[index].Cells[3].Value = item.QueContent;
                dgvQuestions.Rows[index].Cells[4].Value = "修改";
                dgvQuestions.Rows[index].Tag = item;
            }

            for (int i = 0; i < knowRule.QueCount - ques.Count; i++)
            {
                int index = dgvQuestions.Rows.Add();
                dgvQuestions.Rows[index].Cells[0].Tag = courseRule;
                dgvQuestions.Rows[index].Cells[0].Value = courseRule.Name;
                dgvQuestions.Rows[index].Cells[1].Tag = knowRule;
                dgvQuestions.Rows[index].Cells[1].Value = knowRule.Name;
                dgvQuestions.Rows[index].Cells[2].Value = "";
                dgvQuestions.Rows[index].Cells[3].Value = "";
                dgvQuestions.Rows[index].Cells[4].Value = "出题";
            }
        }

        private List<QuestionsInfoModel> GetQuestionModel(string courseNo, string knowNo)
        {
            var putCourse = _putQuestion.Courses.Find(c => c.CourseNo == courseNo);
            if (putCourse == null)
                return new List<QuestionsInfoModel>();

            var putKnow = putCourse.Knows.Find(k => k.KnowNo == knowNo);
            if (putKnow == null)
                return new List<QuestionsInfoModel>();

            return putKnow.Questions ?? new List<QuestionsInfoModel>();
        }

        public void SetQuestionModel(string courseNo, string knowNo, QuestionsInfoModel que)
        {
            var putCourse = _putQuestion.Courses.Find(c => c.CourseNo == courseNo);
            if (putCourse == null)
            {
                putCourse = new PutQuestionCourseModel
                {
                    CourseNo = courseNo,
                    Knows = new List<PutQuestionKnowModel>()
                };
                _putQuestion.Courses.Add(putCourse);
            }
            var putKnow = putCourse.Knows.Find(k => k.KnowNo == knowNo);
            if (putKnow == null)
            {
                putKnow = new PutQuestionKnowModel
                {
                    KnowNo = knowNo,
                    Questions = new List<QuestionsInfoModel>()
                };
                putCourse.Knows.Add(putKnow);
            }
            putKnow.Questions.Add(que);
        }


        private void dgvQuestions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 4)
            {
                FrmCreateQuestion frmCreateQuestion = null;

                var courseRule = dgvQuestions.Rows[e.RowIndex].Cells[0].Tag as SubRule;
                var knowRule = dgvQuestions.Rows[e.RowIndex].Cells[1].Tag as SubRule;

                if (dgvQuestions.Rows[e.RowIndex].Tag is QuestionsInfoModel)//编辑题目
                {
                    var que = dgvQuestions.Rows[e.RowIndex].Tag as QuestionsInfoModel;
                    frmCreateQuestion = new FrmCreateQuestion(que, courseRule, knowRule, _p_guid);
                    if (frmCreateQuestion.ShowDialog() == DialogResult.OK)
                    {
                        LoadRuleTree();
                        InitDgPaper(courseRule, knowRule);
                    }
                }
                else//增加题目
                {
                    frmCreateQuestion = new FrmCreateQuestion(courseRule, knowRule, _p_guid);
                    if (frmCreateQuestion.ShowDialog() == DialogResult.OK)
                    {
                        SetQuestionModel(courseRule.No, knowRule.No, frmCreateQuestion._question);
                        LoadRuleTree();
                        InitDgPaper(courseRule, knowRule);
                    }

                }

            }
        }
    }
}
