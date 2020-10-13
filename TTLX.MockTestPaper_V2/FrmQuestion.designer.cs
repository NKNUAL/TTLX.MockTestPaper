namespace TTLX.MockTestPaper_V2
{
    partial class FrmQuestion
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQuestion));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutLabel = new System.Windows.Forms.FlowLayoutPanel();
            this.lblRuleName = new System.Windows.Forms.Label();
            this.tbRuleDesc = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ruleTree = new System.Windows.Forms.TreeView();
            this.dgvQuestions = new System.Windows.Forms.DataGridView();
            this.colCourse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKnow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQueType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQueContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEdit = new System.Windows.Forms.DataGridViewLinkColumn();
            this.tbPaperName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnQuestion = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuestions)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbPaperName);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.btnQuestion);
            this.splitContainer1.Size = new System.Drawing.Size(1746, 739);
            this.splitContainer1.SplitterDistance = 679;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgvQuestions);
            this.splitContainer2.Size = new System.Drawing.Size(1746, 679);
            this.splitContainer2.SplitterDistance = 450;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer3.Size = new System.Drawing.Size(450, 679);
            this.splitContainer3.SplitterDistance = 214;
            this.splitContainer3.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutLabel);
            this.groupBox1.Controls.Add(this.lblRuleName);
            this.groupBox1.Controls.Add(this.tbRuleDesc);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 214);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "规则信息";
            // 
            // flowLayoutLabel
            // 
            this.flowLayoutLabel.Location = new System.Drawing.Point(12, 160);
            this.flowLayoutLabel.Name = "flowLayoutLabel";
            this.flowLayoutLabel.Padding = new System.Windows.Forms.Padding(3);
            this.flowLayoutLabel.Size = new System.Drawing.Size(432, 48);
            this.flowLayoutLabel.TabIndex = 10;
            // 
            // lblRuleName
            // 
            this.lblRuleName.AutoSize = true;
            this.lblRuleName.Font = new System.Drawing.Font("宋体", 12F);
            this.lblRuleName.Location = new System.Drawing.Point(100, 26);
            this.lblRuleName.Name = "lblRuleName";
            this.lblRuleName.Size = new System.Drawing.Size(72, 16);
            this.lblRuleName.TabIndex = 6;
            this.lblRuleName.Text = "测试规则";
            // 
            // tbRuleDesc
            // 
            this.tbRuleDesc.Font = new System.Drawing.Font("宋体", 12F);
            this.tbRuleDesc.Location = new System.Drawing.Point(103, 59);
            this.tbRuleDesc.Name = "tbRuleDesc";
            this.tbRuleDesc.ReadOnly = true;
            this.tbRuleDesc.Size = new System.Drawing.Size(341, 82);
            this.tbRuleDesc.TabIndex = 5;
            this.tbRuleDesc.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F);
            this.label5.Location = new System.Drawing.Point(6, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "规则描述：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "规则名称：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ruleTree);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(450, 461);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "出题规则";
            // 
            // ruleTree
            // 
            this.ruleTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ruleTree.Font = new System.Drawing.Font("宋体", 12F);
            this.ruleTree.Location = new System.Drawing.Point(3, 17);
            this.ruleTree.Name = "ruleTree";
            this.ruleTree.Size = new System.Drawing.Size(444, 441);
            this.ruleTree.TabIndex = 0;
            this.ruleTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ruleTree_AfterSelect);
            // 
            // dgvQuestions
            // 
            this.dgvQuestions.AllowUserToAddRows = false;
            this.dgvQuestions.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvQuestions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvQuestions.ColumnHeadersHeight = 32;
            this.dgvQuestions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvQuestions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCourse,
            this.colKnow,
            this.colQueType,
            this.colQueContent,
            this.colEdit});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvQuestions.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvQuestions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvQuestions.EnableHeadersVisualStyles = false;
            this.dgvQuestions.Location = new System.Drawing.Point(0, 0);
            this.dgvQuestions.Name = "dgvQuestions";
            this.dgvQuestions.ReadOnly = true;
            this.dgvQuestions.RowHeadersVisible = false;
            this.dgvQuestions.RowHeadersWidth = 40;
            this.dgvQuestions.RowTemplate.Height = 32;
            this.dgvQuestions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQuestions.Size = new System.Drawing.Size(1292, 679);
            this.dgvQuestions.TabIndex = 7;
            this.dgvQuestions.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvQuestions_CellContentClick);
            // 
            // colCourse
            // 
            this.colCourse.HeaderText = "科目";
            this.colCourse.Name = "colCourse";
            this.colCourse.ReadOnly = true;
            this.colCourse.Width = 280;
            // 
            // colKnow
            // 
            this.colKnow.HeaderText = "知识点";
            this.colKnow.Name = "colKnow";
            this.colKnow.ReadOnly = true;
            this.colKnow.Width = 280;
            // 
            // colQueType
            // 
            this.colQueType.HeaderText = "题型";
            this.colQueType.Name = "colQueType";
            this.colQueType.ReadOnly = true;
            this.colQueType.Width = 80;
            // 
            // colQueContent
            // 
            this.colQueContent.HeaderText = "试题内容";
            this.colQueContent.Name = "colQueContent";
            this.colQueContent.ReadOnly = true;
            this.colQueContent.Width = 280;
            // 
            // colEdit
            // 
            this.colEdit.HeaderText = "";
            this.colEdit.Name = "colEdit";
            this.colEdit.ReadOnly = true;
            // 
            // tbPaperName
            // 
            this.tbPaperName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPaperName.Font = new System.Drawing.Font("宋体", 12F);
            this.tbPaperName.Location = new System.Drawing.Point(1274, 11);
            this.tbPaperName.Name = "tbPaperName";
            this.tbPaperName.Size = new System.Drawing.Size(301, 26);
            this.tbPaperName.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(1180, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "试卷名称：";
            // 
            // btnQuestion
            // 
            this.btnQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuestion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(158)))), ((int)(((byte)(216)))));
            this.btnQuestion.FlatAppearance.BorderSize = 0;
            this.btnQuestion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuestion.Font = new System.Drawing.Font("宋体", 12F);
            this.btnQuestion.ForeColor = System.Drawing.Color.White;
            this.btnQuestion.Location = new System.Drawing.Point(1607, 11);
            this.btnQuestion.Name = "btnQuestion";
            this.btnQuestion.Size = new System.Drawing.Size(127, 31);
            this.btnQuestion.TabIndex = 12;
            this.btnQuestion.Text = "生成模拟试卷";
            this.btnQuestion.UseVisualStyleBackColor = false;
            this.btnQuestion.Click += new System.EventHandler(this.btnQuestion_Click);
            // 
            // FrmQuestion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1746, 739);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmQuestion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "模拟试卷出题";
            this.Load += new System.EventHandler(this.FrmQuestion2_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuestions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgvQuestions;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox tbRuleDesc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TreeView ruleTree;
        private System.Windows.Forms.Button btnQuestion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPaperName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCourse;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKnow;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQueType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQueContent;
        private System.Windows.Forms.DataGridViewLinkColumn colEdit;
        private System.Windows.Forms.Label lblRuleName;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutLabel;
    }
}