namespace TTLX.MockTestPaper_V2
{
    partial class FrmPapers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPapers));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cbRules = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEditRecord = new System.Windows.Forms.Button();
            this.btnRuleManager = new System.Windows.Forms.Button();
            this.btnQuestion = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgPapers = new System.Windows.Forms.DataGridView();
            this.lblUserName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPapers)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.splitContainer1.Panel1.Controls.Add(this.lblUserName);
            this.splitContainer1.Panel1.Controls.Add(this.cbRules);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btnEditRecord);
            this.splitContainer1.Panel1.Controls.Add(this.btnRuleManager);
            this.splitContainer1.Panel1.Controls.Add(this.btnQuestion);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(1113, 664);
            this.splitContainer1.SplitterDistance = 49;
            this.splitContainer1.TabIndex = 0;
            // 
            // cbRules
            // 
            this.cbRules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRules.Font = new System.Drawing.Font("宋体", 12F);
            this.cbRules.FormattingEnabled = true;
            this.cbRules.ItemHeight = 16;
            this.cbRules.Location = new System.Drawing.Point(434, 14);
            this.cbRules.Name = "cbRules";
            this.cbRules.Size = new System.Drawing.Size(264, 24);
            this.cbRules.TabIndex = 16;
            this.cbRules.SelectedIndexChanged += new System.EventHandler(this.cbRules_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(340, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "出题规则：";
            // 
            // btnEditRecord
            // 
            this.btnEditRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(158)))), ((int)(((byte)(216)))));
            this.btnEditRecord.FlatAppearance.BorderSize = 0;
            this.btnEditRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditRecord.Font = new System.Drawing.Font("宋体", 12F);
            this.btnEditRecord.ForeColor = System.Drawing.Color.White;
            this.btnEditRecord.Location = new System.Drawing.Point(847, 10);
            this.btnEditRecord.Name = "btnEditRecord";
            this.btnEditRecord.Size = new System.Drawing.Size(121, 31);
            this.btnEditRecord.TabIndex = 14;
            this.btnEditRecord.Text = "本地编辑记录";
            this.btnEditRecord.UseVisualStyleBackColor = false;
            this.btnEditRecord.Click += new System.EventHandler(this.btnEditRecord_Click);
            // 
            // btnRuleManager
            // 
            this.btnRuleManager.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRuleManager.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(158)))), ((int)(((byte)(216)))));
            this.btnRuleManager.FlatAppearance.BorderSize = 0;
            this.btnRuleManager.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRuleManager.Font = new System.Drawing.Font("宋体", 12F);
            this.btnRuleManager.ForeColor = System.Drawing.Color.White;
            this.btnRuleManager.Location = new System.Drawing.Point(716, 10);
            this.btnRuleManager.Name = "btnRuleManager";
            this.btnRuleManager.Size = new System.Drawing.Size(125, 31);
            this.btnRuleManager.TabIndex = 12;
            this.btnRuleManager.Text = "出题规则管理";
            this.btnRuleManager.UseVisualStyleBackColor = false;
            this.btnRuleManager.Click += new System.EventHandler(this.btnAddRule_Click);
            // 
            // btnQuestion
            // 
            this.btnQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuestion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(158)))), ((int)(((byte)(216)))));
            this.btnQuestion.FlatAppearance.BorderSize = 0;
            this.btnQuestion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuestion.Font = new System.Drawing.Font("宋体", 12F);
            this.btnQuestion.ForeColor = System.Drawing.Color.White;
            this.btnQuestion.Location = new System.Drawing.Point(974, 10);
            this.btnQuestion.Name = "btnQuestion";
            this.btnQuestion.Size = new System.Drawing.Size(127, 31);
            this.btnQuestion.TabIndex = 11;
            this.btnQuestion.Text = "模拟试卷出题";
            this.btnQuestion.UseVisualStyleBackColor = false;
            this.btnQuestion.Click += new System.EventHandler(this.btnQuestion_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgPapers);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1113, 611);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "模拟试卷";
            // 
            // dgPapers
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPapers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgPapers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPapers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPapers.Location = new System.Drawing.Point(3, 17);
            this.dgPapers.Name = "dgPapers";
            this.dgPapers.RowTemplate.Height = 23;
            this.dgPapers.Size = new System.Drawing.Size(1107, 591);
            this.dgPapers.TabIndex = 0;
            this.dgPapers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPapers_CellContentClick);
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.lblUserName.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblUserName.Location = new System.Drawing.Point(21, 17);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(76, 16);
            this.lblUserName.TabIndex = 17;
            this.lblUserName.Text = "欢迎您：";
            // 
            // FrmPapers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 664);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPapers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "我的模拟试卷";
            this.Load += new System.EventHandler(this.FrmPapers_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPapers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnRuleManager;
        private System.Windows.Forms.Button btnQuestion;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgPapers;
        private System.Windows.Forms.Button btnEditRecord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbRules;
        private System.Windows.Forms.Label lblUserName;
    }
}