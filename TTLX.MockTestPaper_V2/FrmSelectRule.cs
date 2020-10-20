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
    public partial class FrmSelectRule : Form
    {
        private FrmSelectRule()
        {
            InitializeComponent();
        }

        public QuestionRule SelectedRule { get; set; }

        public FrmSelectRule(List<QuestionRule> rules) : this()
        {
            cbRules.ItemHeight = 30;

            cbRules.Items.AddRange(rules.ToArray());
        }


        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cbRules.SelectedItem is QuestionRule rule)
            {
                SelectedRule = rule;
                DialogResult = DialogResult.OK;
            }
        }

        private void btnNewRule_Click(object sender, EventArgs e)
        {
            FrmRuleManager frmRuleManager = new FrmRuleManager();

            frmRuleManager.ShowDialog();

            cbRules.Items.Clear();

            var rules = WebApiController.Instance.GetAllRules(Global.Instance.LexueID, out _);

            cbRules.Items.AddRange(rules.ToArray());
        }
    }
}
