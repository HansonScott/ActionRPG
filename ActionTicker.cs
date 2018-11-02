using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace RPG
{
    public class ActionTicker: TextBox
    {
        public ActionTicker()
        {
            this.BackColor = Color.Black;
            this.ForeColor = Color.White;
            //this.Font = new Font(ticker.Font.FontFamily, ticker.Font.Size, FontStyle.Bold);
            this.Multiline = true;
            this.ScrollBars = ScrollBars.Vertical;
            this.WordWrap = true;
        }

        //NOTE: need the delegate thing to run cross thread print call.
        private delegate void addDelegate(string msg);
        public void Print(string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new addDelegate(this.PrintAsync), msg);
            }
            else
            {
                this.PrintAsync(msg);
            }
        }
        private void PrintAsync(string msg)
        {
            // add msg to bottom of text and scroll accordingly.
            this.Text += msg + "\r\n";
            this.Select(this.Text.Length, 0);
            this.ScrollToCaret();
        }
    }
}
