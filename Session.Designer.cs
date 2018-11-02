namespace RPG
{
    public partial class Session
    {
        #region Declarations
        private System.Windows.Forms.TabControl tabControl1;
        private TabPageMenu TabPageMenu;

        public TabPageAction TabPageAction;
        public TabPageCharacterInfo TabPageAttributes;
        public TabPageInventory TabPageInventory;
        public TabPageMagic TabPageMagic;

        private System.Windows.Forms.TabPage TabPageMap;
        // Required designer variable.
        private System.ComponentModel.IContainer components = null;
        #endregion

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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TabPageAction = new RPG.TabPageAction();
            this.TabPageMap = new System.Windows.Forms.TabPage();
            this.TabPageAttributes = new RPG.TabPageCharacterInfo();
            this.TabPageInventory = new RPG.TabPageInventory();
            this.TabPageMagic = new RPG.TabPageMagic();
            this.TabPageMenu = new RPG.TabPageMenu();
            this.tabControl1.SuspendLayout();
            this.TabPageMagic.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TabPageAction);
            this.tabControl1.Controls.Add(this.TabPageMap);
            this.tabControl1.Controls.Add(this.TabPageAttributes);
            this.tabControl1.Controls.Add(this.TabPageInventory);
            this.tabControl1.Controls.Add(this.TabPageMagic);
            this.tabControl1.Controls.Add(this.TabPageMenu);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1017, 735);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // TabPageAction
            // 
            this.TabPageAction.Location = new System.Drawing.Point(4, 22);
            this.TabPageAction.Name = "TabPageAction";
            this.TabPageAction.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageAction.Size = new System.Drawing.Size(1009, 709);
            this.TabPageAction.TabIndex = 0;
            this.TabPageAction.Text = "Action";
            this.TabPageAction.UseVisualStyleBackColor = true;
            // 
            // TabPageMap
            // 
            this.TabPageMap.Location = new System.Drawing.Point(4, 22);
            this.TabPageMap.Name = "TabPageMap";
            this.TabPageMap.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageMap.Size = new System.Drawing.Size(1009, 709);
            this.TabPageMap.TabIndex = 1;
            this.TabPageMap.Text = "Map";
            this.TabPageMap.UseVisualStyleBackColor = true;
            // 
            // TabPageAttributes
            // 
            this.TabPageAttributes.Location = new System.Drawing.Point(4, 22);
            this.TabPageAttributes.Name = "TabPageAttributes";
            this.TabPageAttributes.Size = new System.Drawing.Size(1009, 709);
            this.TabPageAttributes.TabIndex = 2;
            this.TabPageAttributes.Text = "Character Info";
            this.TabPageAttributes.UseVisualStyleBackColor = true;
            // 
            // TabPageInventory
            // 
            this.TabPageInventory.Location = new System.Drawing.Point(4, 22);
            this.TabPageInventory.Name = "TabPageInventory";
            this.TabPageInventory.Size = new System.Drawing.Size(1009, 709);
            this.TabPageInventory.TabIndex = 3;
            this.TabPageInventory.Text = "Inventory";
            this.TabPageInventory.UseVisualStyleBackColor = true;
            // 
            // TabPageMagic
            // 
            this.TabPageMagic.Location = new System.Drawing.Point(4, 22);
            this.TabPageMagic.Name = "TabPageMagic";
            this.TabPageMagic.Size = new System.Drawing.Size(1009, 709);
            this.TabPageMagic.TabIndex = 4;
            this.TabPageMagic.Text = "Magic";
            this.TabPageMagic.UseVisualStyleBackColor = true;
            // 
            // TabPageMenu
            // 
            this.TabPageMenu.BackColor = System.Drawing.Color.Transparent;
            this.TabPageMenu.Location = new System.Drawing.Point(4, 22);
            this.TabPageMenu.Name = "TabPageMenu";
            this.TabPageMenu.Size = new System.Drawing.Size(1009, 709);
            this.TabPageMenu.TabIndex = 5;
            this.TabPageMenu.Text = "Menu";
            this.TabPageMenu.UseVisualStyleBackColor = true;
            // 
            // Session
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 734);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Session";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RPG";
            this.tabControl1.ResumeLayout(false);
            this.TabPageMagic.ResumeLayout(false);
            this.TabPageMagic.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

    }
}