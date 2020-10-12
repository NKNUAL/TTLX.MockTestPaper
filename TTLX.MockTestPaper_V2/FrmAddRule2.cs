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
    public partial class FrmAddRule2 : Form
    {
        public FrmAddRule2()
        {
            InitializeComponent();
        }

        QuestionRule _currRule;

        BaseRule _baseRule;

        public FrmAddRule2(QuestionRule rule) : this()
        {
            _currRule = rule;
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
                        QueCount = danxuanCount + duoxuanCount + panduanCount
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
                string typeName = "";
                int setCount = 0;
                if (queType == QuestionsType.Danxuan)
                {
                    typeName = "单选题";
                    setCount = _currRule.CourseRules.Sum(c => c.DanxuanCount);
                }
                else if (queType == QuestionsType.Duoxuan)
                {
                    typeName = "多选题";
                    setCount = _currRule.CourseRules.Sum(c => c.DuoxuanCount);
                }
                else if (queType == QuestionsType.Panduan)
                {
                    typeName = "判断题";
                    setCount = _currRule.CourseRules.Sum(c => c.PanduanCount);
                }
                lblQueTypeName.Text = typeName;
                lblSetCount.Text = $"已设置{typeName} {setCount} 道";

                InitCourseRuleView(queType);
            }
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

                var course = _currRule.CourseRules.Find(c => c.CourseNo == item.Key);
                if (course == null)
                {
                    if (queType == QuestionsType.Danxuan)
                        param[2] = _baseRule.CourseRules.Find(c => c.CourseNo == item.Key).DanxuanCount.ToString();
                    else if (queType == QuestionsType.Duoxuan)
                        param[2] = _baseRule.CourseRules.Find(c => c.CourseNo == item.Key).DuoxuanCount.ToString();
                    else if (queType == QuestionsType.Panduan)
                        param[2] = _baseRule.CourseRules.Find(c => c.CourseNo == item.Key).PanduanCount.ToString();
                }
                else
                {
                    if (queType == QuestionsType.Danxuan)
                        param[2] = course.DanxuanCount.ToString();
                    else if (queType == QuestionsType.Duoxuan)
                        param[2] = course.DuoxuanCount.ToString();
                    else if (queType == QuestionsType.Panduan)
                        param[2] = course.PanduanCount.ToString();

                }

                dgCourseView.Rows.Add(param);
            }
        }

        private void FrmAddRule2_Load(object sender, EventArgs e)
        {
            InitLeftPanel();
        }

        private void dgCourseView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgCourseView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex > 0)
            {
                this.dgCourseView.CurrentCell = this.dgCourseView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                this.dgCourseView.BeginEdit(true);
            }
        }
    }
}
