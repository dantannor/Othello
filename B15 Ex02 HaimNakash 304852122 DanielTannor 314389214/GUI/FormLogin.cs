using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GUI
{
    class FormLogin : Form
    {
        private int m_boardSize = 6;
        Button m_ButtonIncreaseBoardSize = new Button();
        Button m_ButtonAgainstComputer = new Button();
        Button m_ButtonAgainstFriend = new Button();

        public FormLogin()
        {
            this.Size = new Size(400,225);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Othello - Game Settings";
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            InitControls();
        }

        private void InitControls()
        {
            m_ButtonIncreaseBoardSize.Text = "Board size: 6x6 (click to increase)";
            m_ButtonIncreaseBoardSize.Location = new Point(10,10);
            m_ButtonIncreaseBoardSize.Size = new Size(365,75);

            m_ButtonAgainstComputer.Text = "Play against the computer";
            m_ButtonAgainstComputer.Location = new Point(10, 95);
            m_ButtonAgainstComputer.Size = new Size(180, 75);

            m_ButtonAgainstFriend.Text = "Play against your friend";
            m_ButtonAgainstFriend.Location = new Point(200, 95);
            m_ButtonAgainstFriend.Size = new Size(175, 75);


            this.Controls.AddRange(new Control[] { m_ButtonIncreaseBoardSize, m_ButtonAgainstComputer, m_ButtonAgainstFriend});
            m_ButtonIncreaseBoardSize.Click += new EventHandler(m_ButtonIncreaseBoardSize_Click);
        }

        private void m_ButtonIncreaseBoardSize_Click(object sender, EventArgs e)
        {
            m_ButtonIncreaseBoardSize.Text = String.Format("Board size: {0}x{0} (click to increase)",m_boardSize);
            if (m_boardSize != 12)
            {
                m_boardSize += 2;
            }

        }

        private void m_ButtonAgainstComputer_Click(object sender, EventArgs e)
        {
            
        }
    }
}
