namespace TTLX.MockTestPaper_V2
{
    partial class FrmRulePreview
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRulePreview));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSpecialty = new System.Windows.Forms.Label();
            this.lblRuleName = new System.Windows.Forms.Label();
            this.tbRuleDesc = new System.Windows.Forms.RichTextBox();
            this.ruleTree = new System.Windows.Forms.TreeView();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F);
            this.label1.Location = new System.Drawing.Point(22, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "专业名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F);
            this.label2.Location = new System.Drawing.Point(22, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "规则名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 11F);
            this.label3.Location = new System.Drawing.Point(22, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "规则描述：";
            // 
            // lblSpecialty
            // 
            this.lblSpecialty.AutoSize = true;
            this.lblSpecialty.Font = new System.Drawing.Font("宋体", 11F);
            this.lblSpecialty.Location = new System.Drawing.Point(108, 23);
            this.lblSpecialty.Name = "lblSpecialty";
            this.lblSpecialty.Size = new System.Drawing.Size(67, 15);
            this.lblSpecialty.TabIndex = 3;
            this.lblSpecialty.Text = "计算机类";
            // 
            // lblRuleName
            // 
            this.lblRuleName.AutoSize = true;
            this.lblRuleName.Font = new System.Drawing.Font("宋体", 11F);
            this.lblRuleName.Location = new System.Drawing.Point(108, 55);
            this.lblRuleName.Name = "lblRuleName";
            this.lblRuleName.Size = new System.Drawing.Size(67, 15);
            this.lblRuleName.TabIndex = 4;
            this.lblRuleName.Text = "测试规则";
            // 
            // tbRuleDesc
            // 
            this.tbRuleDesc.Font = new System.Drawing.Font("宋体", 11F);
            this.tbRuleDesc.Location = new System.Drawing.Point(110, 86);
            this.tbRuleDesc.Name = "tbRuleDesc";
            this.tbRuleDesc.Size = new System.Drawing.Size(678, 90);
            this.tbRuleDesc.TabIndex = 5;
            this.tbRuleDesc.Text = "";
            // 
            // ruleTree
            // 
            this.ruleTree.Font = new System.Drawing.Font("宋体", 11F);
            this.ruleTree.Location = new System.Drawing.Point(110, 188);
            this.ruleTree.Name = "ruleTree";
            this.ruleTree.Size = new System.Drawing.Size(678, 435);
            this.ruleTree.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 11F);
            this.label4.Location = new System.Drawing.Point(22, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "规则：";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("宋体", 12F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(702, 629);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(86, 31);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmRulePreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 672);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ruleTree);
            this.Controls.Add(this.tbRuleDesc);
            this.Controls.Add(this.lblRuleName);
            this.Controls.Add(this.lblSpecialty);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRulePreview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "规则预览";
            this.Load += new System.EventHandler(this.FrmRulePreview_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSpecialty;
        private System.Windows.Forms.Label lblRuleName;
        private System.Windows.Forms.RichTextBox tbRuleDesc;
        private System.Windows.Forms.TreeView ruleTree;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClose;
    }
}