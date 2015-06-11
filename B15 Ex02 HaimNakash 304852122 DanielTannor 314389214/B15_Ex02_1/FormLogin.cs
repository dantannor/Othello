using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using B15_Ex02_1.Control;

namespace GUI
{
    class FormLogin : Form
    {
        private eBoardSize m_BoardSize = eBoardSize.Six;
        Button m_ButtonIncreaseBoardSize = new Button();
        Button m_ButtonAgainstComputer = new Button();
        Button m_ButtonAgainstFriend = new Button();
        private bool r_AgainstComputer = false;


        public eBoardSize BoardSize
        {
            get { return m_BoardSize; }
            set { m_BoardSize = value; }
        }

        public bool AgainstComputer
        {
            get { return r_AgainstComputer; }
            set { r_AgainstComputer = value; }
        }


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
            m_ButtonAgainstComputer.Click += new EventHandler(m_ButtonAgainstComputer_Click);
        }

        private void m_ButtonIncreaseBoardSize_Click(object sender, EventArgs e)
        {
            if (m_BoardSize != (eBoardSize)12)
            {
                m_BoardSize += 2;
            }
            m_ButtonIncreaseBoardSize.Text = String.Format("Board size: {0}x{0} (click to increase)",(int)m_BoardSize);
        }

        private void m_ButtonAgainstComputer_Click(object sender, EventArgs e)
        {
           //TODO: Close login window
            
            Close();
            this.DialogResult = DialogResult.OK;
            AgainstComputer = true;
        }

        private void m_ButtonAgainstFriend_Click(object sender, EventArgs e)
        {
            //TODO: Close login window

            Close();
            this.DialogResult = DialogResult.OK;
            AgainstComputer = true;
        }
        
    }
}
