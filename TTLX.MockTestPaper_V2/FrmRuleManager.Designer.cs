namespace TTLX.MockTestPaper_V2
{
    partial class FrmRuleManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRuleManager));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnAddRule = new System.Windows.Forms.Button();
            this.cbRules = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnDelRule = new System.Windows.Forms.Button();
            this.tbRuleDesc = new System.Windows.Forms.RichTextBox();
            this.btnEditRule = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lblRuleName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblSpecialtyName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ruleTree = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.splitContainer1.Panel1.Controls.Add(this.btnAddRule);
            this.splitContainer1.Panel1.Controls.Add(this.cbRules);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(991, 565);
            this.splitContainer1.SplitterDistance = 49;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnAddRule
            // 
            this.btnAddRule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddRule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(158)))), ((int)(((byte)(216)))));
            this.btnAddRule.FlatAppearance.BorderSize = 0;
            this.btnAddRule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddRule.Font = new System.Drawing.Font("宋体", 12F);
            this.btnAddRule.ForeColor = System.Drawing.Color.White;
            this.btnAddRule.Location = new System.Drawing.Point(895, 10);
            this.btnAddRule.Name = "btnAddRule";
            this.btnAddRule.Size = new System.Drawing.Size(84, 31);
            this.btnAddRule.TabIndex = 13;
            this.btnAddRule.Text = "添加规则";
            this.btnAddRule.UseVisualStyleBackColor = false;
            this.btnAddRule.Click += new System.EventHandler(this.btnAddRule_Click);
            // 
            // cbRules
            // 
            this.cbRules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRules.Font = new System.Drawing.Font("宋体", 12F);
            this.cbRules.FormattingEnabled = true;
            this.cbRules.Location = new System.Drawing.Point(106, 11);
            this.cbRules.Name = "cbRules";
            this.cbRules.Size = new System.Drawing.Size(234, 24);
            this.cbRules.TabIndex = 1;
            this.cbRules.SelectedIndexChanged += new System.EventHandler(this.cbRules_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "出题规则：";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnDelRule);
            this.splitContainer2.Panel1.Controls.Add(this.tbRuleDesc);
            this.splitContainer2.Panel1.Controls.Add(this.btnEditRule);
            this.splitContainer2.Panel1.Controls.Add(this.label6);
            this.splitContainer2.Panel1.Controls.Add(this.lblRuleName);
            this.splitContainer2.Panel1.Controls.Add(this.label5);
            this.splitContainer2.Panel1.Controls.Add(this.lblSpecialtyName);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ruleTree);
            this.splitContainer2.Size = new System.Drawing.Size(991, 512);
            this.splitContainer2.SplitterDistance = 313;
            this.splitContainer2.TabIndex = 0;
            // 
            // btnDelRule
            // 
            this.btnDelRule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelRule.BackColor = System.Drawing.Color.Red;
            this.btnDelRule.FlatAppearance.BorderSize = 0;
            this.btnDelRule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelRule.Font = new System.Drawing.Font("宋体", 12F);
            this.btnDelRule.ForeColor = System.Drawing.Color.White;
            this.btnDelRule.Location = new System.Drawing.Point(226, 469);
            this.btnDelRule.Name = "btnDelRule";
            this.btnDelRule.Size = new System.Drawing.Size(84, 31);
            this.btnDelRule.TabIndex = 15;
            this.btnDelRule.Text = "删除规则";
            this.btnDelRule.UseVisualStyleBackColor = false;
            this.btnDelRule.Click += new System.EventHandler(this.btnDelRule_Click);
            // 
            // tbRuleDesc
            // 
            this.tbRuleDesc.Font = new System.Drawing.Font("宋体", 12F);
            this.tbRuleDesc.Location = new System.Drawing.Point(74, 89);
            this.tbRuleDesc.Name = "tbRuleDesc";
            this.tbRuleDesc.ReadOnly = true;
            this.tbRuleDesc.Size = new System.Drawing.Size(236, 371);
            this.tbRuleDesc.TabIndex = 5;
            this.tbRuleDesc.Text = "";
            // 
            // btnEditRule
            // 
            this.btnEditRule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditRule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(153)))), ((int)(((byte)(0)))));
            this.btnEditRule.FlatAppearance.BorderSize = 0;
            this.btnEditRule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditRule.Font = new System.Drawing.Font("宋体", 12F);
            this.btnEditRule.ForeColor = System.Drawing.Color.White;
            this.btnEditRule.Location = new System.Drawing.Point(122, 469);
            this.btnEditRule.Name = "btnEditRule";
            this.btnEditRule.Size = new System.Drawing.Size(84, 31);
            this.btnEditRule.TabIndex = 14;
            this.btnEditRule.Text = "修改规则";
            this.btnEditRule.UseVisualStyleBackColor = false;
            this.btnEditRule.Click += new System.EventHandler(this.btnEditRule_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F);
            this.label6.Location = new System.Drawing.Point(12, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "描述：";
            // 
            // lblRuleName
            // 
            this.lblRuleName.AutoSize = true;
            this.lblRuleName.Font = new System.Drawing.Font("宋体", 12F);
            this.lblRuleName.Location = new System.Drawing.Point(74, 56);
            this.lblRuleName.Name = "lblRuleName";
            this.lblRuleName.Size = new System.Drawing.Size(72, 16);
            this.lblRuleName.TabIndex = 3;
            this.lblRuleName.Text = "计算机类";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F);
            this.label5.Location = new System.Drawing.Point(12, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 2;
            this.label5.Text = "名称：";
            // 
            // lblSpecialtyName
            // 
            this.lblSpecialtyName.AutoSize = true;
            this.lblSpecialtyName.Font = new System.Drawing.Font("宋体", 12F);
            this.lblSpecialtyName.Location = new System.Drawing.Point(74, 21);
            this.lblSpecialtyName.Name = "lblSpecialtyName";
            this.lblSpecialtyName.Size = new System.Drawing.Size(72, 16);
            this.lblSpecialtyName.TabIndex = 1;
            this.lblSpecialtyName.Text = "计算机类";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(12, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "专业：";
            // 
            // ruleTree
            // 
            this.ruleTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ruleTree.Font = new System.Drawing.Font("宋体", 12F);
            this.ruleTree.Location = new System.Drawing.Point(0, 0);
            this.ruleTree.Name = "ruleTree";
            this.ruleTree.Size = new System.Drawing.Size(674, 512);
            this.ruleTree.TabIndex = 0;
            // 
            // FrmRuleManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 565);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRuleManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "出题规则管理";
            this.Load += new System.EventHandler(this.FrmRuleManager_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox cbRules;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddRule;
        private System.Windows.Forms.Button btnDelRule;
        private System.Windows.Forms.Button btnEditRule;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView ruleTree;
        private System.Windows.Forms.Label lblSpecialtyName;
        private System.Windows.Forms.Label lblRuleName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox tbRuleDesc;
    }
}