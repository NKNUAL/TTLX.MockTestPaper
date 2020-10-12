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
    public partial class FrmSetKnowPoint : Form
    {

        QuestionRule _rule;
        string _courseNo;
        int _queType;
        int _queCount;

        public FrmSetKnowPoint()
        {
            InitializeComponent();
        }


        public FrmSetKnowPoint(QuestionRule rule, string courseNo, int queType, int queCount) : this()
        {
            _rule = rule;
            _courseNo = courseNo;
            _queType = queType;
            _queCount = queCount;
        }

        private void FrmSetKnowPoint_Load(object sender, EventArgs e)
        {
            lblQueType.Text = Global.Instance.QueTypeConvertToString(_queType);
            lblCourseName.Text = _rule.CourseRules.Find(c => c.CourseNo == _courseNo).CourseName;
            lblTotal.Text = _queCount.ToString();
            lblSetted.Text = "0";
            lblNotSet.Text = "0";

            var knows = WebApiController.Instance
                .GetKnows(Global.Instance.CurrentSpecialtyID.ToString(), _courseNo, out string message);

            if (knows == null)
            {
                MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            listBox_pre.Items.AddRange(knows.ToArray());


        }
    }
}
