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

        private EditMode _editMode;

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

            _editMode = EditMode.Create;
        }


        public FrmQuestion(QuestionRule rule, PutQuestionModel putQuestion, string pGuid) : this()
        {
            this._rule = rule;
            this._putQuestion = putQuestion;
            _p_guid = pGuid;
            _editMode = EditMode.Create;
        }


        public FrmQuestion(string paperName, QuestionRule rule, PutQuestionModel putQuestion) : this()
        {
            this._rule = rule;
            this._putQuestion = putQuestion;
            this.tbPaperName.Text = paperName;
            _editMode = EditMode.Edit;
        }



        private void FrmQuestion2_Load(object sender, EventArgs e)
        {
            this.Text = $"模拟试卷出题-{Global.Instance.CurrentSpecialtyName}";

            lblRuleName.Text = _rule.RuleName;
            tbRuleDesc.Text = _rule.RuleDesc;

            if (_editMode == EditMode.Edit)
            {
                btnQuestion.Enabled = false;//如果是编辑模式，则不需要保存试卷
                tbPaperName.Enabled = false;
            }
            ruleTree.ItemHeight = 30;
            LoadRuleTree();
            Task.Factory.StartNew(ExeSaveRecord);
        }

        /// <summary>
        /// 保存题目到Sqlite
        /// </summary>
        private void ExeSaveRecord()
        {
            if (!string.IsNullOrEmpty(_p_guid))
                QuestionController.Instance.SavePaper(_p_guid, _rule.RuleNo);
        }

        private void ExeDelRecord()
        {
            if (!string.IsNullOrEmpty(_p_guid))
                _ = QuestionController.Instance.DeleteNormalRecord(_p_guid);
        }

        /// <summary>
        /// 加载规则树
        /// </summary>
        private void LoadRuleTree()
        {
            if (_rule == null || _rule.CourseRules == null || _rule.CourseRules.Count == 0)
                return;

            ruleTree.Nodes.Clear();

            foreach (var course in _rule.CourseRules.OrderBy(c => c.No))
            {
                TreeNode ccNode = new TreeNode() { Text = course.ToString(), Tag = course, Name = course.No };
                ruleTree.Nodes.Add(ccNode);

                if (course.KnowRules != null)
                {

                    int hadFinishCount = 0;
                    foreach (var know in course.KnowRules.OrderBy(k => k.No))
                    {
                        TreeNode knowNode = new TreeNode() { Tag = know, Name = course.No + "_" + know.No };
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

        /// <summary>
        /// 加载知识点下的试题
        /// </summary>
        /// <param name="courseRule"></param>
        /// <param name="knowRule"></param>
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

        /// <summary>
        /// 从缓存获取题目
        /// </summary>
        /// <param name="courseNo"></param>
        /// <param name="knowNo"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 保存题目到缓存
        /// </summary>
        /// <param name="courseNo"></param>
        /// <param name="knowNo"></param>
        /// <param name="que"></param>
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
                FrmCreateQuestion frmCreateQuestion;

                var courseRule = dgvQuestions.Rows[e.RowIndex].Cells[0].Tag as SubRule;
                var knowRule = dgvQuestions.Rows[e.RowIndex].Cells[1].Tag as SubRule;

                if (dgvQuestions.Rows[e.RowIndex].Tag is QuestionsInfoModel)//编辑题目
                {
                    var que = dgvQuestions.Rows[e.RowIndex].Tag as QuestionsInfoModel;
                    frmCreateQuestion = new FrmCreateQuestion(que, courseRule, knowRule, _p_guid, _editMode);
                    if (frmCreateQuestion.ShowDialog() == DialogResult.OK)
                    {
                        LoadRuleTree();
                        InitDgPaper(courseRule, knowRule);
                    }
                }
                else//增加题目
                {
                    frmCreateQuestion = new FrmCreateQuestion(courseRule, knowRule, _p_guid, _editMode);
                    if (frmCreateQuestion.ShowDialog() == DialogResult.OK)
                    {
                        SetQuestionModel(courseRule.No, knowRule.No, frmCreateQuestion._question);
                        LoadRuleTree();
                        InitDgPaper(courseRule, knowRule);
                    }

                }

            }
        }

        /// <summary>
        /// 检查题目是否出完
        /// </summary>
        /// <returns></returns>
        private Tuple<string, string> CheckQuestion()
        {
            foreach (var course in _rule.CourseRules.OrderBy(c => c.No))
            {

                if (course.KnowRules != null)
                {

                    foreach (var know in course.KnowRules.OrderBy(k => k.No))
                    {
                        var ques = GetQuestionModel(course.No, know.No);

                        if (ques.Count != know.QueCount)
                            return new Tuple<string, string>(course.No, know.No);

                    }

                }
            }
            return null;
        }

        private void btnQuestion_Click(object sender, EventArgs e)
        {

            var checkResult = CheckQuestion();

            if (checkResult != null)
            {
                var treeNodes = ruleTree.Nodes.Find(checkResult.Item1 + "_" + checkResult.Item2, true);

                if (treeNodes != null && treeNodes.Length > 0)
                {
                    ruleTree.SelectedNode = treeNodes[0];

                    MessageBox.Show("您还有题目未出完，请先出完题目再提交模拟试卷！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(tbPaperName.Text))
                {
                    MessageBox.Show("请您输入模拟试卷名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbPaperName.Focus();
                    return;
                }

                _putQuestion.PaperName = tbPaperName.Text;
                if (WebApiController.Instance.CreatePaper(_putQuestion, out string message))
                {
                    MessageBox.Show("模拟试卷创建成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Task.Factory.StartNew(ExeDelRecord);

                    DialogResult = DialogResult.OK;

                    this.Close();
                    return;
                }
                else
                {
                    MessageBox.Show("模拟试卷创建失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


        }


    }
}
