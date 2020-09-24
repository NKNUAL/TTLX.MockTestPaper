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
using TTLX.Controller.ResposeModel;

namespace TTLX.MockTestPaper_V2
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            UserLogin();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        private void UserLogin()
        {
            string userid = this.txtUserID.Text.Trim();
            string pwd = this.txtPwd.Text.Trim();
            if (string.IsNullOrEmpty(userid) || string.IsNullOrEmpty(pwd))
            {
                MessageBox.Show("账号或密码不能为空!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            UserInfoModel user = WebApiController.Instance.UserLogin(userid, pwd, out string message);
            if (user != null)
            {
                Global.Instance.LexueID = user.UserId;
                Global.Instance.UserName = user.UserName;
                Global.Instance.CurrentSpecialtyID = int.Parse(user.SpecialtyId);
                Global.Instance.CurrentSpecialtyName = user.SpecialtyName;
                Global.Instance.DanxuanScore = user.DanxuanScore;
                Global.Instance.DuoxuanScore = user.DuoxuanScore;
                Global.Instance.PanduanScore = user.PanduanScore;
                DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtUserID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.txtPwd.Focus();
        }

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                UserLogin();
        }
    }
}
