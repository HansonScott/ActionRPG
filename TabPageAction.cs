using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace RPG
{
    public class TabPageAction: TabPage
    {
        // basic visuals set in session designer(container)
        #region Declaractions
        public PanelAction panelAction;
        public PanelActionToolbar panelActionToolbar;
        #endregion

        #region Constructor
        public TabPageAction()
        {
            this.panelActionToolbar = new RPG.PanelActionToolbar();
            this.panelActionToolbar.Location = new System.Drawing.Point(8, 600);
            this.panelActionToolbar.Size = new System.Drawing.Size(992, 100);
            this.panelActionToolbar.Name = "panelActionToolbar1";
            this.panelActionToolbar.BackColor = Color.Red;
            this.Controls.Add(this.panelActionToolbar);

            this.panelAction = new RPG.PanelAction(panelActionToolbar);
            this.panelAction.Location = new System.Drawing.Point(8, 6);
            this.panelAction.Name = "panelAction1";
            this.panelAction.Size = new System.Drawing.Size(PanelAction.PANEL_WIDTH, PanelAction.PANEL_HEIGHT);
            this.panelAction.BackColor = Color.Green;
            this.Controls.Add(this.panelAction);

        }
        #endregion

        #region Events
        #endregion

        #region Private methods
        #endregion
    }
}
