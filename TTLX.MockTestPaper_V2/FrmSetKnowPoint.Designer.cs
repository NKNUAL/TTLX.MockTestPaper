namespace TTLX.MockTestPaper_V2
{
    partial class FrmSetKnowPoint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSetKnowPoint));
            this.label1 = new System.Windows.Forms.Label();
            this.lblQueType = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCourseName = new System.Windows.Forms.Label();
            this.tbCount = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.listBox_rules = new System.Windows.Forms.ListBox();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.listBox_pre = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblSetted = new System.Windows.Forms.Label();
            this.lblNotSet = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbCount)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "题目类型：";
            // 
            // lblQueType
            // 
            this.lblQueType.AutoSize = true;
            this.lblQueType.Font = new System.Drawing.Font("宋体", 12F);
            this.lblQueType.Location = new System.Drawing.Point(106, 23);
            this.lblQueType.Name = "lblQueType";
            this.lblQueType.Size = new System.Drawing.Size(56, 16);
            this.lblQueType.TabIndex = 1;
            this.lblQueType.Text = "单选题";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(12, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "科目：";
            // 
            // lblCourseName
            // 
            this.lblCourseName.AutoSize = true;
            this.lblCourseName.Font = new System.Drawing.Font("宋体", 12F);
            this.lblCourseName.Location = new System.Drawing.Point(106, 60);
            this.lblCourseName.Name = "lblCourseName";
            this.lblCourseName.Size = new System.Drawing.Size(72, 16);
            this.lblCourseName.TabIndex = 3;
            this.lblCourseName.Text = "科目科目";
            // 
            // tbCount
            // 
            this.tbCount.Location = new System.Drawing.Point(507, 190);
            this.tbCount.Name = "tbCount";
            this.tbCount.Size = new System.Drawing.Size(53, 21);
            this.tbCount.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F);
            this.label7.Location = new System.Drawing.Point(-67, 115);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 18;
            this.label7.Text = "知识点列表";
            // 
            // listBox_rules
            // 
            this.listBox_rules.Font = new System.Drawing.Font("宋体", 12F);
            this.listBox_rules.FormattingEnabled = true;
            this.listBox_rules.HorizontalScrollbar = true;
            this.listBox_rules.ItemHeight = 16;
            this.listBox_rules.Location = new System.Drawing.Point(566, 128);
            this.listBox_rules.Name = "listBox_rules";
            this.listBox_rules.Size = new System.Drawing.Size(471, 260);
            this.listBox_rules.TabIndex = 17;
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(469, 246);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(88, 23);
            this.btnDel.TabIndex = 16;
            this.btnDel.Text = "《《 移除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(471, 217);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 23);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "添加 》》";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(469, 195);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "数量：";
            // 
            // listBox_pre
            // 
            this.listBox_pre.Font = new System.Drawing.Font("宋体", 12F);
            this.listBox_pre.FormattingEnabled = true;
            this.listBox_pre.HorizontalScrollbar = true;
            this.listBox_pre.ItemHeight = 16;
            this.listBox_pre.Location = new System.Drawing.Point(15, 128);
            this.listBox_pre.Name = "listBox_pre";
            this.listBox_pre.Size = new System.Drawing.Size(450, 260);
            this.listBox_pre.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F);
            this.label6.Location = new System.Drawing.Point(12, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 16);
            this.label6.TabIndex = 20;
            this.label6.Text = "题目总数：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F);
            this.label8.Location = new System.Drawing.Point(285, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 16);
            this.label8.TabIndex = 21;
            this.label8.Text = "已设置数量：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 12F);
            this.label9.Location = new System.Drawing.Point(577, 95);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 16);
            this.label9.TabIndex = 22;
            this.label9.Text = "未设置数量：";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("宋体", 12F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(951, 403);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(86, 31);
            this.btnClose.TabIndex = 23;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("宋体", 12F);
            this.lblTotal.Location = new System.Drawing.Point(106, 95);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(16, 16);
            this.lblTotal.TabIndex = 24;
            this.lblTotal.Text = "0";
            // 
            // lblSetted
            // 
            this.lblSetted.AutoSize = true;
            this.lblSetted.Font = new System.Drawing.Font("宋体", 12F);
            this.lblSetted.Location = new System.Drawing.Point(395, 95);
            this.lblSetted.Name = "lblSetted";
            this.lblSetted.Size = new System.Drawing.Size(16, 16);
            this.lblSetted.TabIndex = 25;
            this.lblSetted.Text = "0";
            // 
            // lblNotSet
            // 
            this.lblNotSet.AutoSize = true;
            this.lblNotSet.Font = new System.Drawing.Font("宋体", 12F);
            this.lblNotSet.Location = new System.Drawing.Point(687, 95);
            this.lblNotSet.Name = "lblNotSet";
            this.lblNotSet.Size = new System.Drawing.Size(16, 16);
            this.lblNotSet.TabIndex = 26;
            this.lblNotSet.Text = "0";
            // 
            // FrmSetKnowPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 445);
            this.Controls.Add(this.lblNotSet);
            this.Controls.Add(this.lblSetted);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbCount);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.listBox_rules);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listBox_pre);
            this.Controls.Add(this.lblCourseName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblQueType);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSetKnowPoint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置知识点";
            this.Load += new System.EventHandler(this.FrmSetKnowPoint_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblQueType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCourseName;
        private System.Windows.Forms.NumericUpDown tbCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox listBox_rules;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listBox_pre;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblSetted;
        private System.Windows.Forms.Label lblNotSet;
    }
}