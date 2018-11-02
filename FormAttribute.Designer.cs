namespace RPG
{
    partial class FormAttribute
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
            this.btnUp1 = new System.Windows.Forms.Button();
            this.lblValStr = new System.Windows.Forms.Label();
            this.btnDn1 = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTitleAttributes = new System.Windows.Forms.Label();
            this.lblBonus1 = new System.Windows.Forms.Label();
            this.lblBonus2 = new System.Windows.Forms.Label();
            this.btnDn2 = new System.Windows.Forms.Button();
            this.lblValDex = new System.Windows.Forms.Label();
            this.btnUp2 = new System.Windows.Forms.Button();
            this.lblBonus4 = new System.Windows.Forms.Label();
            this.btnDn4 = new System.Windows.Forms.Button();
            this.lblValInt = new System.Windows.Forms.Label();
            this.btnUp4 = new System.Windows.Forms.Button();
            this.lblBonus3 = new System.Windows.Forms.Label();
            this.btnDn3 = new System.Windows.Forms.Button();
            this.lblValCon = new System.Windows.Forms.Label();
            this.btnUp3 = new System.Windows.Forms.Button();
            this.lblBonus6 = new System.Windows.Forms.Label();
            this.btnDn6 = new System.Windows.Forms.Button();
            this.lblValCha = new System.Windows.Forms.Label();
            this.btnUp6 = new System.Windows.Forms.Button();
            this.lblBonus5 = new System.Windows.Forms.Label();
            this.btnDn5 = new System.Windows.Forms.Button();
            this.lblValWis = new System.Windows.Forms.Label();
            this.btnUp5 = new System.Windows.Forms.Button();
            this.lblPointTotal = new System.Windows.Forms.Label();
            this.lblStatCha = new System.Windows.Forms.Label();
            this.lblStatWis = new System.Windows.Forms.Label();
            this.lblStatInt = new System.Windows.Forms.Label();
            this.lblStatCon = new System.Windows.Forms.Label();
            this.lblStatDex = new System.Windows.Forms.Label();
            this.lblStatStr = new System.Windows.Forms.Label();
            this.lblTitlePoints = new System.Windows.Forms.Label();
            this.lblTitleBonuses = new System.Windows.Forms.Label();
            this.btn_Done = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUp1
            // 
            this.btnUp1.Location = new System.Drawing.Point(189, 107);
            this.btnUp1.Name = "btnUp1";
            this.btnUp1.Size = new System.Drawing.Size(30, 23);
            this.btnUp1.TabIndex = 0;
            this.btnUp1.Text = "UP";
            this.btnUp1.UseVisualStyleBackColor = true;
            this.btnUp1.Click += new System.EventHandler(this.btnUp1_Click);
            // 
            // lblValStr
            // 
            this.lblValStr.AutoSize = true;
            this.lblValStr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValStr.Location = new System.Drawing.Point(238, 107);
            this.lblValStr.Name = "lblValStr";
            this.lblValStr.Size = new System.Drawing.Size(25, 13);
            this.lblValStr.TabIndex = 1;
            this.lblValStr.Text = "Val";
            this.lblValStr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDn1
            // 
            this.btnDn1.Location = new System.Drawing.Point(279, 107);
            this.btnDn1.Name = "btnDn1";
            this.btnDn1.Size = new System.Drawing.Size(30, 23);
            this.btnDn1.TabIndex = 2;
            this.btnDn1.Text = "Dn";
            this.btnDn1.UseVisualStyleBackColor = true;
            this.btnDn1.Click += new System.EventHandler(this.btnDn1_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(67, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(363, 13);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Add points to set your character\'s abilities. Click the titles to see description" +
                "s";
            // 
            // lblTitleAttributes
            // 
            this.lblTitleAttributes.AutoSize = true;
            this.lblTitleAttributes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleAttributes.Location = new System.Drawing.Point(67, 74);
            this.lblTitleAttributes.Name = "lblTitleAttributes";
            this.lblTitleAttributes.Size = new System.Drawing.Size(61, 13);
            this.lblTitleAttributes.TabIndex = 4;
            this.lblTitleAttributes.Text = "Attributes";
            this.lblTitleAttributes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBonus1
            // 
            this.lblBonus1.AutoSize = true;
            this.lblBonus1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBonus1.Location = new System.Drawing.Point(370, 107);
            this.lblBonus1.Name = "lblBonus1";
            this.lblBonus1.Size = new System.Drawing.Size(14, 13);
            this.lblBonus1.TabIndex = 5;
            this.lblBonus1.Text = "0";
            this.lblBonus1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBonus2
            // 
            this.lblBonus2.AutoSize = true;
            this.lblBonus2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBonus2.Location = new System.Drawing.Point(370, 152);
            this.lblBonus2.Name = "lblBonus2";
            this.lblBonus2.Size = new System.Drawing.Size(14, 13);
            this.lblBonus2.TabIndex = 9;
            this.lblBonus2.Text = "0";
            this.lblBonus2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDn2
            // 
            this.btnDn2.Location = new System.Drawing.Point(279, 152);
            this.btnDn2.Name = "btnDn2";
            this.btnDn2.Size = new System.Drawing.Size(30, 23);
            this.btnDn2.TabIndex = 8;
            this.btnDn2.Text = "Dn";
            this.btnDn2.UseVisualStyleBackColor = true;
            this.btnDn2.Click += new System.EventHandler(this.btnDn2_Click);
            // 
            // lblValDex
            // 
            this.lblValDex.AutoSize = true;
            this.lblValDex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValDex.Location = new System.Drawing.Point(238, 152);
            this.lblValDex.Name = "lblValDex";
            this.lblValDex.Size = new System.Drawing.Size(25, 13);
            this.lblValDex.TabIndex = 7;
            this.lblValDex.Text = "Val";
            this.lblValDex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnUp2
            // 
            this.btnUp2.Location = new System.Drawing.Point(189, 152);
            this.btnUp2.Name = "btnUp2";
            this.btnUp2.Size = new System.Drawing.Size(30, 23);
            this.btnUp2.TabIndex = 6;
            this.btnUp2.Text = "UP";
            this.btnUp2.UseVisualStyleBackColor = true;
            this.btnUp2.Click += new System.EventHandler(this.btnUp2_Click);
            // 
            // lblBonus4
            // 
            this.lblBonus4.AutoSize = true;
            this.lblBonus4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBonus4.Location = new System.Drawing.Point(370, 244);
            this.lblBonus4.Name = "lblBonus4";
            this.lblBonus4.Size = new System.Drawing.Size(14, 13);
            this.lblBonus4.TabIndex = 17;
            this.lblBonus4.Text = "0";
            this.lblBonus4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDn4
            // 
            this.btnDn4.Location = new System.Drawing.Point(279, 244);
            this.btnDn4.Name = "btnDn4";
            this.btnDn4.Size = new System.Drawing.Size(30, 23);
            this.btnDn4.TabIndex = 16;
            this.btnDn4.Text = "Dn";
            this.btnDn4.UseVisualStyleBackColor = true;
            this.btnDn4.Click += new System.EventHandler(this.btnDn4_Click);
            // 
            // lblValInt
            // 
            this.lblValInt.AutoSize = true;
            this.lblValInt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValInt.Location = new System.Drawing.Point(238, 244);
            this.lblValInt.Name = "lblValInt";
            this.lblValInt.Size = new System.Drawing.Size(25, 13);
            this.lblValInt.TabIndex = 15;
            this.lblValInt.Text = "Val";
            this.lblValInt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnUp4
            // 
            this.btnUp4.Location = new System.Drawing.Point(189, 244);
            this.btnUp4.Name = "btnUp4";
            this.btnUp4.Size = new System.Drawing.Size(30, 23);
            this.btnUp4.TabIndex = 14;
            this.btnUp4.Text = "UP";
            this.btnUp4.UseVisualStyleBackColor = true;
            this.btnUp4.Click += new System.EventHandler(this.btnUp4_Click);
            // 
            // lblBonus3
            // 
            this.lblBonus3.AutoSize = true;
            this.lblBonus3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBonus3.Location = new System.Drawing.Point(370, 199);
            this.lblBonus3.Name = "lblBonus3";
            this.lblBonus3.Size = new System.Drawing.Size(14, 13);
            this.lblBonus3.TabIndex = 13;
            this.lblBonus3.Text = "0";
            this.lblBonus3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDn3
            // 
            this.btnDn3.Location = new System.Drawing.Point(279, 199);
            this.btnDn3.Name = "btnDn3";
            this.btnDn3.Size = new System.Drawing.Size(30, 23);
            this.btnDn3.TabIndex = 12;
            this.btnDn3.Text = "Dn";
            this.btnDn3.UseVisualStyleBackColor = true;
            this.btnDn3.Click += new System.EventHandler(this.btnDn3_Click);
            // 
            // lblValCon
            // 
            this.lblValCon.AutoSize = true;
            this.lblValCon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValCon.Location = new System.Drawing.Point(238, 199);
            this.lblValCon.Name = "lblValCon";
            this.lblValCon.Size = new System.Drawing.Size(25, 13);
            this.lblValCon.TabIndex = 11;
            this.lblValCon.Text = "Val";
            this.lblValCon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnUp3
            // 
            this.btnUp3.Location = new System.Drawing.Point(189, 199);
            this.btnUp3.Name = "btnUp3";
            this.btnUp3.Size = new System.Drawing.Size(30, 23);
            this.btnUp3.TabIndex = 10;
            this.btnUp3.Text = "UP";
            this.btnUp3.UseVisualStyleBackColor = true;
            this.btnUp3.Click += new System.EventHandler(this.btnUp3_Click);
            // 
            // lblBonus6
            // 
            this.lblBonus6.AutoSize = true;
            this.lblBonus6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBonus6.Location = new System.Drawing.Point(370, 336);
            this.lblBonus6.Name = "lblBonus6";
            this.lblBonus6.Size = new System.Drawing.Size(14, 13);
            this.lblBonus6.TabIndex = 25;
            this.lblBonus6.Text = "0";
            this.lblBonus6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDn6
            // 
            this.btnDn6.Location = new System.Drawing.Point(279, 336);
            this.btnDn6.Name = "btnDn6";
            this.btnDn6.Size = new System.Drawing.Size(30, 23);
            this.btnDn6.TabIndex = 24;
            this.btnDn6.Text = "Dn";
            this.btnDn6.UseVisualStyleBackColor = true;
            this.btnDn6.Click += new System.EventHandler(this.btnDn6_Click);
            // 
            // lblValCha
            // 
            this.lblValCha.AutoSize = true;
            this.lblValCha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValCha.Location = new System.Drawing.Point(238, 336);
            this.lblValCha.Name = "lblValCha";
            this.lblValCha.Size = new System.Drawing.Size(25, 13);
            this.lblValCha.TabIndex = 23;
            this.lblValCha.Text = "Val";
            this.lblValCha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnUp6
            // 
            this.btnUp6.Location = new System.Drawing.Point(189, 336);
            this.btnUp6.Name = "btnUp6";
            this.btnUp6.Size = new System.Drawing.Size(30, 23);
            this.btnUp6.TabIndex = 22;
            this.btnUp6.Text = "UP";
            this.btnUp6.UseVisualStyleBackColor = true;
            this.btnUp6.Click += new System.EventHandler(this.btnUp6_Click);
            // 
            // lblBonus5
            // 
            this.lblBonus5.AutoSize = true;
            this.lblBonus5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBonus5.Location = new System.Drawing.Point(370, 291);
            this.lblBonus5.Name = "lblBonus5";
            this.lblBonus5.Size = new System.Drawing.Size(14, 13);
            this.lblBonus5.TabIndex = 21;
            this.lblBonus5.Text = "0";
            this.lblBonus5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDn5
            // 
            this.btnDn5.Location = new System.Drawing.Point(279, 291);
            this.btnDn5.Name = "btnDn5";
            this.btnDn5.Size = new System.Drawing.Size(30, 23);
            this.btnDn5.TabIndex = 20;
            this.btnDn5.Text = "Dn";
            this.btnDn5.UseVisualStyleBackColor = true;
            this.btnDn5.Click += new System.EventHandler(this.btnDn5_Click);
            // 
            // lblValWis
            // 
            this.lblValWis.AutoSize = true;
            this.lblValWis.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValWis.Location = new System.Drawing.Point(238, 291);
            this.lblValWis.Name = "lblValWis";
            this.lblValWis.Size = new System.Drawing.Size(25, 13);
            this.lblValWis.TabIndex = 19;
            this.lblValWis.Text = "Val";
            this.lblValWis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnUp5
            // 
            this.btnUp5.Location = new System.Drawing.Point(189, 291);
            this.btnUp5.Name = "btnUp5";
            this.btnUp5.Size = new System.Drawing.Size(30, 23);
            this.btnUp5.TabIndex = 18;
            this.btnUp5.Text = "UP";
            this.btnUp5.UseVisualStyleBackColor = true;
            this.btnUp5.Click += new System.EventHandler(this.btnUp5_Click);
            // 
            // lblPointTotal
            // 
            this.lblPointTotal.AutoSize = true;
            this.lblPointTotal.Location = new System.Drawing.Point(189, 395);
            this.lblPointTotal.Name = "lblPointTotal";
            this.lblPointTotal.Size = new System.Drawing.Size(90, 13);
            this.lblPointTotal.TabIndex = 26;
            this.lblPointTotal.Text = "Total Points Left: ";
            this.lblPointTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatCha
            // 
            this.lblStatCha.AutoSize = true;
            this.lblStatCha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatCha.Location = new System.Drawing.Point(67, 336);
            this.lblStatCha.Name = "lblStatCha";
            this.lblStatCha.Size = new System.Drawing.Size(58, 13);
            this.lblStatCha.TabIndex = 32;
            this.lblStatCha.Text = "Charisma";
            this.lblStatCha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatCha.Click += new System.EventHandler(this.lblStatCha_Click);
            // 
            // lblStatWis
            // 
            this.lblStatWis.AutoSize = true;
            this.lblStatWis.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatWis.Location = new System.Drawing.Point(67, 291);
            this.lblStatWis.Name = "lblStatWis";
            this.lblStatWis.Size = new System.Drawing.Size(51, 13);
            this.lblStatWis.TabIndex = 31;
            this.lblStatWis.Text = "Wisdom";
            this.lblStatWis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatWis.Click += new System.EventHandler(this.lblStatWis_Click);
            // 
            // lblStatInt
            // 
            this.lblStatInt.AutoSize = true;
            this.lblStatInt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatInt.Location = new System.Drawing.Point(67, 244);
            this.lblStatInt.Name = "lblStatInt";
            this.lblStatInt.Size = new System.Drawing.Size(73, 13);
            this.lblStatInt.TabIndex = 30;
            this.lblStatInt.Text = "Intelligence";
            this.lblStatInt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatInt.Click += new System.EventHandler(this.lblStatInt_Click);
            // 
            // lblStatCon
            // 
            this.lblStatCon.AutoSize = true;
            this.lblStatCon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatCon.Location = new System.Drawing.Point(67, 199);
            this.lblStatCon.Name = "lblStatCon";
            this.lblStatCon.Size = new System.Drawing.Size(74, 13);
            this.lblStatCon.TabIndex = 29;
            this.lblStatCon.Text = "Constitution";
            this.lblStatCon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatCon.Click += new System.EventHandler(this.lblStatCon_Click);
            // 
            // lblStatDex
            // 
            this.lblStatDex.AutoSize = true;
            this.lblStatDex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatDex.Location = new System.Drawing.Point(67, 152);
            this.lblStatDex.Name = "lblStatDex";
            this.lblStatDex.Size = new System.Drawing.Size(57, 13);
            this.lblStatDex.TabIndex = 28;
            this.lblStatDex.Text = "Dexterity";
            this.lblStatDex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatDex.Click += new System.EventHandler(this.lblStatDex_Click);
            // 
            // lblStatStr
            // 
            this.lblStatStr.AutoSize = true;
            this.lblStatStr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatStr.Location = new System.Drawing.Point(67, 107);
            this.lblStatStr.Name = "lblStatStr";
            this.lblStatStr.Size = new System.Drawing.Size(55, 13);
            this.lblStatStr.TabIndex = 27;
            this.lblStatStr.Text = "Strength";
            this.lblStatStr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatStr.Click += new System.EventHandler(this.lblStatStr_Click);
            // 
            // lblTitlePoints
            // 
            this.lblTitlePoints.AutoSize = true;
            this.lblTitlePoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitlePoints.Location = new System.Drawing.Point(231, 74);
            this.lblTitlePoints.Name = "lblTitlePoints";
            this.lblTitlePoints.Size = new System.Drawing.Size(42, 13);
            this.lblTitlePoints.TabIndex = 33;
            this.lblTitlePoints.Text = "Points";
            this.lblTitlePoints.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleBonuses
            // 
            this.lblTitleBonuses.AutoSize = true;
            this.lblTitleBonuses.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleBonuses.Location = new System.Drawing.Point(348, 74);
            this.lblTitleBonuses.Name = "lblTitleBonuses";
            this.lblTitleBonuses.Size = new System.Drawing.Size(55, 13);
            this.lblTitleBonuses.TabIndex = 34;
            this.lblTitleBonuses.Text = "Bonuses";
            this.lblTitleBonuses.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Done
            // 
            this.btn_Done.Location = new System.Drawing.Point(405, 431);
            this.btn_Done.Name = "btn_Done";
            this.btn_Done.Size = new System.Drawing.Size(75, 23);
            this.btn_Done.TabIndex = 35;
            this.btn_Done.Text = "Done";
            this.btn_Done.UseVisualStyleBackColor = true;
            this.btn_Done.Click += new System.EventHandler(this.btn_Done_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(12, 431);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 36;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormAttribute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 466);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btn_Done);
            this.Controls.Add(this.lblTitleBonuses);
            this.Controls.Add(this.lblTitlePoints);
            this.Controls.Add(this.lblStatCha);
            this.Controls.Add(this.lblStatWis);
            this.Controls.Add(this.lblStatInt);
            this.Controls.Add(this.lblStatCon);
            this.Controls.Add(this.lblStatDex);
            this.Controls.Add(this.lblStatStr);
            this.Controls.Add(this.lblPointTotal);
            this.Controls.Add(this.lblBonus6);
            this.Controls.Add(this.btnDn6);
            this.Controls.Add(this.lblValCha);
            this.Controls.Add(this.btnUp6);
            this.Controls.Add(this.lblBonus5);
            this.Controls.Add(this.btnDn5);
            this.Controls.Add(this.lblValWis);
            this.Controls.Add(this.btnUp5);
            this.Controls.Add(this.lblBonus4);
            this.Controls.Add(this.btnDn4);
            this.Controls.Add(this.lblValInt);
            this.Controls.Add(this.btnUp4);
            this.Controls.Add(this.lblBonus3);
            this.Controls.Add(this.btnDn3);
            this.Controls.Add(this.lblValCon);
            this.Controls.Add(this.btnUp3);
            this.Controls.Add(this.lblBonus2);
            this.Controls.Add(this.btnDn2);
            this.Controls.Add(this.lblValDex);
            this.Controls.Add(this.btnUp2);
            this.Controls.Add(this.lblBonus1);
            this.Controls.Add(this.lblTitleAttributes);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnDn1);
            this.Controls.Add(this.lblValStr);
            this.Controls.Add(this.btnUp1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAttribute";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RPG | Attributes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUp1;
        private System.Windows.Forms.Label lblValStr;
        private System.Windows.Forms.Button btnDn1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTitleAttributes;
        private System.Windows.Forms.Label lblBonus1;
        private System.Windows.Forms.Label lblBonus2;
        private System.Windows.Forms.Button btnDn2;
        private System.Windows.Forms.Label lblValDex;
        private System.Windows.Forms.Button btnUp2;
        private System.Windows.Forms.Label lblBonus4;
        private System.Windows.Forms.Button btnDn4;
        private System.Windows.Forms.Label lblValInt;
        private System.Windows.Forms.Button btnUp4;
        private System.Windows.Forms.Label lblBonus3;
        private System.Windows.Forms.Button btnDn3;
        private System.Windows.Forms.Label lblValCon;
        private System.Windows.Forms.Button btnUp3;
        private System.Windows.Forms.Label lblBonus6;
        private System.Windows.Forms.Button btnDn6;
        private System.Windows.Forms.Label lblValCha;
        private System.Windows.Forms.Button btnUp6;
        private System.Windows.Forms.Label lblBonus5;
        private System.Windows.Forms.Button btnDn5;
        private System.Windows.Forms.Label lblValWis;
        private System.Windows.Forms.Button btnUp5;
        private System.Windows.Forms.Label lblPointTotal;
        private System.Windows.Forms.Label lblStatCha;
        private System.Windows.Forms.Label lblStatWis;
        private System.Windows.Forms.Label lblStatInt;
        private System.Windows.Forms.Label lblStatCon;
        private System.Windows.Forms.Label lblStatDex;
        private System.Windows.Forms.Label lblStatStr;
        private System.Windows.Forms.Label lblTitlePoints;
        private System.Windows.Forms.Label lblTitleBonuses;
        private System.Windows.Forms.Button btn_Done;
        private System.Windows.Forms.Button btnCancel;
    }
}

