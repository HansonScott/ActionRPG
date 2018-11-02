using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace RPG
{
    class FormLog: Form
    {
        public TextBox tbLog;

        public FormLog()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.tbLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbLog
            // 
            this.tbLog.Location = new System.Drawing.Point(12, 12);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.Size = new System.Drawing.Size(468, 442);
            this.tbLog.TabIndex = 0;
            // 
            // FormLog
            // 
            this.ClientSize = new System.Drawing.Size(492, 466);
            this.Controls.Add(this.tbLog);
            this.Name = "FormLog";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
