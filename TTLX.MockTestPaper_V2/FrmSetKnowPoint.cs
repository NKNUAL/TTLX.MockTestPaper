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
using TTLX.Controller.ResposeModel;

namespace TTLX.MockTestPaper_V2
{
    public partial class FrmSetKnowPoint : Form
    {
        RuleSetHelper _setHelper;
        string _courseNo;
        string _courseName;
        private FrmSetKnowPoint()
        {
            InitializeComponent();
        }

        int _totalCount = 0;
        int _settedCount = 0;
        int _notSetCount = 0;
        public FrmSetKnowPoint(RuleSetHelper setHelper, string courseNo, string courseName) : this()
        {
            _setHelper = setHelper;
            _courseNo = courseNo;
            _courseName = courseName;
        }

        private void FrmSetKnowPoint_Load(object sender, EventArgs e)
        {
            lblQueType.Text = Global.Instance.QueTypeConvertToString((int)_setHelper.QueType);
            lblCourseName.Text = _courseName;

            LoadDataList();
            SetLabelCount();

        }

        private void SetLabelCount()
        {
            lblTotal.Text = _totalCount.ToString();
            lblSetted.Text = _settedCount.ToString();
            lblNotSet.Text = _notSetCount.ToString();
        }


        private void LoadDataList()
        {
            var course = _setHelper.Courses.Find(c => c.CourseNo == _courseNo);

            _totalCount = course.SetCount;

            var knows = WebApiController.Instance
                .GetKnows(Global.Instance.CurrentSpecialtyID.ToString(), _courseNo, out string message);

            if (knows == null)
            {
                MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            List<RuleSetHelper_Know> knows_pre = knows.ConvertAll(c => new RuleSetHelper_Know
            {
                KnowNo = c.Key,
                SetCount = 0,
                KnowName = c.Value
            });
            List<RuleSetHelper_Know> knows_rules = new List<RuleSetHelper_Know>();

            _settedCount = 0;
            _notSetCount = _totalCount;
            if (course.Knows != null)
            {
                foreach (var know in course.Knows)
                {
                    var temp = knows_pre.Find(k => k.KnowNo == know.KnowNo);
                    knows_pre.Remove(temp);
                    knows_rules.Add(know);
                    _settedCount += know.SetCount;
                    _notSetCount -= know.SetCount;
                }
            }
            listBox_pre.Items.Clear();
            listBox_rules.Items.Clear();
            listBox_pre.Items.AddRange(knows_pre.ToArray());
            listBox_rules.Items.AddRange(knows_rules.ToArray());
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (listBox_pre.SelectedItem is RuleSetHelper_Know knowTemp)
            {
                if (tbCount.Value == 0)
                {
                    MessageBox.Show("请规定当前知识点下的题目数量！");
                    return;
                }
                int queCount = decimal.ToInt32(tbCount.Value);
                if (queCount > _notSetCount)
                {
                    MessageBox.Show("数量设置不能超过未设置数量！");
                    return;
                }

                var course = _setHelper.Courses.Find(c => c.CourseNo == _courseNo);

                if (course.Knows == null)
                    course.Knows = new List<RuleSetHelper_Know>();

                var know = course.Knows.Find(k => k.KnowNo == knowTemp.KnowNo);
                if (!course.Knows.Exists(k => k.KnowNo == knowTemp.KnowNo))
                {
                    knowTemp.SetCount = queCount;

                    course.Knows.Add(knowTemp);
                }

                LoadDataList();
                SetLabelCount();
            }
            else
            {
                MessageBox.Show("请先选择知识点！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (listBox_rules.SelectedItem is RuleSetHelper_Know knowTemp)
            {

                var course = _setHelper.Courses.Find(c => c.CourseNo == _courseNo);

                if (course.Knows == null)
                    course.Knows = new List<RuleSetHelper_Know>();

                var know = course.Knows.Find(k => k.KnowNo == knowTemp.KnowNo);
                if (know != null)
                {
                    course.Knows.Remove(know);
                }

                LoadDataList();
                SetLabelCount();
            }
            else
            {
                MessageBox.Show("请先选择知识点！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
    }
}
