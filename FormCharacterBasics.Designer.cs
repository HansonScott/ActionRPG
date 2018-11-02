namespace RPG
{
    partial class FormCharacterBasics
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
            this.btn_Done = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.cbGender = new System.Windows.Forms.ComboBox();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblAlignment = new System.Windows.Forms.Label();
            this.cbAlignment = new System.Windows.Forms.ComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblColor1 = new System.Windows.Forms.Label();
            this.btnColor1 = new System.Windows.Forms.Button();
            this.btnColor2 = new System.Windows.Forms.Button();
            this.lblColor2 = new System.Windows.Forms.Label();
            this.lblColor1Desc = new System.Windows.Forms.Label();
            this.lblColor2Desc = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Done
            // 
            this.btn_Done.Location = new System.Drawing.Point(405, 431);
            this.btn_Done.Name = "btn_Done";
            this.btn_Done.Size = new System.Drawing.Size(75, 23);
            this.btn_Done.TabIndex = 3;
            this.btn_Done.Text = "Done";
            this.btn_Done.UseVisualStyleBackColor = true;
            this.btn_Done.Click += new System.EventHandler(this.btn_Done_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(148, 27);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(193, 13);
            this.lblTitle.TabIndex = 7;
            this.lblTitle.Text = "Fill in your character\'s basic information.";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(192, 95);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(149, 20);
            this.tbName.TabIndex = 0;
            // 
            // cbGender
            // 
            this.cbGender.DisplayMember = "Name";
            this.cbGender.DropDownHeight = 50;
            this.cbGender.FormattingEnabled = true;
            this.cbGender.IntegralHeight = false;
            this.cbGender.Location = new System.Drawing.Point(192, 145);
            this.cbGender.MaxDropDownItems = 2;
            this.cbGender.Name = "cbGender";
            this.cbGender.Size = new System.Drawing.Size(149, 21);
            this.cbGender.TabIndex = 1;
            this.cbGender.ValueMember = "ID";
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Location = new System.Drawing.Point(128, 148);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(45, 13);
            this.lblGender.TabIndex = 5;
            this.lblGender.Text = "Gender:";
            // 
            // lblAlignment
            // 
            this.lblAlignment.AutoSize = true;
            this.lblAlignment.Location = new System.Drawing.Point(117, 204);
            this.lblAlignment.Name = "lblAlignment";
            this.lblAlignment.Size = new System.Drawing.Size(56, 13);
            this.lblAlignment.TabIndex = 6;
            this.lblAlignment.Text = "Alignment:";
            // 
            // cbAlignment
            // 
            this.cbAlignment.DisplayMember = "Name";
            this.cbAlignment.DropDownHeight = 120;
            this.cbAlignment.FormattingEnabled = true;
            this.cbAlignment.IntegralHeight = false;
            this.cbAlignment.Location = new System.Drawing.Point(192, 201);
            this.cbAlignment.MaxDropDownItems = 9;
            this.cbAlignment.Name = "cbAlignment";
            this.cbAlignment.Size = new System.Drawing.Size(149, 21);
            this.cbAlignment.TabIndex = 2;
            this.cbAlignment.ValueMember = "ID";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(135, 98);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 8;
            this.lblName.Text = "Name:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(12, 431);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblColor1
            // 
            this.lblColor1.AutoSize = true;
            this.lblColor1.Location = new System.Drawing.Point(105, 268);
            this.lblColor1.Name = "lblColor1";
            this.lblColor1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblColor1.Size = new System.Drawing.Size(68, 13);
            this.lblColor1.TabIndex = 10;
            this.lblColor1.Text = "Primary Color";
            this.lblColor1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnColor1
            // 
            this.btnColor1.Location = new System.Drawing.Point(192, 263);
            this.btnColor1.Name = "btnColor1";
            this.btnColor1.Size = new System.Drawing.Size(23, 23);
            this.btnColor1.TabIndex = 11;
            this.btnColor1.Text = " ";
            this.btnColor1.UseVisualStyleBackColor = true;
            this.btnColor1.Click += new System.EventHandler(this.btnColor1_Click);
            // 
            // btnColor2
            // 
            this.btnColor2.Location = new System.Drawing.Point(192, 308);
            this.btnColor2.Name = "btnColor2";
            this.btnColor2.Size = new System.Drawing.Size(23, 23);
            this.btnColor2.TabIndex = 13;
            this.btnColor2.Text = " ";
            this.btnColor2.UseVisualStyleBackColor = true;
            this.btnColor2.Click += new System.EventHandler(this.btnColor2_Click);
            // 
            // lblColor2
            // 
            this.lblColor2.AutoSize = true;
            this.lblColor2.Location = new System.Drawing.Point(88, 313);
            this.lblColor2.Name = "lblColor2";
            this.lblColor2.Size = new System.Drawing.Size(85, 13);
            this.lblColor2.TabIndex = 12;
            this.lblColor2.Text = "Secondary Color";
            // 
            // lblColor1Desc
            // 
            this.lblColor1Desc.AutoSize = true;
            this.lblColor1Desc.Location = new System.Drawing.Point(221, 268);
            this.lblColor1Desc.Name = "lblColor1Desc";
            this.lblColor1Desc.Size = new System.Drawing.Size(77, 13);
            this.lblColor1Desc.TabIndex = 14;
            this.lblColor1Desc.Text = "(Used for Skin)";
            // 
            // lblColor2Desc
            // 
            this.lblColor2Desc.AutoSize = true;
            this.lblColor2Desc.Location = new System.Drawing.Point(221, 313);
            this.lblColor2Desc.Name = "lblColor2Desc";
            this.lblColor2Desc.Size = new System.Drawing.Size(136, 13);
            this.lblColor2Desc.TabIndex = 15;
            this.lblColor2Desc.Text = "(Used for hair, eyes, mouth)";
            // 
            // FormCharacterBasics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 466);
            this.ControlBox = false;
            this.Controls.Add(this.lblColor2Desc);
            this.Controls.Add(this.lblColor1Desc);
            this.Controls.Add(this.btnColor2);
            this.Controls.Add(this.lblColor2);
            this.Controls.Add(this.btnColor1);
            this.Controls.Add(this.lblColor1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblAlignment);
            this.Controls.Add(this.cbAlignment);
            this.Controls.Add(this.lblGender);
            this.Controls.Add(this.cbGender);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btn_Done);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCharacterBasics";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RPG | Character Basics";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Done;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.ComboBox cbGender;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label lblAlignment;
        private System.Windows.Forms.ComboBox cbAlignment;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblColor1;
        private System.Windows.Forms.Button btnColor1;
        private System.Windows.Forms.Button btnColor2;
        private System.Windows.Forms.Label lblColor2;
        private System.Windows.Forms.Label lblColor1Desc;
        private System.Windows.Forms.Label lblColor2Desc;
    }
}