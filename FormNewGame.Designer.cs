namespace RPG
{
    partial class FormNewGame
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnNewCharacter = new System.Windows.Forms.Button();
            this.btnUsePremadeCharacter = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(115, 48);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(266, 31);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "New Game Options";
            // 
            // btnNewCharacter
            // 
            this.btnNewCharacter.Location = new System.Drawing.Point(178, 160);
            this.btnNewCharacter.Name = "btnNewCharacter";
            this.btnNewCharacter.Size = new System.Drawing.Size(125, 23);
            this.btnNewCharacter.TabIndex = 1;
            this.btnNewCharacter.Text = "Create New Character";
            this.btnNewCharacter.UseVisualStyleBackColor = true;
            this.btnNewCharacter.Click += new System.EventHandler(this.btnNewCharacter_Click);
            // 
            // btnUsePremadeCharacter
            // 
            this.btnUsePremadeCharacter.Location = new System.Drawing.Point(169, 221);
            this.btnUsePremadeCharacter.Name = "btnUsePremadeCharacter";
            this.btnUsePremadeCharacter.Size = new System.Drawing.Size(144, 23);
            this.btnUsePremadeCharacter.TabIndex = 2;
            this.btnUsePremadeCharacter.Text = "Use Premade Character";
            this.btnUsePremadeCharacter.UseVisualStyleBackColor = true;
            this.btnUsePremadeCharacter.Click += new System.EventHandler(this.btnUsePremadeCharacter_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(201, 280);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormNewGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 466);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUsePremadeCharacter);
            this.Controls.Add(this.btnNewCharacter);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormNewGame";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RPG | NewGame";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnNewCharacter;
        private System.Windows.Forms.Button btnUsePremadeCharacter;
        private System.Windows.Forms.Button btnCancel;
    }
}