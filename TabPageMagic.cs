using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace RPG
{
    public class TabPageMagic: TabPage
    {
        #region Class Declarations
        private Label label1;
        private DataGridView dataGridView1;
        private Button btn_Remove;
        private Button btn_Add;
        private DataGridView dataGridView2;
        private Actor thisActor;
        #endregion

        #region Constructor and Setup Methods
        public TabPageMagic()
        {
            this.Location = new System.Drawing.Point(4, 22);
            this.Name = "TabPageMagic";
            this.Size = new System.Drawing.Size(1009, 709);
            this.TabIndex = 4;
            this.Text = "Magic";
            this.UseVisualStyleBackColor = true;

            InitializeLocalComponents();
        }
        private void InitializeLocalComponents()
        {
            this.label1 = new Label();
            this.dataGridView1 = new DataGridView();
            this.btn_Remove = new Button();
            this.btn_Add = new Button();
            this.dataGridView2 = new DataGridView();

            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(422, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Spellbook";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Size = new System.Drawing.Size(560, 425);
            this.dataGridView1.Location = new System.Drawing.Point((this.Width - this.dataGridView1.Width) / 2, 80);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dataGridView_DataBindingComplete);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(223, 520);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(123, 23);
            this.btn_Add.TabIndex = 2;
            this.btn_Add.Text = "Add To Quick Spells";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new EventHandler(btn_Add_Click);
            // 
            // btn_Remove
            // 
            this.btn_Remove.Location = new System.Drawing.Point(641, 520);
            this.btn_Remove.Name = "btn_Remove";
            this.btn_Remove.Size = new System.Drawing.Size(144, 23);
            this.btn_Remove.TabIndex = 3;
            this.btn_Remove.Text = "Remove from Quick Spells";
            this.btn_Remove.UseVisualStyleBackColor = true;
            this.btn_Remove.Click += new EventHandler(btn_Remove_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Size = new System.Drawing.Size(562, 124);
            this.dataGridView2.Location = new System.Drawing.Point(223, 559);
            this.dataGridView2.Name = "dataGridView1";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.TabIndex = 4;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dataGridView_DataBindingComplete);

            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.btn_Remove);
            this.Controls.Add(this.dataGridView2);
        }
        #endregion

        #region Load Methods
        public void LoadActor(Actor a)
        {
            this.thisActor = a;
            LoadSpellBookIntoGrid(this.dataGridView1, a.SpellBook);
            LoadSpellBookIntoGrid(this.dataGridView2, a.QuickSpellBook);
        }
        private void LoadSpellBookIntoGrid(DataGridView grid, RPGSpellBook book)
        {
            DataTable data = SetupSpellDataTable();

            for (int i = 0; i < book.SpellCountMax; i++)
            {
                RPGSpell s = book.GetSpellAtIndex(i);

                // if we don't have a spell, just add a blank row
                if (s == null) 
                { 
                    data.Rows.Add(data.NewRow()); 
                    continue;
                }

                AddSpellToDataTable(data, s);
            }

            grid.DataSource = data;
        }
        private DataTable SetupSpellDataTable()
        {
            DataTable data = new DataTable();

            data.Columns.Add(new DataColumn("Realm"));
            data.Columns.Add(new DataColumn("Range"));
            data.Columns.Add(new DataColumn("Type"));
            data.Columns.Add(new DataColumn("Effect"));
            data.Columns.Add(new DataColumn("Power"));

            DataColumn colCost = new DataColumn("Cost");
            colCost.DataType = typeof(Int32);
            data.Columns.Add(colCost);

            data.Columns.Add(new DataColumn("Duration"));

            return data;
        }
        private void AddSpellToDataTable(DataTable data, RPGSpell s)
        {
            #region Build Row Data
            string realm = Enum.GetName(typeof(SpellRealm), s.Realm);
            string range = Enum.GetName(typeof(RPGEffect.EffectRange), s.Effect.Range);

            bool bIsBuff = s.Effect.EffectIsABuff;
            string isBuff = "Attack";
            string effect = s.GetShortEffectDescription();
            string pwr = "" + s.Effect.MinPower + " - " + s.Effect.MaxPower;
            string cost = s.Cost.ToString();

            string duration = s.Effect.GetDurationAsString();
            if (s.Effect.DurationEffectType == RPGEffect.DurationType.Permanent)
            {
                duration = "Once";
            }

            #endregion

            #region Assign vars to new row
            DataRow newRow = data.NewRow();
            newRow["Realm"] = realm;
            newRow["type"] = isBuff;
            newRow["Range"] = range;
            newRow["Effect"] = effect;
            newRow["Power"] = pwr;
            newRow["Cost"] = cost;
            newRow["Duration"] = duration;
            #endregion

            data.Rows.Add(newRow);
        }
        void dataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // adjust column widths
            (sender as DataGridView).Columns["Realm"].Width = 50;
            (sender as DataGridView).Columns["Type"].Width = 50;
            (sender as DataGridView).Columns["Cost"].Width = 50;
            (sender as DataGridView).Columns["Power"].Width = 50;
        }
        #endregion

        #region UI Events
        void btn_Add_Click(object sender, EventArgs e)
        {
            AddSelectedSpellsToQuickSpellBook();
        }
        void btn_Remove_Click(object sender, EventArgs e)
        {
            RemoveSelectedQuickSpellsFromQuickBook();
        }
        #endregion

        #region Private Functions
        private void AddSelectedSpellsToQuickSpellBook()
        {
            // if there are no spells selected, notify and return
            if (dataGridView1.SelectedRows.Count < 1)
            {
                string msg = "Select spells from your book to add them to the quick slots.";
                MessageBox.Show(msg, "No spells selected", MessageBoxButtons.OK);
                return;
            }

            // for each spell - add if selected
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // skip if not selected
                if(!dataGridView1.Rows[i].Selected){continue;}

                // skip if row is blank
                DataGridViewRow thisRow = dataGridView1.Rows[i];
                if (thisRow.Cells[0].Value.ToString().Length < 1) { continue; }

                // use index to fing correct spell
                RPGSpell thisSpell = thisActor.SpellBook.GetSpellAtIndex(i);

                // find an open slot
                bool worked = thisActor.QuickSpellBook.AddSpell(thisSpell);

                if(!worked)
                {
                    // nofity and return
                    string msg2 = "Spell could not be added.";
                    MessageBox.Show(msg2, "Add failed", MessageBoxButtons.OK);
                }
            }

            // reload the quick spell book grid
            LoadSpellBookIntoGrid(this.dataGridView2, thisActor.QuickSpellBook);
        }
        private void RemoveSelectedQuickSpellsFromQuickBook()
        {
            // if there are no spells selected, notify and return
            if (dataGridView2.SelectedRows.Count < 1)
            {
                string msg = "Select spells from your quick slots to remove them.";
                MessageBox.Show(msg, "No spells selected", MessageBoxButtons.OK);
                return;
            }

            // for each selected quick spell
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                // skip if not selected
                if (!dataGridView2.Rows[i].Selected) { continue; }

                // skip if row is blank
                DataGridViewRow thisRow = dataGridView2.Rows[i];
                if (thisRow.Cells[0].Value.ToString().Length < 1) { continue; }

                // use index to fing correct spell
                RPGSpell thisSpell = thisActor.QuickSpellBook.GetSpellAtIndex(i);

                // remove it from the actor's quick book
                thisActor.QuickSpellBook.RemoveSpell(thisSpell);

            } // end for loop

            // reload the quick spell book grid
            LoadSpellBookIntoGrid(this.dataGridView2, thisActor.QuickSpellBook);
        }
        #endregion
    }
}
