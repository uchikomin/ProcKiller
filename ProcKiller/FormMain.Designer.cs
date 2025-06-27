using System.Drawing;
using System.Windows.Forms;

namespace ProcKiller
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxKeyword = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.listViewProcess = new System.Windows.Forms.ListView();
            this.columnHeaderPid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderProcessName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderImagePathName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonKill = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "キーワード";
            // 
            // textBoxKeyword
            // 
            this.textBoxKeyword.AcceptsReturn = true;
            this.textBoxKeyword.Location = new System.Drawing.Point(69, 5);
            this.textBoxKeyword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxKeyword.Name = "textBoxKeyword";
            this.textBoxKeyword.Size = new System.Drawing.Size(174, 19);
            this.textBoxKeyword.TabIndex = 0;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(249, 4);
            this.buttonSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(64, 20);
            this.buttonSearch.TabIndex = 1;
            this.buttonSearch.Text = "検索";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "対象プロセス";
            // 
            // listViewProcess
            // 
            this.listViewProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewProcess.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderPid,
            this.columnHeaderProcessName,
            this.columnHeaderImagePathName});
            this.listViewProcess.FullRowSelect = true;
            this.listViewProcess.HideSelection = false;
            this.listViewProcess.Location = new System.Drawing.Point(10, 46);
            this.listViewProcess.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listViewProcess.Name = "listViewProcess";
            this.listViewProcess.Size = new System.Drawing.Size(640, 259);
            this.listViewProcess.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewProcess.TabIndex = 2;
            this.listViewProcess.UseCompatibleStateImageBehavior = false;
            this.listViewProcess.View = System.Windows.Forms.View.Details;
            this.listViewProcess.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewProcess_ColumnClick);
            this.listViewProcess.SelectedIndexChanged += new System.EventHandler(this.listViewProcess_SelectedIndexChanged);
            this.listViewProcess.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewProcess_KeyDown);
            // 
            // columnHeaderPid
            // 
            this.columnHeaderPid.Text = "PID";
            this.columnHeaderPid.Width = 80;
            // 
            // columnHeaderProcessName
            // 
            this.columnHeaderProcessName.Text = "プロセス名";
            this.columnHeaderProcessName.Width = 160;
            // 
            // columnHeaderImagePathName
            // 
            this.columnHeaderImagePathName.Text = "イメージパス名";
            this.columnHeaderImagePathName.Width = 360;
            // 
            // buttonKill
            // 
            this.buttonKill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonKill.Enabled = false;
            this.buttonKill.Location = new System.Drawing.Point(556, 309);
            this.buttonKill.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonKill.Name = "buttonKill";
            this.buttonKill.Size = new System.Drawing.Size(94, 28);
            this.buttonKill.TabIndex = 4;
            this.buttonKill.Text = "プロセス停止";
            this.buttonKill.UseVisualStyleBackColor = true;
            this.buttonKill.Click += new System.EventHandler(this.buttonKill_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemove.Location = new System.Drawing.Point(575, 18);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 3;
            this.buttonRemove.Text = "除外";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // FormMain
            // 
            this.AcceptButton = this.buttonSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 348);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonKill);
            this.Controls.Add(this.listViewProcess);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textBoxKeyword);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(676, 387);
            this.Name = "FormMain";
            this.Text = "Process Killer";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox textBoxKeyword;
        private Button buttonSearch;
        private Label label2;
        private ListView listViewProcess;
        private ColumnHeader columnHeaderPid;
        private ColumnHeader columnHeaderProcessName;
        private ColumnHeader columnHeaderImagePathName;
        private Button buttonKill;
        private Button buttonRemove;
    }
}
