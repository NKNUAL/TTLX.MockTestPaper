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
using TTLX.Common.Expand;
using TTLX.Controller;
using TTLX.Controller.Model;
using TTLX.Controller.RequestModel;
using TTLX.Controller.ResposeModel;

namespace TTLX.MockTestPaper_V2.Nurse
{
    public partial class FrmQuestion_Nurse : Form
    {
        private FrmQuestion_Nurse()
        {
            InitializeComponent();
        }


        private PutQuestionNurseModel _putQuestion;

        private string _p_guid;

        private EditMode _editMode;

        private QuestionRule_Nurse _rule;

        /// <summary>
        /// 首次出题
        /// </summary>
        /// <param name="rule"></param>
        public FrmQuestion_Nurse(QuestionRule_Nurse rule) : this()
        {
            this._rule = rule;
            this._putQuestion = new PutQuestionNurseModel
            {
                RuleNo = rule.RuleNo,
                UserId = Global.Instance.LexueID,
                UserName = Global.Instance.UserName,
                A_ = new List<PutQuestionA_Model>()
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
        public FrmQuestion_Nurse(QuestionRule_Nurse rule, PutQuestionNurseModel putQuestion, string pGuid) : this()
        {
            _rule = rule;
            _putQuestion = putQuestion;
            _p_guid = pGuid;
            _editMode = EditMode.Create;
        }

        /// <summary>
        /// 编辑已经入库的试卷
        /// </summary>
        /// <param name="paperName"></param>
        /// <param name="rule"></param>
        /// <param name="putQuestion"></param>
        //public FrmQuestion_Nurse(string paperName, QuestionRule rule, PutQuestionModel putQuestion) : this()
        //{
        //    this._rule = rule;
        //    this._putQuestion = putQuestion;
        //    this.tbPaperName.Text = paperName;
        //    _editMode = EditMode.Edit;
        //}

        private void FrmQuestion2_Load(object sender, EventArgs e)
        {
            this.Text = $"模拟试卷出题-{Global.Instance.CurrentSpecialtyName}";

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
            foreach (var item in _rule.A_)
            {
                var model = _putQuestion.A_.Find(x => x.TypeId == item.TypeId);

                int hasCount = GetHasCount((NurseRuleType)item.TypeId, item.SubQueCount);

                string text = string.Format("{0}：已出{1}道，剩余{2}道。", item.TypeName, hasCount, item.QueCount - hasCount);

                if (item.TypeId == (int)NurseRuleType.A1)
                    lblA1.Text = text;
                else if (item.TypeId == (int)NurseRuleType.A2)
                    lblA2.Text = text;
                else
                    lblA3.Text = text;
            }


        }

        /// <summary>
        /// 获取已出题数量
        /// </summary>
        /// <param name="nurseRuleType"></param>
        /// <returns></returns>
        private int GetHasCount(NurseRuleType nurseRuleType, int subQueCount)
        {
            int hasCount = 0;

            if (nurseRuleType == NurseRuleType.A3)
            {
                var models = _putQuestion.A_.FindAll(x => x.TypeId == (int)nurseRuleType);

                if (models == null)
                    return 0;

                foreach (var A3 in models)
                {
                    if (((A3.Questions?.Count) ?? 0) == subQueCount)
                        hasCount++;
                }
            }
            else
            {
                var model = _putQuestion.A_.Find(x => x.TypeId == (int)nurseRuleType);
                hasCount = (model?.Questions?.Count) ?? 0;
            }

            return hasCount;
        }

        /// <summary>
        /// 设置已出题的展示树
        /// </summary>
        private void SetHasQueTree()
        {
            Dictionary<string, Dictionary<string, int>> dicTree = new Dictionary<string, Dictionary<string, int>>();
            foreach (var item in _putQuestion.A_)
            {
                if (item.Questions != null)
                {
                    foreach (var que in item.Questions)
                    {
                        if (!dicTree.ContainsKey(que.CourseNo))
                        {
                            dicTree.Add(que.CourseNo, new Dictionary<string, int>());
                        }

                        if (!dicTree[que.CourseNo].ContainsKey(que.KnowNo))
                        {
                            dicTree[que.CourseNo].Add(que.KnowNo, 0);
                        }

                        dicTree[que.CourseNo][que.KnowNo]++;
                    }
                }
            }

            var courses = WebApiController.Instance.GetCourse(Global.Instance.CurrentSpecialtyID.ToString(), out _);

            queTree.Nodes.Clear();

            foreach (var item in dicTree)
            {
                TreeNode node = new TreeNode
                {
                    Text = courses.Find(c => c.Key == item.Key).Value + $"({item.Value.Sum(q => q.Value)})"
                };
                queTree.Nodes.Add(node);

                var knows = WebApiController.Instance.GetKnows(Global.Instance.CurrentSpecialtyID.ToString(), item.Key, out _);

                foreach (var know in item.Value)
                {
                    node.Nodes.Add(new TreeNode
                    {
                        Text = knows.Find(k => k.Key == know.Key).Value + $"({know.Value})"
                    });
                }
            }
        }

        /// <summary>
        /// 保存题目到
        /// </summary>
        private void ExeSaveRecord()
        {
            if (!string.IsNullOrEmpty(_p_guid))
                WebApiController.Instance.SaveLocalPaper(Global.Instance.LexueID, _p_guid, _rule.RuleNo);
        }

        /// <summary>
        /// 上传到服务器后，删除本地记录
        /// </summary>
        private void ExeDelRecord()
        {
            if (_editMode == EditMode.Create)
                WebApiController.Instance.DeleteLocalRecord(_p_guid);
        }

        /// <summary>
        /// 加载规则树
        /// </summary>
        private void LoadRuleTree()
        {

            SetCountLabel();
            SetHasQueTree();

            ruleTree.Nodes.Clear();

            foreach (var item in _rule.A_)
            {
                TreeNode ccNode = new TreeNode()
                {
                    Tag = item.TypeId,
                    Name = item.TypeId.ToString()
                };
                ruleTree.Nodes.Add(ccNode);

                int hasCount = GetHasCount((NurseRuleType)item.TypeId, item.SubQueCount);

                ccNode.Text = item.TypeName + $"({hasCount}/{item.QueCount})";
                if (hasCount == item.QueCount)
                    ccNode.ForeColor = Color.Blue;
                else
                    ccNode.ForeColor = Color.Red;
            }
        }

        TreeNode _currSelectEdNode = null;

        private void ruleTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _currSelectEdNode = e.Node;

            if (e.Node == null)
                return;

            if (e.Node.Tag is int typeId)
            {
                InitViewColumn((NurseRuleType)typeId);

                var models = _putQuestion.A_.FindAll(x => x.TypeId == typeId);

                var typeRule = _rule.A_.Find(x => x.TypeId == typeId);

                if (typeId == (int)NurseRuleType.A3)
                {

                    foreach (var model in models)
                    {
                        int index = dgvQuestions.Rows.Add();
                        dgvQuestions.Rows[index].Cells[0].Value = $"第{index + 1}题";
                        dgvQuestions.Rows[index].Cells[0].Tag = typeId;
                        dgvQuestions.Rows[index].Cells[1].Value = model.GeneralName;
                        dgvQuestions.Rows[index].Cells[2].Value = "修改";
                        dgvQuestions.Rows[index].Cells[2].Tag = model;
                    }

                    for (int i = 0; i < typeRule.QueCount - models.Count; i++)
                    {
                        int index = dgvQuestions.Rows.Add();
                        dgvQuestions.Rows[index].Cells[0].Value = $"第{index + 1}题";
                        dgvQuestions.Rows[index].Cells[0].Tag = typeId;
                        dgvQuestions.Rows[index].Cells[1].Value = "";
                        dgvQuestions.Rows[index].Cells[2].Value = "出题";
                    }
                }
                else
                {
                    var courses = WebApiController.Instance.GetCourse(Global.Instance.CurrentSpecialtyID.ToString(), out _);

                    foreach (var model in models)
                    {
                        if (model.Questions != null)
                        {
                            foreach (var que in model.Questions)
                            {
                                var knows = WebApiController.Instance.GetKnows(Global.Instance.CurrentSpecialtyID.ToString(), que.CourseNo, out _);
                                int index = dgvQuestions.Rows.Add();
                                dgvQuestions.Rows[index].Cells[0].Value = $"第{index + 1}题";
                                dgvQuestions.Rows[index].Cells[0].Tag = typeId;
                                dgvQuestions.Rows[index].Cells[1].Value = courses.Find(c => c.Key == que.CourseNo).Value;
                                dgvQuestions.Rows[index].Cells[2].Value = knows.Find(k => k.Key == que.KnowNo).Value;
                                dgvQuestions.Rows[index].Cells[3].Value = "单选题";
                                dgvQuestions.Rows[index].Cells[3].Tag = (int)QuestionsType.Danxuan;
                                dgvQuestions.Rows[index].Cells[4].Value = que.QueContent;
                                dgvQuestions.Rows[index].Cells[5].Value = "修改";
                                dgvQuestions.Rows[index].Cells[5].Tag = que;
                            }
                        }
                    }

                    for (int i = 0; i < typeRule.QueCount - models.Sum(m => m.Questions.Count); i++)
                    {
                        int index = dgvQuestions.Rows.Add();
                        dgvQuestions.Rows[index].Cells[0].Value = $"第{index + 1}题";
                        dgvQuestions.Rows[index].Cells[0].Tag = typeId;
                        dgvQuestions.Rows[index].Cells[1].Value = "";
                        dgvQuestions.Rows[index].Cells[2].Value = "";
                        dgvQuestions.Rows[index].Cells[3].Value = "单选题";
                        dgvQuestions.Rows[index].Cells[3].Tag = (int)QuestionsType.Danxuan;
                        dgvQuestions.Rows[index].Cells[4].Value = "";
                        dgvQuestions.Rows[index].Cells[5].Value = "出题";
                    }

                }
            }
        }

        private void InitViewColumn(NurseRuleType nurseRuleType)
        {
            dgvQuestions.Columns.Clear();

            if (nurseRuleType != NurseRuleType.A3)
            {
                dgvQuestions.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "序号",
                    FillWeight = 10
                });
                dgvQuestions.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "科目",
                    FillWeight = 15
                });
                dgvQuestions.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "知识点",
                    FillWeight = 20
                });
                dgvQuestions.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "题型",
                    FillWeight = 15
                });
                dgvQuestions.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "试题内容",
                    FillWeight = 40
                });
                dgvQuestions.Columns.Add(new DataGridViewLinkColumn
                {
                    HeaderText = "",
                    FillWeight = 10
                });
            }
            else
            {
                dgvQuestions.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "序号",
                    FillWeight = 10
                });
                dgvQuestions.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "总题干",
                    FillWeight = 80
                });
                dgvQuestions.Columns.Add(new DataGridViewLinkColumn
                {
                    HeaderText = "",
                    FillWeight = 10
                });
            }
            dgvQuestions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void dgvQuestions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvQuestions.Columns.Count - 1)
            {
                int typeId = (dgvQuestions.Rows[e.RowIndex].Cells[0].Tag as int?) ?? 0;

                if (typeId == (int)NurseRuleType.A1 || typeId == (int)NurseRuleType.A2)
                {
                    if (dgvQuestions.Rows[e.RowIndex].Cells[5].Tag is QuestionsInfoModel2 que)//修改
                    {
                        FrmCreateQuestion_Nurse frmCreateQuestion_Nurse = new FrmCreateQuestion_Nurse(this, que, typeId, _p_guid, _editMode);
                        if (frmCreateQuestion_Nurse.ShowDialog() == DialogResult.OK)
                        {
                            LoadRuleTree();
                            var nodes = ruleTree.Nodes.Find(typeId.ToString(), false);
                            if (nodes != null && nodes.Length > 0)
                            {
                                ruleTree.SelectedNode = nodes[0];
                            }
                        }
                    }
                    else//出题
                    {
                        FrmCreateQuestion_Nurse frmCreateQuestion_Nurse = new FrmCreateQuestion_Nurse(this, typeId, _p_guid, _editMode);
                        if (frmCreateQuestion_Nurse.ShowDialog() == DialogResult.OK)
                        {
                            SetQuestionModel(typeId, frmCreateQuestion_Nurse._question);

                            LoadRuleTree();
                            var nodes = ruleTree.Nodes.Find(typeId.ToString(), false);
                            if (nodes != null && nodes.Length > 0)
                            {
                                ruleTree.SelectedNode = nodes[0];
                            }
                        }
                    }
                }

                if (typeId == (int)NurseRuleType.A3)
                {
                    if (dgvQuestions.Rows[e.RowIndex].Cells[2].Tag is PutQuestionA_Model putModel)//修改
                    {

                    }
                    else//出题
                    {

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
        //private Dictionary<string, List<QuestionsInfoModel>> GetQuestionModel(string courseNo, string knowNo)
        //{
        //    var putCourse = _putQuestion.Courses.Find(c => c.CourseNo == courseNo);
        //    if (putCourse == null)
        //        return new Dictionary<string, List<QuestionsInfoModel>>();

        //    Dictionary<string, List<QuestionsInfoModel>> que = new Dictionary<string, List<QuestionsInfoModel>>();
        //    if (knowNo == null)
        //    {
        //        foreach (var item in putCourse.Knows.OrderBy(k => k.KnowNo))
        //        {
        //            que.Add(item.KnowNo, item.Questions.OrderBy(q => q.QueType).ToList());
        //        }
        //        return que;
        //    }
        //    else
        //    {
        //        var putKnow = putCourse.Knows.Find(k => k.KnowNo == knowNo);
        //        if (putKnow == null || putKnow.Questions == null)
        //            return new Dictionary<string, List<QuestionsInfoModel>>();

        //        que.Add(putKnow.KnowNo, putKnow.Questions.OrderBy(q => q.QueType).ToList());

        //        return que;
        //    }
        //}

        /// <summary>
        /// 获取出题数量
        /// </summary>
        /// <param name="courseNo"></param>
        /// <param name="queType"></param>
        /// <returns></returns>
        //private int GetQuestionCount(string courseNo, QuestionsType queType)
        //{
        //    if (courseNo == null)
        //    {
        //        return _putQuestion.Courses
        //            .Sum(c => c.Knows.Sum(k => k.Questions.Count(q => q.QueType == (int)queType)));
        //    }

        //    return _putQuestion.Courses
        //        .Where(c => c.CourseNo == courseNo)
        //        .Sum(c => c.Knows.Sum(k => k.Questions.Count(q => q.QueType == (int)queType)));
        //}

        /// <summary>
        /// 保存题目到缓存--A1 A2题型
        /// </summary>
        /// <param name="courseNo"></param>
        /// <param name="knowNo"></param>
        /// <param name="que"></param>
        public void SetQuestionModel(int typeId, QuestionsInfoModel2 que)
        {
            var model = _putQuestion.A_.Find(c => c.TypeId == typeId);

            var rule = _rule.A_.Find(c => c.TypeId == typeId);

            if (model == null)
            {
                model = new PutQuestionA_Model
                {
                    TypeId = typeId,
                    Questions = new List<QuestionsInfoModel2>(),
                    TypeName = rule.TypeName
                };
                _putQuestion.A_.Add(model);
            }

            model.Questions.Add(que);
        }

        /// <summary>
        /// 保存题目道缓存--A3题型
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="que"></param>
        public void SetQuestionModel(int typeId, PutQuestionA_Model que)
        {
            var model = _putQuestion.A_.Find(c => c.GeneralNo == que.GeneralNo);

            if (model == null)
            {
                _putQuestion.A_.Add(model);
            }
        }


        /// <summary>
        /// 检查题目是否出完
        /// </summary>
        /// <returns></returns>
        //private string CheckQuestion()
        //{
        //    foreach (var course in _rule.CourseRules.OrderBy(c => c.CourseNo))
        //    {
        //        var ques = GetQuestionModel(course.CourseNo, null);

        //        if (course.DanxuanCount + course.DuoxuanCount + course.PanduanCount != ques.Sum(q => q.Value.Count))
        //        {
        //            return course.CourseNo;
        //        }
        //    }
        //    return null;
        //}

        //private void btnQuestion_Click(object sender, EventArgs e)
        //{
        //    var courseNo = CheckQuestion();

        //    if (courseNo != null)
        //    {
        //        var treeNodes = ruleTree.Nodes.Find(courseNo, false);
        //        if (treeNodes != null && treeNodes.Length > 0)
        //        {
        //            ruleTree.SelectedNode = treeNodes[0];
        //            MessageBox.Show("您还有题目未出完，请先出完题目再提交模拟试卷！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(tbPaperName.Text))
        //        {
        //            MessageBox.Show("请您输入模拟试卷名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            tbPaperName.Focus();
        //            return;
        //        }

        //        _putQuestion.PaperName = tbPaperName.Text;

        //        if (WebApiController.Instance.CreatePaper(_putQuestion, out string message))
        //        {
        //            MessageBox.Show("模拟试卷创建成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //            Task.Factory.StartNew(ExeDelRecord);

        //            DialogResult = DialogResult.OK;

        //            this.Close();
        //            return;
        //        }
        //        else
        //        {
        //            MessageBox.Show("模拟试卷创建失败！" + message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }
        //    }



        //}

        /// <summary>
        /// 检查本地题目相似度
        /// </summary>
        /// <param name="queContent"></param>
        /// <returns></returns>
        public bool CheckSimilarity(string queNo, string queContent, out string content)
        {
            content = string.Empty;

            if (_putQuestion == null || _putQuestion.A_ == null || _putQuestion.A_.Count == 0)
                return false;

            List<QuestionsInfoModel2> totalQUes = new List<QuestionsInfoModel2>();

            _putQuestion.A_.AsParallel().ForAll(c =>
            {
                if (c.Questions != null)
                {
                    totalQUes.AddRange(c.Questions);
                }
            });

            if (!string.IsNullOrEmpty(queNo))
            {
                totalQUes.RemoveAll(q => q.No == queNo);
            }

            bool isSimilarity = false;
            string tempContent = string.Empty;
            Parallel.ForEach(totalQUes, (q, loopState) =>
            {
                if (!string.IsNullOrWhiteSpace(q.QueContent) && LevenshteinDistanceHelper.CompareStrings(q.QueContent, queContent) >= 0.9)
                {
                    isSimilarity = true;
                    tempContent = q.QueContent;
                    loopState.Stop();
                }
            });
            content = tempContent;
            return isSimilarity;

        }

    }
}
