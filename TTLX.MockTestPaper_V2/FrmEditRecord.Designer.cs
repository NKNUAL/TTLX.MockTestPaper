namespace TTLX.MockTestPaper_V2
{
    partial class FrmEditRecord
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEditRecord));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblCount = new System.Windows.Forms.Label();
            this.cbRules = new System.Windows.Forms.ComboBox();
            this.lblCbRule = new System.Windows.Forms.Label();
            this.dgRecord = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRecord)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.lblCount);
            this.splitContainer1.Panel1.Controls.Add(this.cbRules);
            this.splitContainer1.Panel1.Controls.Add(this.lblCbRule);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgRecord);
            this.splitContainer1.Size = new System.Drawing.Size(1029, 604);
            this.splitContainer1.SplitterDistance = 56;
            this.splitContainer1.TabIndex = 0;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("宋体", 12F);
            this.lblCount.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblCount.Location = new System.Drawing.Point(886, 20);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(80, 16);
            this.lblCount.TabIndex = 2;
            this.lblCount.Text = "共5条记录";
            // 
            // cbRules
            // 
            this.cbRules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRules.Font = new System.Drawing.Font("宋体", 12F);
            this.cbRules.FormattingEnabled = true;
            this.cbRules.Location = new System.Drawing.Point(115, 17);
            this.cbRules.Name = "cbRules";
            this.cbRules.Size = new System.Drawing.Size(355, 24);
            this.cbRules.TabIndex = 1;
            this.cbRules.SelectedIndexChanged += new System.EventHandler(this.cbRules_SelectedIndexChanged);
            // 
            // lblCbRule
            // 
            this.lblCbRule.AutoSize = true;
            this.lblCbRule.Font = new System.Drawing.Font("宋体", 12F);
            this.lblCbRule.Location = new System.Drawing.Point(21, 20);
            this.lblCbRule.Name = "lblCbRule";
            this.lblCbRule.Size = new System.Drawing.Size(88, 16);
            this.lblCbRule.TabIndex = 0;
            this.lblCbRule.Text = "出题规则：";
            // 
            // dgRecord
            // 
            this.dgRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgRecord.Location = new System.Drawing.Point(0, 0);
            this.dgRecord.Name = "dgRecord";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgRecord.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgRecord.RowTemplate.Height = 23;
            this.dgRecord.Size = new System.Drawing.Size(1029, 544);
            this.dgRecord.TabIndex = 0;
            this.dgRecord.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgRecord_CellContentClick);
            // 
            // FrmEditRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 604);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmEditRecord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "模拟试卷出题记录";
            this.Load += new System.EventHandler(this.FrmEditRecord_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgRecord)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblCbRule;
        private System.Windows.Forms.ComboBox cbRules;
        private System.Windows.Forms.DataGridView dgRecord;
        private System.Windows.Forms.Label lblCount;
    }
}