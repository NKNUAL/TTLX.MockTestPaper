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
using TTLX.Controller.Model;
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

        /// <summary>
        /// 首次出题
        /// </summary>
        /// <param name="rule"></param>
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

        /// <summary>
        /// 继续出未完成的试卷
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="putQuestion"></param>
        /// <param name="pGuid"></param>
        public FrmQuestion(QuestionRule rule, PutQuestionModel putQuestion, string pGuid) : this()
        {
            this._rule = rule;
            this._putQuestion = putQuestion;
            _p_guid = pGuid;
            _editMode = EditMode.Create;
        }

        /// <summary>
        /// 编辑已经入库的试卷
        /// </summary>
        /// <param name="paperName"></param>
        /// <param name="rule"></param>
        /// <param name="putQuestion"></param>
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
        /// 设置出题记录数
        /// </summary>
        private void SetCountLabel()
        {
            flowLayoutLabel.Controls.Clear();

            int danxuanTotalCount = _rule.CourseRules.Sum(c => c.DanxuanCount);
            int duoxuanTotalCount = _rule.CourseRules.Sum(c => c.DuoxuanCount);
            int panduanTotalCount = _rule.CourseRules.Sum(c => c.PanduanCount);

            if (_rule.CourseRules.Sum(c => c.DanxuanCount) > 0)
            {
                int hasCount = GetQuestionCount(null, QuestionsType.Danxuan);

                Label lbl = new Label
                {
                    Text = $"单选题已出{hasCount}，剩余{danxuanTotalCount - hasCount}道。",
                    ForeColor = Color.Blue,
                    Font = new Font("宋体", 12),
                    Width = 216,
                    Padding = new Padding(0, 0, 0, 3)
                };
                flowLayoutLabel.Controls.Add(lbl);
            }

            if (_rule.CourseRules.Sum(c => c.DuoxuanCount) > 0)
            {
                int hasCount = GetQuestionCount(null, QuestionsType.Duoxuan);

                Label lbl = new Label
                {
                    Text = $"多选题题已出{hasCount}，剩余{duoxuanTotalCount - hasCount}。",
                    ForeColor = Color.Blue,
                    Font = new Font("宋体", 12),
                    Width = 216,
                    Padding = new Padding(0, 0, 0, 3)
                };
                flowLayoutLabel.Controls.Add(lbl);
            }

            if (_rule.CourseRules.Sum(c => c.PanduanCount) > 0)
            {
                int hasCount = GetQuestionCount(null, QuestionsType.Panduan);

                Label lbl = new Label
                {
                    Text = $"判断题已出{hasCount}，剩余{panduanTotalCount - hasCount}。",
                    ForeColor = Color.Blue,
                    Font = new Font("宋体", 12),
                    Width = 216,
                    Padding = new Padding(0, 0, 0, 3)
                };
                flowLayoutLabel.Controls.Add(lbl);
            }
        }

        /// <summary>
        /// 保存题目到Sqlite
        /// </summary>
        private void ExeSaveRecord()
        {
            if (!string.IsNullOrEmpty(_p_guid))
                QuestionController.Instance.SavePaper(_p_guid, _rule.RuleNo);
        }

        /// <summary>
        /// 上传到服务器后，删除本地记录
        /// </summary>
        private void ExeDelRecord()
        {
            if (_editMode == EditMode.Create)
                _ = QuestionController.Instance.DeleteNormalRecord(_p_guid);
        }

        /// <summary>
        /// 加载规则树
        /// </summary>
        private void LoadRuleTree()
        {

            SetCountLabel();

            if (_rule == null || _rule.CourseRules == null || _rule.CourseRules.Count == 0)
                return;

            ruleTree.Nodes.Clear();

            foreach (var course in _rule.CourseRules.OrderBy(c => c.CourseNo))
            {
                TreeNode ccNode = new TreeNode()
                {
                    Tag = course,
                    Name = course.CourseNo
                };
                ruleTree.Nodes.Add(ccNode);

                //此科目单选题已出题目数
                int danxuanHasCount = GetQuestionCount(course.CourseNo, QuestionsType.Danxuan);
                int duoxuanHasCount = GetQuestionCount(course.CourseNo, QuestionsType.Duoxuan);
                int panduanHasCount = GetQuestionCount(course.CourseNo, QuestionsType.Panduan);
                int totalHashCount = danxuanHasCount + duoxuanHasCount + panduanHasCount;
                int totalCount = course.DanxuanCount + course.DuoxuanCount + course.PanduanCount;

                ccNode.Text = course.CourseName + $"({totalHashCount}/{totalCount})";

                if (totalHashCount == totalCount)
                    ccNode.ForeColor = Color.Blue;
                else
                    ccNode.ForeColor = Color.Red;

                List<TreeKnowModel> listKnows = new List<TreeKnowModel>();
                //已经出了题的Know
                var course_que = _putQuestion.Courses.Find(c => c.CourseNo == course.CourseNo);
                if (course_que != null)
                {
                    if (course_que.Knows != null && course_que.Knows.Count > 0)
                    {
                        var knows = WebApiController.Instance
                            .GetKnows(Global.Instance.CurrentSpecialtyID.ToString(), course.CourseNo, out _);
                        foreach (var know in course_que.Knows)
                        {
                            int knowQueCount = know.Questions == null ? 0 : know.Questions.Count;
                            listKnows.Add(new TreeKnowModel
                            {
                                Name = course.CourseNo + "_" + know.KnowNo,
                                Tag = know.KnowNo,
                                Text = knows.Find(k => k.Key == know.KnowNo).Value + $"({knowQueCount})"
                            });
                        }
                    }
                }
                //规则中的Know
                if (course.KnowRules != null && course.KnowRules.Count > 0)
                {
                    foreach (var know in course.KnowRules)
                    {
                        string node_key = course.CourseNo + "_" + know.KnowNo;
                        var temp = listKnows.Find(k => k.Name == node_key);
                        if (temp == null)
                        {
                            listKnows.Add(new TreeKnowModel
                            {
                                Name = node_key,
                                Text = know.KnowName + "(0)",
                                Tag = know.KnowNo
                            });
                        }
                    }
                }

                foreach (var know in listKnows.OrderBy(k => k.Tag.ToString()))
                {
                    ccNode.Nodes.Add(new TreeNode
                    {
                        Text = know.Text,
                        Tag = know.Tag,
                        Name = know.Name
                    });
                }
            }
        }

        TreeNode _currSelectEdNode = null;

        private void ruleTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _currSelectEdNode = e.Node;

            if (e.Node == null)
                return;

            if (e.Node.Tag is CourseRule courseRule)
            {
                InitDgPaper_Course(courseRule);
            }
            else if (e.Node.Parent != null && e.Node.Parent.Tag is CourseRule courseRule2 && e.Node.Tag is string knowNo)
            {
                InitDgPaper_Know(courseRule2, knowNo);
            }

        }

        /// <summary>
        /// 加载科目下的试题
        /// </summary>
        private void InitDgPaper_Course(CourseRule courseRule)
        {
            var ques = GetQuestionModel(courseRule.CourseNo, null);
            dgvQuestions.Rows.Clear();

            var knows = WebApiController.Instance
                .GetKnows(Global.Instance.CurrentSpecialtyID.ToString(), courseRule.CourseNo, out _);

            InitDbPaper_HasFinish(courseRule, ques, knows, null);

            int danxuanHashCount = 0;
            int duoxuanHashCount = 0;
            int panduanHashCount = 0;

            if (dgvQuestions.Rows != null)
            {
                foreach (DataGridViewRow row in dgvQuestions.Rows)
                {
                    if (row.Cells[2].Tag.ToString() == ((int)QuestionsType.Danxuan).ToString())
                    {
                        danxuanHashCount++;
                    }
                    if (row.Cells[2].Tag.ToString() == ((int)QuestionsType.Duoxuan).ToString())
                    {
                        duoxuanHashCount++;
                    }
                    if (row.Cells[2].Tag.ToString() == ((int)QuestionsType.Panduan).ToString())
                    {
                        panduanHashCount++;
                    }
                }
            }

            for (int i = 0; i < courseRule.DanxuanCount - danxuanHashCount; i++)
            {
                int index = dgvQuestions.Rows.Add();
                dgvQuestions.Rows[index].Cells[0].Tag = courseRule;
                dgvQuestions.Rows[index].Cells[0].Value = courseRule.CourseName;
                dgvQuestions.Rows[index].Cells[1].Value = "";
                dgvQuestions.Rows[index].Cells[1].Tag = null;
                dgvQuestions.Rows[index].Cells[2].Value = "单选题";
                dgvQuestions.Rows[index].Cells[2].Tag = (int)QuestionsType.Danxuan;
                dgvQuestions.Rows[index].Cells[3].Value = "";
                dgvQuestions.Rows[index].Cells[4].Value = "出题";
            }
            for (int i = 0; i < courseRule.DuoxuanCount - duoxuanHashCount; i++)
            {
                int index = dgvQuestions.Rows.Add();
                dgvQuestions.Rows[index].Cells[0].Tag = courseRule;
                dgvQuestions.Rows[index].Cells[0].Value = courseRule.CourseName;
                dgvQuestions.Rows[index].Cells[1].Value = "";
                dgvQuestions.Rows[index].Cells[1].Tag = null;
                dgvQuestions.Rows[index].Cells[2].Value = "多选题";
                dgvQuestions.Rows[index].Cells[2].Tag = (int)QuestionsType.Danxuan;
                dgvQuestions.Rows[index].Cells[3].Value = "";
                dgvQuestions.Rows[index].Cells[4].Value = "出题";
            }
            for (int i = 0; i < courseRule.PanduanCount - panduanHashCount; i++)
            {
                int index = dgvQuestions.Rows.Add();
                dgvQuestions.Rows[index].Cells[0].Tag = courseRule;
                dgvQuestions.Rows[index].Cells[0].Value = courseRule.CourseName;
                dgvQuestions.Rows[index].Cells[1].Value = "";
                dgvQuestions.Rows[index].Cells[1].Tag = null;
                dgvQuestions.Rows[index].Cells[2].Value = "判断题";
                dgvQuestions.Rows[index].Cells[2].Tag = (int)QuestionsType.Danxuan;
                dgvQuestions.Rows[index].Cells[3].Value = "";
                dgvQuestions.Rows[index].Cells[4].Value = "出题";
            }

        }
        /// <summary>
        /// 加载知识点下的试题
        /// </summary>
        private void InitDgPaper_Know(CourseRule courseRule, string knowNo)
        {
            var ques = GetQuestionModel(courseRule.CourseNo, knowNo);

            dgvQuestions.Rows.Clear();

            var knows = WebApiController.Instance
                .GetKnows(Global.Instance.CurrentSpecialtyID.ToString(), courseRule.CourseNo, out _);

            InitDbPaper_HasFinish(courseRule, ques, knows, knowNo);

        }

        private void InitDbPaper_HasFinish(CourseRule courseRule, Dictionary<string, List<QuestionsInfoModel>> ques, List<KVModel> knows, string knowNo)
        {
            foreach (var que in ques)
            {
                foreach (var item in que.Value)
                {
                    int index = dgvQuestions.Rows.Add();
                    dgvQuestions.Rows[index].Cells[0].Tag = courseRule;
                    dgvQuestions.Rows[index].Cells[0].Value = courseRule.CourseName;
                    dgvQuestions.Rows[index].Cells[1].Value = knows.Find(k => k.Key == que.Key).Value;
                    dgvQuestions.Rows[index].Cells[1].Tag = que.Key;
                    dgvQuestions.Rows[index].Cells[2].Value = Global.Instance.QueTypeConvertToString(item.QueType);
                    dgvQuestions.Rows[index].Cells[2].Tag = item.QueType;
                    dgvQuestions.Rows[index].Cells[3].Value = item.QueContent;
                    dgvQuestions.Rows[index].Cells[4].Value = "修改";
                    dgvQuestions.Rows[index].Tag = item;
                }
            }

            if (courseRule.KnowRules != null)
            {
                foreach (var item in courseRule.KnowRules)
                {
                    if (knowNo != null)
                    {
                        if (item.KnowNo != knowNo)
                            continue;
                    }

                    int danxuanHasCount = 0;
                    int duoxuanHasCount = 0;
                    int panduanHasCount = 0;

                    if (ques.ContainsKey(item.KnowNo))
                    {
                        danxuanHasCount = ques[item.KnowNo].Count(q => q.QueType == (int)QuestionsType.Danxuan);
                        duoxuanHasCount = ques[item.KnowNo].Count(q => q.QueType == (int)QuestionsType.Danxuan);
                        panduanHasCount = ques[item.KnowNo].Count(q => q.QueType == (int)QuestionsType.Danxuan);
                    }

                    for (int i = 0; i < item.DanxuanCount - danxuanHasCount; i++)
                    {
                        int index = dgvQuestions.Rows.Add();
                        dgvQuestions.Rows[index].Cells[0].Tag = courseRule;
                        dgvQuestions.Rows[index].Cells[0].Value = courseRule.CourseName;
                        dgvQuestions.Rows[index].Cells[1].Value = item.KnowName;
                        dgvQuestions.Rows[index].Cells[1].Tag = item.KnowNo;
                        dgvQuestions.Rows[index].Cells[2].Value = "单选题";
                        dgvQuestions.Rows[index].Cells[2].Tag = (int)QuestionsType.Danxuan;
                        dgvQuestions.Rows[index].Cells[3].Value = "";
                        dgvQuestions.Rows[index].Cells[4].Value = "出题";
                    }
                    for (int i = 0; i < item.DuoxuanCount - duoxuanHasCount; i++)
                    {
                        int index = dgvQuestions.Rows.Add();
                        dgvQuestions.Rows[index].Cells[0].Tag = courseRule;
                        dgvQuestions.Rows[index].Cells[0].Value = courseRule.CourseName;
                        dgvQuestions.Rows[index].Cells[1].Value = item.KnowName;
                        dgvQuestions.Rows[index].Cells[1].Tag = item.KnowNo;
                        dgvQuestions.Rows[index].Cells[2].Value = "多选题";
                        dgvQuestions.Rows[index].Cells[2].Tag = (int)QuestionsType.Duoxuan;
                        dgvQuestions.Rows[index].Cells[3].Value = "";
                        dgvQuestions.Rows[index].Cells[4].Value = "出题";
                    }
                    for (int i = 0; i < item.PanduanCount - panduanHasCount; i++)
                    {
                        int index = dgvQuestions.Rows.Add();
                        dgvQuestions.Rows[index].Cells[0].Tag = courseRule;
                        dgvQuestions.Rows[index].Cells[0].Value = courseRule.CourseName;
                        dgvQuestions.Rows[index].Cells[1].Value = item.KnowName;
                        dgvQuestions.Rows[index].Cells[1].Tag = item.KnowNo;
                        dgvQuestions.Rows[index].Cells[2].Value = "判断题";
                        dgvQuestions.Rows[index].Cells[2].Tag = (int)QuestionsType.Panduan;
                        dgvQuestions.Rows[index].Cells[3].Value = "";
                        dgvQuestions.Rows[index].Cells[4].Value = "出题";
                    }
                }
            }
        }


        /// <summary>
        /// 从缓存获取题目
        /// </summary>
        /// <param name="courseNo"></param>
        /// <param name="knowNo"></param>
        /// <returns></returns>
        private Dictionary<string, List<QuestionsInfoModel>> GetQuestionModel(string courseNo, string knowNo)
        {
            var putCourse = _putQuestion.Courses.Find(c => c.CourseNo == courseNo);
            if (putCourse == null)
                return new Dictionary<string, List<QuestionsInfoModel>>();

            Dictionary<string, List<QuestionsInfoModel>> que = new Dictionary<string, List<QuestionsInfoModel>>();
            if (knowNo == null)
            {
                foreach (var item in putCourse.Knows.OrderBy(k => k.KnowNo))
                {
                    que.Add(item.KnowNo, item.Questions.OrderBy(q => q.QueType).ToList());
                }
                return que;
            }
            else
            {
                var putKnow = putCourse.Knows.Find(k => k.KnowNo == knowNo);
                if (putKnow == null || putKnow.Questions == null)
                    return new Dictionary<string, List<QuestionsInfoModel>>();

                que.Add(putKnow.KnowNo, putKnow.Questions.OrderBy(q => q.QueType).ToList());

                return que;
            }
        }


        /// <summary>
        /// 获取出题数量
        /// </summary>
        /// <param name="courseNo"></param>
        /// <param name="queType"></param>
        /// <returns></returns>
        private int GetQuestionCount(string courseNo, QuestionsType queType)
        {
            if (courseNo == null)
            {
                return _putQuestion.Courses
                    .Sum(c => c.Knows.Sum(k => k.Questions.Count(q => q.QueType == (int)queType)));
            }

            return _putQuestion.Courses
                .Where(c => c.CourseNo == courseNo)
                .Sum(c => c.Knows.Sum(k => k.Questions.Count(q => q.QueType == (int)queType)));
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


                var courseRule = dgvQuestions.Rows[e.RowIndex].Cells[0].Tag as CourseRule;
                int queType = (dgvQuestions.Rows[e.RowIndex].Cells[2].Tag as int?) ?? 0;
                string knowNo = dgvQuestions.Rows[e.RowIndex].Cells[1].Tag as string;

                FrmCreateQuestion frmCreateQuestion;
                if (dgvQuestions.Rows[e.RowIndex].Tag is QuestionsInfoModel que)//编辑题目
                {

                    frmCreateQuestion = new FrmCreateQuestion(que, courseRule, queType, knowNo, _p_guid, _editMode);
                    if (frmCreateQuestion.ShowDialog() == DialogResult.OK)
                    {
                        LoadRuleTree();

                        if (_currSelectEdNode != null)
                        {
                            if (_currSelectEdNode.Tag is CourseRule)
                            {
                                var nodes = ruleTree.Nodes.Find(courseRule.CourseNo, true);
                                if (nodes != null && nodes.Length > 0)
                                {
                                    ruleTree.SelectedNode = nodes[0];
                                }
                            }
                            else if (_currSelectEdNode.Tag is string tempKnowNo)
                            {
                                var nodes = ruleTree.Nodes.Find(courseRule.CourseNo + "_" + tempKnowNo, true);
                                if (nodes != null && nodes.Length > 0)
                                {
                                    ruleTree.SelectedNode = nodes[0];
                                }
                            }
                        }
                    }
                    //frmCreateQuestion = new FrmCreateQuestion(courseRule, queType, knowRule, _p_guid, _editMode);
                    //if (frmCreateQuestion.ShowDialog() == DialogResult.OK)
                    //{
                    //    LoadRuleTree();
                    //    InitDgPaper(courseRule, knowRule);
                    //}
                }
                else//增加题目
                {
                    frmCreateQuestion = new FrmCreateQuestion(courseRule, queType, knowNo, _p_guid, _editMode);
                    if (frmCreateQuestion.ShowDialog() == DialogResult.OK)
                    {
                        SetQuestionModel(courseRule.CourseNo, frmCreateQuestion._knowNo, frmCreateQuestion._question);
                        LoadRuleTree();

                        if (_currSelectEdNode != null)
                        {
                            if (_currSelectEdNode.Tag is CourseRule)
                            {
                                var nodes = ruleTree.Nodes.Find(courseRule.CourseNo, true);
                                if (nodes != null && nodes.Length > 0)
                                {
                                    ruleTree.SelectedNode = nodes[0];
                                }
                            }
                            else if (_currSelectEdNode.Tag is string tempKnowNo)
                            {
                                var nodes = ruleTree.Nodes.Find(courseRule.CourseNo + "_" + tempKnowNo, true);
                                if (nodes != null && nodes.Length > 0)
                                {
                                    ruleTree.SelectedNode = nodes[0];
                                }
                            }
                        }


                    }

                    //frmCreateQuestion = new FrmCreateQuestion(courseRule, knowRule, _p_guid, _editMode);
                    //if (frmCreateQuestion.ShowDialog() == DialogResult.OK)
                    //{
                    //    SetQuestionModel(courseRule.No, knowRule.No, frmCreateQuestion._question);
                    //    LoadRuleTree();
                    //    InitDgPaper(courseRule, knowRule);
                    //}

                }

            }
        }

        /// <summary>
        /// 检查题目是否出完
        /// </summary>
        /// <returns></returns>
        private string CheckQuestion()
        {
            foreach (var course in _rule.CourseRules.OrderBy(c => c.CourseNo))
            {
                var ques = GetQuestionModel(course.CourseNo, null);

                if (course.DanxuanCount + course.DuoxuanCount + course.PanduanCount != ques.Sum(q => q.Value.Count))
                {
                    return course.CourseNo;
                }
            }
            return null;
        }



        private void btnQuestion_Click(object sender, EventArgs e)
        {

            var courseNo = CheckQuestion();

            if (courseNo != null)
            {
                var treeNodes = ruleTree.Nodes.Find(courseNo, false);
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
                    MessageBox.Show("模拟试卷创建失败！" + message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }



        }


    }
}
