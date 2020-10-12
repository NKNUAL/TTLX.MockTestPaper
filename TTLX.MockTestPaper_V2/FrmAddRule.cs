using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TTLX.Common;
using TTLX.Controller;
using TTLX.Controller.RequestModel;
using TTLX.Controller.ResposeModel;

namespace TTLX.MockTestPaper_V2
{
    public partial class FrmAddRule : Form
    {
        /// <summary>
        /// 当前指定的规则
        /// </summary>
        QuestionRule _rule;
        public FrmAddRule()
        {
            InitializeComponent();

            _rule = new QuestionRule
            {
                SpecialtyId = Global.Instance.CurrentSpecialtyID.ToString(),
                //CourseRules = new List<SubRule>()
            };

            _editMode = EditMode.Create;
        }
        EditMode _editMode;
        public FrmAddRule(QuestionRule rule)
        {
            InitializeComponent();
            _rule = rule;
            _editMode = EditMode.Edit;
            tbRuleName.Text = rule.RuleName;
            tbRuleDesc.Text = rule.RuleDesc;

        }


        private void cbCourse_SelectedIndexChanged(object sender, EventArgs e)
        {

            listBox_pre.Items.Clear();
            listBox_rules.Items.Clear();
            if (cbCourse.SelectedItem is KVModel)
            {

                var kv = cbCourse.SelectedItem as KVModel;

                var knows = WebApiController.Instance
                    .GetKnows(Global.Instance.CurrentSpecialtyID.ToString(), kv.Key, out string message);

                if (knows == null)
                {
                    MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var rules = RuleHelper.GetKnowRules(_rule, kv.Key);

                knows.RemoveAll(k => rules.Select(r => r.No).Contains(k.Key));

                listBox_pre.Items.AddRange(knows.ToArray());

                //listBox_rules.Items.AddRange(rules.ToArray());

            }
        }

        private void FrmAddRule_Load(object sender, EventArgs e)
        {
            this.Text = $"添加出题规则-[{Global.Instance.CurrentSpecialtyName}]";

            lblSpecialty.Text = Global.Instance.CurrentSpecialtyName;

            if (string.IsNullOrEmpty(Global.Instance.LexueID))
            {
                MessageBox.Show("请您先登陆", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            LoadCourse();
        }


        /// <summary>
        /// 加载科目
        /// </summary>
        private void LoadCourse()
        {
            var course = WebApiController.Instance
                .GetCourse(Global.Instance.CurrentSpecialtyID.ToString(), out string message);

            if (course == null)
            {
                MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cbCourse.Items.AddRange(course.ToArray());

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbCourse.SelectedItem is KVModel)
            {
                if (listBox_pre.SelectedItem is KVModel)
                {
                    var course = cbCourse.SelectedItem as KVModel;
                    var know = listBox_pre.SelectedItem as KVModel;

                    if (tbCount.Value == 0)
                    {
                        MessageBox.Show("请规定当前知识点下的题目数量！");
                        return;
                    }

                    int queCount = decimal.ToInt32(tbCount.Value);

                    RuleHelper.AddRule(_rule, course.Key, course.Value, know.Key, know.Value, queCount);

                    listBox_pre.Items.Remove(know);

                    //listBox_rules.Items.Add(new SubRule { No = know.Key, Name = know.Value, QueCount = queCount });

                }
                else
                {
                    MessageBox.Show("请先选择知识点！");
                    return;
                }
            }
            else
            {
                MessageBox.Show("请先选择科目！");
                return;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            //if (listBox_rules.SelectedItem is SubRule)
            //{
            //    if (cbCourse.SelectedItem is KVModel)
            //    {
            //        var course = cbCourse.SelectedItem as KVModel;
            //        var subRule = listBox_rules.SelectedItem as SubRule;

            //        RuleHelper.DelRule(_rule, course.Key, subRule.No);

            //        listBox_rules.Items.Remove(subRule);

            //        listBox_pre.Items.Add(new KVModel { Key = subRule.No, Value = subRule.Name });
            //    }
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_rule.CourseRules == null || _rule.CourseRules.Count == 0
                || _rule.CourseRules.Sum(c => c.QueCount) == 0)
            {
                MessageBox.Show("请还未定义任何规则，请先定义规则在保存！");
                return;
            }


            if (string.IsNullOrEmpty(tbRuleName.Text))
            {
                MessageBox.Show("请输入规则名称！");
                return;
            }


            if (_editMode == EditMode.Create)
            {
                _rule.RuleName = tbRuleName.Text;
                _rule.RuleDesc = tbRuleDesc.Text;
                if (WebApiController.Instance.AddRule(Global.Instance.LexueID, _rule, out string message))
                {
                    MessageBox.Show("添加规则成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CacheController
                        .Instance(Global.Instance.CurrentSpecialtyID.ToString()).SetRules(null);

                    WebApiController.Instance.GetAllRules(Global.Instance.LexueID, out _);

                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("添加规则失败：" + message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                string sourceRuleName = _rule.RuleName;
                string sourceRuleDesc = _rule.RuleDesc;
                _rule.RuleName = tbRuleName.Text;
                _rule.RuleDesc = tbRuleDesc.Text;
                if (WebApiController.Instance.EditRule(Global.Instance.LexueID, _rule, out bool seccess, out string message))
                {
                    if (seccess)
                    {
                        MessageBox.Show("修改规则成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                        this.Close();
                        return;
                    }
                    else
                    {
                        _rule.RuleName = sourceRuleName;
                        _rule.RuleDesc = sourceRuleDesc;
                        MessageBox.Show("修改规则失败：" + message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    _rule.RuleName = sourceRuleName;
                    _rule.RuleDesc = sourceRuleDesc;
                    MessageBox.Show("修改规则失败：" + message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            FrmRulePreview frmRulePreview = new FrmRulePreview(Global.Instance.CurrentSpecialtyName, tbRuleName.Text, tbRuleDesc.Text, _rule);

            frmRulePreview.ShowDialog();
        }

        //private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        //{
        //    e.Graphics.FillRectangle(new SolidBrush(e.BackColor), e.Bounds);
        //    if (e.Index >= 0)
        //    {
        //        StringFormat sStringFormat = new StringFormat();
        //        sStringFormat.LineAlignment = StringAlignment.Center;
        //        KVModel kv = (((ListBox)sender).Items[e.Index]) as KVModel;
        //        e.Graphics.DrawString(kv.Value, e.Font, new SolidBrush(e.ForeColor), e.Bounds, sStringFormat);
        //    }
        //    e.DrawFocusRectangle();
        //}

        //private void listBox_MeasureItem(object sender, MeasureItemEventArgs e)
        //{
        //    e.ItemHeight += 4;
        //}
    }
}
