using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Collections;

namespace RPG
{
    public class TabPageInventory: TabPage
    {
        #region Declarations
        private Label lblName;
        private Label lblBody;
        private Label lblPack;
        private Label lblGround;
        private Label lblQuick;

        private DataGridView dgvBody;
        private DataGridView dgvQuick;
        private DataGridView dgvPack;
        private DataGridView dgvGround;

        private DataTable dtBodyItems;
        private DataTable dtQuickItems;
        private DataTable dtPackItems;
        private DataTable dtGroundItems;

        private DataGridViewTextBoxColumn colBodyItemPosition;
        private DataGridViewTextBoxColumn colBodyItemName;
        private DataGridViewTextBoxColumn colBodyItemType;
        private DataGridViewTextBoxColumn colBodyItemDescription;

        private DataGridViewTextBoxColumn colQuickItemName;
        private DataGridViewTextBoxColumn colQuickItemType;
        private DataGridViewTextBoxColumn colQuickItemDescription;

        private DataGridViewTextBoxColumn colPackItemName;
        private DataGridViewTextBoxColumn colPackItemType;
        private DataGridViewTextBoxColumn colPackItemDescription;

        private DataGridViewTextBoxColumn colGroundItemName;
        private DataGridViewTextBoxColumn colGroundItemType;
        private DataGridViewTextBoxColumn colGroundItemDescription;

        private Button btnEquip;
        private Button btnUnEquip;
        private Button btnItemDetails;
        private Button btnDrop;
        private Button btnPickup;

        private Button btnQuickAdd;
        private Button btnQuickRemove;

        private Actor thisActor;
        private ArrayList groundItems;
        #endregion

        #region Constructor and Setup
        public TabPageInventory()
        {
            lblName = SetupLabel(14F, 330, 25, "Inventory");
            lblName.Width = 310;

            lblBody = SetupLabel(10F, 1, 80, "Body Items");            
            lblPack = SetupLabel(10F, 550, 80, "Pack Items (" + Inventory.PACK_SIZE + ")");
            lblGround = SetupLabel(10F, 1, 390, "Ground Items");
            lblQuick = SetupLabel(10F, 550, 390, "Quick Items (" + Inventory.QUICK_SIZE + ")");

            SetupBodyGrid();
            SetupQuickGrid();
            SetupPackGrid();
            SetupGroundGrid();

            btnEquip = SetupButton("Equip", 730, 80);
            btnEquip.Click += new EventHandler(btnEquip_Click);

            btnUnEquip = SetupButton("Remove", 320, 80);
            btnUnEquip.Click += new EventHandler(btnUnEquip_Click);

            btnItemDetails = SetupButton("Item \nDetails", 450, 340);
            btnItemDetails.Height = 40;
            btnItemDetails.Click += new EventHandler(btnItemDetails_Click);

            btnDrop = SetupButton("Drop", 800, 80);
            btnDrop.Click += new EventHandler(btnDrop_Click);

            btnPickup = SetupButton("Pick Up", 320, 390);
            btnPickup.Click += new EventHandler(btnPickup_Click);

            btnQuickAdd = SetupButton("Add Q", 870, 80);
            btnQuickAdd.Click += new EventHandler(btnQuickAdd_Click);

            btnQuickRemove = SetupButton("Remove Q", 850, 390);
            btnQuickRemove.Click += new EventHandler(btnQuickRemove_Click);
            btnQuickRemove.Width = 80;

            this.Controls.Add(lblName);
            this.Controls.Add(lblBody);
            this.Controls.Add(dgvBody);
            this.Controls.Add(lblQuick);
            this.Controls.Add(dgvQuick);
            this.Controls.Add(lblPack);
            this.Controls.Add(dgvPack);
            this.Controls.Add(lblGround);
            this.Controls.Add(dgvGround);

            this.Controls.Add(btnEquip);
            this.Controls.Add(btnUnEquip);
            this.Controls.Add(btnItemDetails);
            this.Controls.Add(btnDrop);
            this.Controls.Add(btnPickup);
            this.Controls.Add(btnQuickAdd);
            this.Controls.Add(btnQuickRemove);
        }

        private Label SetupLabel(float size, int X, int Y, string title)
        {
            Label lbl = new Label();
            lbl.Font = new System.Drawing.Font(
                                "Microsoft Sans Serif", size,
                                System.Drawing.FontStyle.Bold,
                                System.Drawing.GraphicsUnit.Point,
                                ((byte)(0)));
            lbl.Location = new Point(X, Y);
            lbl.Name = title;
            lbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            lbl.Size = new System.Drawing.Size(175, 25);
            lbl.Text = title;
            lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            return lbl;
        }
        private Button SetupButton(string Text, int X, int Y)
        {
            Button b = new Button();
            b.Width = 60;
            b.Text = Text;
            b.Location = new Point(X, Y);
            return b;
        }
        private void SetupBodyGrid()
        {
            dgvBody = new DataGridView();
            dgvBody.Name = "BodyGrid";
                colBodyItemPosition = new DataGridViewTextBoxColumn();
            SetupColumn(colBodyItemPosition, "Slot", 100);
                colBodyItemName = new DataGridViewTextBoxColumn();
            SetupColumn(colBodyItemName, "Name", 75);
                colBodyItemType = new DataGridViewTextBoxColumn();
            SetupColumn(colBodyItemType, "Type", 75);
                colBodyItemDescription = new DataGridViewTextBoxColumn();
            SetupColumn(colBodyItemDescription, "Description", 150);

            SetupDataGridView(dgvBody, new Point(20, 120));
            dgvBody.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] 
            {
                colBodyItemPosition,
                colBodyItemName,
                colBodyItemType,
                colBodyItemDescription
            });
            dgvBody.CellClick += new DataGridViewCellEventHandler(dgvBody_CellClick);
            dgvBody.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dgvBody_DataBindingComplete);
        }
        private void SetupPackGrid()
        {
            dgvPack = new DataGridView();
            colPackItemName = new DataGridViewTextBoxColumn();
            SetupColumn(colPackItemName, "Name", 100);
            colPackItemType = new DataGridViewTextBoxColumn();
            SetupColumn(colPackItemType, "Type", 75);
            colPackItemDescription = new DataGridViewTextBoxColumn();
            SetupColumn(colPackItemDescription, "Description", 250);

            SetupDataGridView(dgvPack, new Point(570, 120));
            dgvPack.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] 
            {
                colPackItemName,
                colPackItemType,
                colPackItemDescription
            });
            dgvPack.CellClick += new DataGridViewCellEventHandler(dgvPack_CellClick);
            dgvPack.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dgvPack_DataBindingComplete);
        }
        private void SetupGroundGrid()
        {
            dgvGround = new DataGridView();
            dgvGround.Name = "GroundGrid";
            colGroundItemName = new DataGridViewTextBoxColumn();
            SetupColumn(colGroundItemName, "Name", 100);
            colGroundItemType = new DataGridViewTextBoxColumn();
            SetupColumn(colGroundItemType, "Type", 75);
            colGroundItemDescription = new DataGridViewTextBoxColumn();
            SetupColumn(colGroundItemDescription, "Description", 250);

            groundItems = new ArrayList();

            SetupDataGridView(dgvGround, new Point(20, 420));
            dgvGround.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] 
            {
                colGroundItemName,
                colGroundItemType,
                colGroundItemDescription
            });
            dgvGround.CellClick += new DataGridViewCellEventHandler(dgvGround_CellClick);
            dgvGround.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dgvGround_DataBindingComplete);
        }
        private void SetupQuickGrid()
        {
            dgvQuick = new DataGridView();
            colQuickItemName = new DataGridViewTextBoxColumn();
            SetupColumn(colQuickItemName, "Name", 100);
            colQuickItemType = new DataGridViewTextBoxColumn();
            SetupColumn(colQuickItemType, "Type", 75);
            colQuickItemDescription = new DataGridViewTextBoxColumn();
            SetupColumn(colQuickItemDescription, "Description", 250);

            SetupDataGridView(dgvQuick, new Point(570, 420));
            dgvQuick.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] 
            {
                colQuickItemName,
                colQuickItemType,
                colQuickItemDescription
            });
            dgvQuick.CellClick += new DataGridViewCellEventHandler(dgvQuick_CellClick);
            dgvQuick.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dgvQuick_DataBindingComplete);
        }

        void dgvBody_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // color anything, or change the text of anything?
            ClearRowSelection(dgvBody);
        }
        void dgvQuick_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // color anything, or change the text of anything?
            ClearRowSelection(dgvQuick);
        }
        void dgvPack_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // color anything, or change the text of anything?
            ClearRowSelection(dgvPack);
        }
        void dgvGround_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // color anything, or change the text of anything?
            ClearRowSelection(dgvGround);
        }

        private void SetupDataGridView(DataGridView dgv, Point location)
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.BackgroundColor = System.Drawing.SystemColors.Window;
            dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            dgv.GridColor = System.Drawing.SystemColors.Window;
            dgv.Location = location;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dgv.RowHeadersWidth = 4;
            dgv.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgv.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgv.ShowCellErrors = false;
            dgv.ShowCellToolTips = false;
            dgv.ShowEditingIcon = false;
            dgv.ShowRowErrors = false;
            dgv.Size = new System.Drawing.Size(380, 250);
        }
        private void SetupColumn(DataGridViewTextBoxColumn col, string Name, int Width)
        {
            col.DataPropertyName = Name;
            col.HeaderText = Name;
            col.Name = Name;
            col.ReadOnly = true;
            col.Width = Width;
        }
        #endregion

        #region Event methods
        void dgvBody_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearQuickSelections();
            ClearPackSelections();
            ClearGroundSelections();
        }
        void dgvQuick_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearBodySelections();
            ClearPackSelections();
            ClearGroundSelections();
        }
        void dgvPack_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearBodySelections();
            ClearQuickSelections();
            ClearGroundSelections();
        }
        void dgvGround_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearBodySelections();
            ClearPackSelections();
            ClearQuickSelections();
        }

        void btnEquip_Click(object sender, EventArgs e)
        {
            // move selected pack item to body, at correct slot
            // if already equipped, then switch locations.
            RPGObject item = GetSelectedItemFromPack();
            // now, check if body already has an item at that slot
            if (item == null)
            {
                MessageBox.Show("No item selected from pack to equip.");
                return;
            }
            else if (item.isOfType(typeof(RPGItem)) == false)
            {
                MessageBox.Show("This item type cannot be equipped.");
                return;
            }
            else
            {
                // check if destination slot is already filled
                if (thisActor.inventory.GetBodyItem(((RPGItem)item).Slot) != null)
                {
                    // find currently equipped item and remove it
                    RPGItem currentlyEquippedItem
                        = thisActor.inventory.GetBodyItem(((RPGItem)item).Slot);

                    if (AddItemToPack(currentlyEquippedItem))
                    {
                        RemoveItemFromBody(currentlyEquippedItem);
                    }
                    else
                    {
                        MessageBox.Show("Unable to add equipped item back into pack.");
                        return;
                    }
                } // end it slot is already filled
                else
                {
                    // add new item to body
                    if (AddItemToBody(((RPGItem)item), ((RPGItem)item).Slot))
                    {
                        RemoveItemFromPack(item);
                    }
                    else
                    {
                        MessageBox.Show("Unable to equip item.");
                    }
                } // end else - destination slot empty
            } // end else - item ok
        }
        void btnUnEquip_Click(object sender, EventArgs e)
        {
            // move selected body item to pack
            RPGItem item = GetSelectedItemFromBody();
            if (item == null)
            {
                MessageBox.Show("No item selected from body to unequip.");
            }
            else if (AddItemToPack(item))
            {
                RemoveItemFromBody(item);
            }
            else
            {
                MessageBox.Show("Unable to unequip item from body.");
            }
            
        }
        void btnPickup_Click(object sender, EventArgs e)
        {
            // move selected ground item to pack
            RPGObject item = GetSelectedItemFromGround();
            if (item == null)
            {
                MessageBox.Show("No item selected from ground.");
            }
            else if (AddItemToPack(item))
            {
                RemoveItemFromGround(item);
            }
            else
            {
                MessageBox.Show("unable to pick up item from ground.");
            }            
        }
        void btnDrop_Click(object sender, EventArgs e)
        {
            // move selected pack item to ground
            RPGObject item = GetSelectedItemFromPack();
            if (item == null)
            {
                MessageBox.Show("No item selected from the pack to drop.");
            }
            else if (AddItemToGround(item))
            {
                RemoveItemFromPack(item);
            }
            else
            {
                MessageBox.Show("Unable to drop item to ground.");
            }
            
        }
        void btnQuickRemove_Click(object sender, EventArgs e)
        {
            // move selected ground item to pack
            RPGObject item = GetSelectedItemFromQuick();
            if (item == null)
            {
                MessageBox.Show("No item selected from pack ");
            }
            else if(AddItemToPack(item))
            {
                RemoveItemFromQuick(item);
            }
            else
            {
                MessageBox.Show("Could not move item from quick to pack.");
            }
        }
        void btnQuickAdd_Click(object sender, EventArgs e)
        {
            // move selected pack item to ground
            RPGObject item = GetSelectedItemFromPack();
            if (item == null)
            {
                MessageBox.Show("No item selected from pack to add to quick items.");
            }
            else if (AddItemToQuick(item))
            {
                RemoveItemFromPack(item);
            }
            else
            {
                MessageBox.Show("Could not add item to Quick Items");
            }
        }

        void btnItemDetails_Click(object sender, EventArgs e)
        {
            // pop selected item details in new window
            RPGObject selItem = GetSelectedItemFromGround();
            if (selItem == null)
            {
                selItem = GetSelectedItemFromPack();
            }
            if (selItem == null)
            {
                selItem = GetSelectedItemFromBody();
            }
            if (selItem == null)
            {
                selItem = GetSelectedItemFromQuick();
            }

            if (selItem != null)
            {
                FormItemDetails fid = new FormItemDetails(selItem);
                fid.Show();
            }
        }
        #endregion

        #region Public methods
        public void LoadActor(Actor a)
        {
            this.thisActor = a;
            lblName.Text = a.Name + "'s Inventory";
            LoadBodyGrid(a);
            LoadPackGrid(a);
            LoadQuickGrid(a);
            LoadGroundGrid(a.Location);
        }
        public void SetGroundItemsToActionPanel()
        {
            // get the items from the ground grid, and create a new item 
            if (groundItems != null &&
                groundItems.Count > 0)
            {
                // create a new item drop object
                RPGDrop newDrop = new RPGDrop();
                int X = thisActor.Location.X;
                int Y = thisActor.Location.Y + 40; // +40 to look like ground.
                newDrop.Location = new Point(X, Y);

                for (int i = groundItems.Count - 1; i >= 0; i--)
                {
                    // get each item from the ground collection
                    RPGObject obj = groundItems[i] as RPGObject;

                    // set its location to the current actor
                    obj.Location = newDrop.Location;

                    // add it to the drop item
                    newDrop.AddItem(obj);

                    // and remove this item from the groundItems collection
                    groundItems.Remove(obj);

                }
                // add the item to the actionPanel's items.
                Session.thisSession.thisArea.AddObject(newDrop);

            }
        }
        #endregion

        #region Private methods
        private RPGItem GetSelectedItemFromBody()
        {
            if (dgvBody.SelectedRows.Count == 0) { return null; }
            string selDesc = dgvBody.SelectedRows[0].Cells[3].Value.ToString();

            for (int i = 0; i < Enum.GetValues(typeof(Inventory.BodySlot)).Length; i++)
            {
                RPGItem item = thisActor.inventory.GetBodyItem(i);
                if (item != null && item.Description == selDesc)
                {
                    return item;
                }
            }

            return null;
        }
        private RPGObject GetSelectedItemFromQuick()
        {
            if (dgvQuick.SelectedRows.Count != 1) { return null; }

            string selDesc = dgvQuick.SelectedRows[0].Cells[2].Value.ToString();
            for (int i = 0; i < Inventory.QUICK_SIZE; i++)
            {
                RPGObject item = thisActor.inventory.GetQuickItem(i);
                if (item != null && (item as RPGItem).Description == selDesc)
                {
                    return item;
                }
            }

            return null;
        }
        private RPGObject GetSelectedItemFromPack()
        {
            if (dgvPack.SelectedRows.Count != 1) { return null; }

            string selDesc = dgvPack.SelectedRows[0].Cells[2].Value.ToString();
            for (int i = 0; i < Inventory.PACK_SIZE; i++)
            {
                RPGObject item = thisActor.inventory.GetPackItem(i);
                if (item != null && (item as RPGItem).Description == selDesc)
                {
                    return item;
                }
            }

            return null;
        }
        private RPGObject GetSelectedItemFromGround()
        {
            if (dgvGround.SelectedRows.Count == 0) { return null; }
            string selDesc = dgvGround.SelectedRows[0].Cells[2].Value.ToString();
            foreach (RPGObject item in groundItems)
            {
                if (item != null 
                    && item.Description == selDesc)
                {
                    return item;
                }
            }
            return null;
        }

        private bool AddItemToBody(RPGItem item, Inventory.BodySlot slot)
        {
            bool result = false;
            if (item != null && thisActor.inventory.AddBodyItem(item))
            {
                result = true;
                LoadBodyGrid(thisActor);
            }
            return result;
        }
        private bool AddItemToQuick(RPGObject item)
        {
            bool result = false;
            if (item != null && item.isOfType(typeof(RPGItem)))
            {
                result = thisActor.inventory.AddQuickItem(item as RPGItem);
                LoadQuickGrid(thisActor);
            }
            return result;
        }
        private bool AddItemToPack(RPGObject item)
        {
            bool result = false;
            if (item != null && thisActor.inventory.AddItem(item))
            {
                LoadPackGrid(thisActor);
                result = true;
            }
            return result;
        }
        private bool AddItemToGround(RPGObject item)
        {
            LoadGroundRow(item);
            // assumption: we can always add an item to the ground
            return true; 
        }
        private void RemoveItemFromBody(RPGItem item)
        {
            if(item == null){return;}

            if (thisActor.inventory.GetBodyItem(item.Slot) == item)
            {
                thisActor.inventory.RemoveBodyItem(item.Slot);
            }
            else
            {
                // item was not on the body for removal...
            }

            LoadBodyGrid(thisActor);
        }
        private void RemoveItemFromQuick(RPGObject item)
        {
            // since the item can be anywhere, check all of them.

            for (int i = 0; i < Inventory.QUICK_SIZE; i++)
            {
                if (thisActor.inventory.GetQuickItem(i) == item)
                {
                    thisActor.inventory.RemoveQuickItem(i);
                    break;
                }
            }

            LoadQuickGrid(thisActor);
        }
        private void RemoveItemFromPack(RPGObject item)
        {
            // since the item can be anywhere, check all of them.

            for (int i = 0; i < Inventory.PACK_SIZE; i++)
            {
                if (thisActor.inventory.GetPackItem(i) == item)
                {
                    thisActor.inventory.RemovePackItem(i);
                    break;
                }
            }

            LoadPackGrid(thisActor);
        }
        private void RemoveItemFromGround(RPGObject item)
        {
            // takes care of the data too
            RemoveItemFromGrid(dgvGround, item);
        }
        private void RemoveItemFromGrid(DataGridView dgv, RPGObject item)
        {
            if (item == null) { return; }
            int IndexOfDescription = 2;
            if(dgv.Name == "BodyGrid")
            {
                IndexOfDescription += 1;
            }
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                string desc = dgv.Rows[i].Cells[IndexOfDescription].Value.ToString();
                if (desc == item.Description)
                {
                    if (dgv.Name == "BodyGrid")
                    {
                        dgv.Rows[i].Cells[1].Value = "";
                        dgv.Rows[i].Cells[2].Value = "";
                        dgv.Rows[i].Cells[3].Value = "";
                    }
                    else
                    {
                        dgv.Rows.Remove(dgv.Rows[i]);

                        if (dgv.Name == "GroundGrid")
                        {
                            groundItems.Remove(item);
                        }
                    }
                    return;
                }
            }
        }

        private void ClearBodySelections()
        {
            ClearRowSelection(dgvBody);
        }
        private void ClearQuickSelections()
        {
            ClearRowSelection(dgvQuick);
        }
        private void ClearPackSelections()
        {
            ClearRowSelection(dgvPack);
        }
        private void ClearGroundSelections()
        {
            ClearRowSelection(dgvGround);
        }
        private void ClearRowSelection(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dgv.SelectedRows.Count; i++)
                {
                    dgv.SelectedRows[i].Selected = false;
                }
            }
        }

        private void LoadBodyGrid(Actor a)
        {
            ((System.ComponentModel.ISupportInitialize)(this.dgvBody)).BeginInit();

            dtBodyItems = new DataTable();
            dtBodyItems.Columns.Add("Slot");
            dtBodyItems.Columns.Add("Name");
            dtBodyItems.Columns.Add("Type");
            dtBodyItems.Columns.Add("Description");

            LoadBodyRow(a.inventory.GetBodyItem((int)Inventory.BodySlot.Head), Inventory.BodySlot.Head, true);
            LoadBodyRow(a.inventory.GetBodyItem((int)Inventory.BodySlot.Neck), Inventory.BodySlot.Neck, true);
            LoadBodyRow(a.inventory.GetBodyItem((int)Inventory.BodySlot.Torso), Inventory.BodySlot.Torso, true);
            LoadBodyRow(a.inventory.GetBodyItem((int)Inventory.BodySlot.Hand1), Inventory.BodySlot.Hand1, true);
            LoadBodyRow(a.inventory.GetBodyItem((int)Inventory.BodySlot.Hand2), Inventory.BodySlot.Hand2, true);
            LoadBodyRow(a.inventory.GetBodyItem((int)Inventory.BodySlot.Ring1), Inventory.BodySlot.Ring1, true);
            LoadBodyRow(a.inventory.GetBodyItem((int)Inventory.BodySlot.Ring2), Inventory.BodySlot.Ring2, true);
            LoadBodyRow(a.inventory.GetBodyItem((int)Inventory.BodySlot.Belt), Inventory.BodySlot.Belt, true);
            LoadBodyRow(a.inventory.GetBodyItem((int)Inventory.BodySlot.Feet), Inventory.BodySlot.Feet, true);
            LoadBodyRow(a.inventory.GetBodyItem((int)Inventory.BodySlot.Ammo), Inventory.BodySlot.Ammo, true);

            dgvBody.DataSource = dtBodyItems;
            ((System.ComponentModel.ISupportInitialize)(this.dgvBody)).EndInit();
        }
        private void LoadBodyRow(RPGItem item, Inventory.BodySlot slot, bool AddNewRow)
        {
            DataRow row;
            if (AddNewRow)
            {
                row = dtBodyItems.NewRow();
            }
            else
            {
                row = dtBodyItems.Rows[(int)slot];
            }
            row[0] = Enum.GetName(typeof(Inventory.BodySlot), slot);

            if (item == null)
            {
                row[1] = "";
                row[2] = "";
                row[3] = "";
            }
            else
            {
                row[1] = item.Name;
                row[2] = GetItemType(item);
                row[3] = item.Description;

            }
            if (AddNewRow)
            {
                dtBodyItems.Rows.Add(row);
            }
        }

        private void LoadQuickGrid(Actor a)
        {
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuick)).BeginInit();

            dtQuickItems = new DataTable();
            dtQuickItems.Columns.Add("Name");
            dtQuickItems.Columns.Add("Type");
            dtQuickItems.Columns.Add("Description");

            for (int i = 0; i < Inventory.QUICK_SIZE; i++)
            {
                LoadQuickRow(a.inventory.GetQuickItem(i));
            }

            dgvQuick.DataSource = dtQuickItems;
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuick)).EndInit();
        }
        private void LoadQuickRow(RPGObject item)
        {
            if (item == null) { return; }
            DataRow row = dtQuickItems.NewRow();
            row[0] = item.Name;
            row[1] = GetItemType(item);
            if (item.isOfType(typeof(RPGItem)))
            {
                row[2] = (item as RPGItem).Description;
            }
            dtQuickItems.Rows.Add(row);
        }

        private void LoadPackGrid(Actor a)
        {
            ((System.ComponentModel.ISupportInitialize)(this.dgvPack)).BeginInit();

            dtPackItems = new DataTable();
            dtPackItems.Columns.Add("Name");
            dtPackItems.Columns.Add("Type");
            dtPackItems.Columns.Add("Description");

            for (int i = 0; i < Inventory.PACK_SIZE; i++)
            {
                LoadPackRow(a.inventory.GetPackItem(i));
            }

            dgvPack.DataSource = dtPackItems;
            ((System.ComponentModel.ISupportInitialize)(this.dgvPack)).EndInit();
        }
        private void LoadPackRow(RPGObject item)
        {
            if (item == null) { return; }
            DataRow row = dtPackItems.NewRow();
            row[0] = item.Name;
            row[1] = GetItemType(item);
            if (item.isOfType(typeof(RPGItem)))
            {
                row[2] = (item as RPGItem).Description;
            }
            dtPackItems.Rows.Add(row);
        }

        private void LoadGroundGrid(Point p)
        {
            ArrayList items = Session.thisSession.thisArea.GetItemsNear(p);
            dtGroundItems = new DataTable();
            dtGroundItems.Columns.Add("Name");
            dtGroundItems.Columns.Add("Type");
            dtGroundItems.Columns.Add("Description");

            foreach (RPGObject item in items)
            {
                if (item.isOfType(typeof(RPGItem)))
                {
                    LoadGroundRow((RPGItem)item);
                }
                else if (item.isOfType(typeof(RPGDrop)))
                {
                    RPGObject[] DropItems = (item as RPGDrop).GetItems();
                    for (int i = 0; i < DropItems.Length; i++)
                    {
                        LoadGroundRow(DropItems[i] as RPGItem);
                    }
                }
            }

            dgvGround.DataSource = dtGroundItems;
        }
        private void LoadGroundRow(RPGObject item)
        {
            if (item == null) { return; }
            DataRow row = dtGroundItems.NewRow();
            row[0] = item.Name;
            row[1] = GetItemType(item);

            if (item.isOfType(typeof(RPGItem)))
            {
                row[2] = (item as RPGItem).Description;
            }
            dtGroundItems.Rows.Add(row);
            groundItems.Add(item);
        }
        private string GetItemType(RPGObject item)
        {
            // set 'type' conditionally.
            string type = item.GetType().ToString();

            switch (type)
            {
                case ("RPG.RPGWeapon"):
                    {
                        // then get the weapon class/type
                        return Enum.GetName(typeof(RPGWeapon.WeaponClass), ((RPGWeapon)item).weaponClass);
                        //break;
                    }
                case ("RPG.Projectile"):
                    {
                        return Enum.GetName(typeof(Projectile.ProjectileType), ((Projectile)item).type);
                        //break;
                    }
                case ("RPG.RPGArmor"):
                    {
                        // then get the armor class/type
                        return Enum.GetName(typeof(RPGArmor.ArmorClass), ((RPGArmor)item).Class);
                        //break;
                    }
                case ("RPG.RPGPotion"):
                    {
                        return "Potion";
                        //break;
                    }
                default:
                    {
                        return type;
                    }
            } // end switch
        }
        #endregion

    }
}
