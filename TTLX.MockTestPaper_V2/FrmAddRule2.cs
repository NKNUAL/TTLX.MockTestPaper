using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTLX.Common;
using TTLX.Common.Expand;
using TTLX.Controller;
using TTLX.Controller.Model;
using TTLX.Controller.ResposeModel;

namespace TTLX.MockTestPaper_V2
{
    public partial class FrmAddRule2 : Form
    {


        QuestionRule _currRule;

        BaseRule _baseRule;

        RuleSetHelper _setHelper { get; set; }

        public FrmAddRule2()
        {
            InitializeComponent();
        }

        public FrmAddRule2(QuestionRule rule) : this()
        {
            _currRule = rule;
        }

        private void HideRightPanel()
        {
            splitContainer1.Panel2.Hide();
        }

        private void ShowRightPanel()
        {
            splitContainer1.Panel2.Show();
        }

        private void InitLeftPanel()
        {
            _baseRule = WebApiController.Instance
                .GetBaseRule(Global.Instance.CurrentSpecialtyID.ToString(), out string message);

            if (_baseRule == null)
            {
                MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            var courses = WebApiController.Instance
                .GetCourse(Global.Instance.CurrentSpecialtyID.ToString(), out message);

            if (courses == null)
            {
                MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (_currRule == null)
            {
                _currRule = new QuestionRule
                {
                    SpecialtyId = Global.Instance.CurrentSpecialtyID.ToString(),
                    CourseRules = new List<CourseRule>()
                };
                foreach (var item in courses)
                {
                    var temp_course = _baseRule.CourseRules.Find(c => c.CourseNo == item.Key);

                    int danxuanCount = (temp_course?.DanxuanCount) ?? 0;
                    int duoxuanCount = (temp_course?.DuoxuanCount) ?? 0;
                    int panduanCount = (temp_course?.PanduanCount) ?? 0;

                    _currRule.CourseRules.Add(new CourseRule
                    {
                        CourseNo = item.Key,
                        CourseName = item.Value,
                        DanxuanCount = danxuanCount,
                        DuoxuanCount = duoxuanCount,
                        PanduanCount = panduanCount,
                    });
                }
            }

            lblSpecialtyName.Text = Global.Instance.CurrentSpecialtyName;


            lbl_danxuan.Text = _baseRule.DanxuanCount + " 道";
            lbl_duoxuan.Text = _baseRule.DuoxuanCount + " 道";
            lbl_panduan.Text = _baseRule.PanduanCount + " 道";

            btnSetDanxuan.Tag = QuestionsType.Danxuan;
            btnSetDuoxuan.Tag = QuestionsType.Duoxuan;
            btnSetPanduan.Tag = QuestionsType.Panduan;

            if (_baseRule.DanxuanCount == 0)
                btnSetDanxuan.Enabled = false;
            if (_baseRule.DuoxuanCount == 0)
                btnSetDuoxuan.Enabled = false;
            if (_baseRule.PanduanCount == 0)
                btnSetPanduan.Enabled = false;


        }

        private void btnSetRule_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn.Tag is QuestionsType queType)
            {
                ShowRightPanel();

                if (_setHelper == null || _setHelper.QueType != queType)
                {
                    _setHelper = new RuleSetHelper
                    {
                        QueType = queType,
                        Courses = _currRule.CourseRules.ConvertAll(c =>
                        {
                            var temp = new RuleSetHelper_Course
                            {
                                CourseNo = c.CourseNo,
                            };
                            if (queType == QuestionsType.Danxuan)
                                temp.SetCount = c.DanxuanCount;
                            if (queType == QuestionsType.Duoxuan)
                                temp.SetCount = c.DuoxuanCount;
                            if (queType == QuestionsType.Panduan)
                                temp.SetCount = c.PanduanCount;

                            if (c.KnowRules != null)
                            {
                                temp.Knows = c.KnowRules.ConvertAll(k =>
                                {
                                    var tempKnow = new RuleSetHelper_Know
                                    {
                                        KnowNo = k.KnowNo,
                                    };
                                    if (queType == QuestionsType.Danxuan)
                                        tempKnow.SetCount = k.DanxuanCount ?? 0;
                                    if (queType == QuestionsType.Duoxuan)
                                        tempKnow.SetCount = k.DuoxuanCount ?? 0;
                                    if (queType == QuestionsType.Panduan)
                                        tempKnow.SetCount = k.PanduanCount ?? 0;

                                    return tempKnow;
                                });
                            }

                            return temp;
                        })
                    };
                }

                int baseCount = 0;
                if (queType == QuestionsType.Danxuan)
                    baseCount = _baseRule.CourseRules.Sum(c => c.DanxuanCount);
                else if (queType == QuestionsType.Duoxuan)
                    baseCount = _baseRule.CourseRules.Sum(c => c.DuoxuanCount);
                else if (queType == QuestionsType.Panduan)
                    baseCount = _baseRule.CourseRules.Sum(c => c.PanduanCount);

                _setHelper.MaxQueCount = baseCount;

                string typeName = Global.Instance.QueTypeConvertToString((int)queType);
                int setCount = _setHelper.Courses.Sum(c => c.SetCount);

                lblQueTypeName.Text = typeName;
                SetCountLabel();
                tbPrompt.Text = "提示：单击下方题目数量列可以修改每个科目的题目数量，" +
                    "但是每个科目的题目数量相加必须等于" + baseCount;

                InitCourseRuleView(queType);
            }
        }


        private void SetCountLabel()
        {
            string typeName = Global.Instance.QueTypeConvertToString((int)_setHelper.QueType);
            int setCount = _setHelper.Courses.Sum(c => c.SetCount);
            lblSetCount.Text = $"已设置{typeName} {setCount} 道";
        }


        private void InitCourseRuleView(QuestionsType queType)
        {
            var courses = WebApiController.Instance
                .GetCourse(Global.Instance.CurrentSpecialtyID.ToString(), out string message);

            dgCourseView.Rows.Clear();

            foreach (var item in courses)
            {
                string[] param = new string[4];
                param[0] = item.Key;
                param[1] = item.Value;
                param[3] = "设置知识点";

                var course = _setHelper.Courses.Find(c => c.CourseNo == item.Key);
                if (course == null)
                {
                    int setCount = 0;

                    if (queType == QuestionsType.Danxuan)
                        setCount = _baseRule.CourseRules.Find(c => c.CourseNo == item.Key).DanxuanCount;
                    else if (queType == QuestionsType.Duoxuan)
                        setCount = _baseRule.CourseRules.Find(c => c.CourseNo == item.Key).DuoxuanCount;
                    else if (queType == QuestionsType.Panduan)
                        setCount = _baseRule.CourseRules.Find(c => c.CourseNo == item.Key).PanduanCount;

                    course = new RuleSetHelper_Course
                    {
                        SetCount = setCount,
                        CourseNo = item.Key
                    };
                    _setHelper.Courses.Add(course);
                }

                param[2] = course.SetCount.ToString();


                dgCourseView.Rows.Add(param);
            }
        }

        private void FrmAddRule2_Load(object sender, EventArgs e)
        {
            InitLeftPanel();
            HideRightPanel();
        }

        private void dgCourseView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                string cellValue = dgCourseView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                var course = _setHelper.Courses
                        .Find(c => c.CourseNo == dgCourseView.Rows[e.RowIndex].Cells[0].Value.ToString());

                if (!Regex.IsMatch(cellValue, "^[0-9]*$"))
                {
                    MessageBox.Show("请输入数字！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    dgCourseView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = course.SetCount;

                    return;
                }

                if (int.Parse(cellValue) > _setHelper.MaxQueCount)
                {
                    MessageBox.Show($"单个科目数不能大于{_setHelper.MaxQueCount}！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    dgCourseView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = course.SetCount;

                    return;
                }

                if (CheckSetCount() > _setHelper.MaxQueCount)
                {
                    MessageBox.Show($"总设置题目数不能大于{_setHelper.MaxQueCount}！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    dgCourseView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = course.SetCount;

                    return;
                }

                course.SetCount = int.Parse(cellValue);
                SetCountLabel();
            }
        }

        private int CheckSetCount()
        {
            int totalCount = 0;
            foreach (DataGridViewRow item in dgCourseView.Rows)
            {
                totalCount += int.Parse(item.Cells[2].Value.ToString());
            }
            return totalCount;
        }


        private void dgCourseView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                this.dgCourseView.CurrentCell = this.dgCourseView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                this.dgCourseView.BeginEdit(true);
            }
            else if (e.ColumnIndex == 3 && e.RowIndex >= 0)
            {
                string courseNo = dgCourseView.Rows[e.RowIndex].Cells[0].Value.ToString();
                string courseName = dgCourseView.Rows[e.RowIndex].Cells[1].Value.ToString();
                FrmSetKnowPoint frmSetKnowPoint = new FrmSetKnowPoint(_setHelper, courseNo, courseName);
                frmSetKnowPoint.ShowDialog();
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            FrmRulePreview frmRulePreview =
                new FrmRulePreview(Global.Instance.CurrentSpecialtyName, tbRuleName.Text, tbRuleDesc.Text, _currRule);

            frmRulePreview.ShowDialog();

        }

        private void btnSaveModify_Click(object sender, EventArgs e)
        {
            if (_setHelper.Courses.Sum(c => c.SetCount) < _setHelper.MaxQueCount)
            {
                MessageBox.Show($"当前设置题目数小于" + _setHelper.MaxQueCount + "，请继续设置科目题目数。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _setHelper.Courses.AsParallel().ForAll(c =>
            {
                var course = _currRule.CourseRules.Find(cr => cr.CourseNo == c.CourseNo);

                if (_setHelper.QueType == QuestionsType.Danxuan)
                    course.DanxuanCount = c.SetCount;
                else if (_setHelper.QueType == QuestionsType.Duoxuan)
                    course.DuoxuanCount = c.SetCount;
                else if (_setHelper.QueType == QuestionsType.Panduan)
                    course.PanduanCount = c.SetCount;

                if (c.Knows != null)
                {
                    if (course.KnowRules == null)
                        course.KnowRules = new List<KnowRule>();

                    c.Knows.AsParallel().ForAll(k =>
                    {
                        var know = course.KnowRules.Find(kr => kr.KnowNo == k.KnowNo);
                        if (know == null)
                        {
                            know = new KnowRule
                            {
                                KnowNo = k.KnowNo,
                                KnowName = k.KnowName
                            };
                            course.KnowRules.Add(know);
                        }

                        if (_setHelper.QueType == QuestionsType.Danxuan)
                            know.DanxuanCount = k.SetCount;
                        else if (_setHelper.QueType == QuestionsType.Duoxuan)
                            know.DuoxuanCount = k.SetCount;
                        else if (_setHelper.QueType == QuestionsType.Panduan)
                            know.PanduanCount = k.SetCount;
                    });

                    course.KnowRules.RemoveAll(k => !c.Knows.Select(t => t.KnowNo).Contains(k.KnowNo));

                }
            });

            MessageBox.Show($"保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(tbRuleName.Text))
            {
                MessageBox.Show("请输入规则名称！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_currRule.CourseRules.Sum(c => c.DanxuanCount) != _baseRule.DanxuanCount)
            {
                MessageBox.Show("单选题规则未设置完成！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_currRule.CourseRules.Sum(c => c.DuoxuanCount) != _baseRule.DuoxuanCount)
            {
                MessageBox.Show("多选题规则未设置完成！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_currRule.CourseRules.Sum(c => c.PanduanCount) != _baseRule.PanduanCount)
            {
                MessageBox.Show("判断题规则未设置完成！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _currRule.RuleName = tbRuleName.Text;
            _currRule.RuleDesc = tbRuleDesc.Text;

            if (WebApiController.Instance.AddRule(Global.Instance.LexueID, _currRule, out string message))
            {


            }


        }
    }
}
