using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace PegSolitaire1
{

    public partial class Form1 : Form
    {
        private Button[] buttons;
        private Button curBtn;
        private int Srow, Scol;
        private int Erow, Ecol;
        public Form1()
        {

            InitializeComponent();
            InitializeButtons();
        }

        private void InitializeButtons()
        {
            buttons = new Button[]
            {
                button1,button2,button3,button4,button5,button6,button7,button8,button9,button10,
                button11,button12,button13,button14,button15,button16,button17,button18,button19,button20,
                button21,button22,button23,button24,button25,button26,button27,button28,button29,button30,
                button31,button32
            };

            for (int i = 0; i < 32; i++)
            {
                buttons[i].MouseDown += MouseDownEvent;
                buttons[i].MouseUp += MouseUpEvent;

            }

        }

        private void OnClickRestart(object sender, EventArgs e)
        {
            string exePath = Process.GetCurrentProcess().MainModule.FileName;

            Process.Start(exePath);

            Environment.Exit(0);
        }
        private void MouseDownEvent(Object sender, MouseEventArgs e)
        {
            if (sender.GetType() == typeof(Panel))
            {
                return;
            }
            curBtn = sender as Button;

            Point p1 = curBtn.PointToScreen(new Point(e.X, e.Y));
            p1 = tableLayoutPanelBallHolders.PointToClient(p1);

            int iBoxWidht = tableLayoutPanelBallHolders.Width / 7;
            int iBoxHeight = tableLayoutPanelBallHolders.Height / 7;

            Srow = p1.Y / (iBoxHeight);
            Scol = p1.X / iBoxWidht;


        }

        private void MouseUpEvent(Object sender, MouseEventArgs e)
        {
            if (sender.GetType() == typeof(Panel))
            {
                return;
            }

            if (curBtn != null)
            {
                Point p = curBtn.PointToScreen(new Point(e.X, e.Y));
                p = tableLayoutPanelBallHolders.PointToClient(p);

                int iBoxWidht = tableLayoutPanelBallHolders.Width / 7;
                int iBoxHeight = tableLayoutPanelBallHolders.Height / 7;

                Erow = p.Y / (iBoxHeight);
                Ecol = p.X / iBoxWidht;

                if (ContainsBtn(tableLayoutPanelBallHolders.GetControlFromPosition(Ecol, Erow)))
                {
                    return;
                }



                int diffRow = Erow - Srow;
                int diffCol = Ecol - Scol;

                //up
                if (diffRow == -2 && diffCol == 0)
                {
                    tableLayoutPanelBallHolders.Controls.Remove(tableLayoutPanelBallHolders.GetControlFromPosition(Scol, Srow - 1));
                    labelScore.Text = Convert.ToString(Convert.ToInt32(labelScore.Text) - 1);
                }
                //down
                else if (diffRow == 2 && diffCol == 0)
                {
                    tableLayoutPanelBallHolders.Controls.Remove(tableLayoutPanelBallHolders.GetControlFromPosition(Ecol, Erow - 1));
                    labelScore.Text = Convert.ToString(Convert.ToInt32(labelScore.Text) - 1);
                }
                //right
                else if (diffRow == 0 && diffCol == 2)
                {
                    tableLayoutPanelBallHolders.Controls.Remove(tableLayoutPanelBallHolders.GetControlFromPosition(Ecol - 1, Erow));
                    labelScore.Text = Convert.ToString(Convert.ToInt32(labelScore.Text) - 1);
                }
                //left
                else if (diffRow == 0 && diffCol == -2)
                {
                    tableLayoutPanelBallHolders.Controls.Remove(tableLayoutPanelBallHolders.GetControlFromPosition(Scol - 1, Erow));
                    labelScore.Text = Convert.ToString(Convert.ToInt32(labelScore.Text) - 1);
                }
                else
                {
                    MessageBox.Show("Game Over\nPoints: " + labelScore.Text);
                    return;
                }
                tableLayoutPanelBallHolders.Controls.Add(curBtn, Ecol, Erow);





            }
            curBtn = null;
        }
        private bool ContainsBtn(Control panel)
        {
            if (panel == null) return false;
            foreach (Control c in panel.Controls)
            {
                if (c is Button)
                {
                    return true;
                }


            }
            return false;
        }
    }
}
