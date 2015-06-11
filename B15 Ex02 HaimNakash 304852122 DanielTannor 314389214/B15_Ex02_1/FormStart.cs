namespace B15_Ex02_1
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using B15_Ex02_1.Control;

    public class FormStart : Form
    {
        private readonly Button m_ButtonIncreaseBoardSize = new Button();

        private readonly Button m_ButtonAgainstComputer = new Button();

        private readonly Button m_ButtonAgainstFriend = new Button();

        private eBoardSize m_BoardSize = eBoardSize.Six;

        private bool r_AgainstComputer;
       
        public eBoardSize BoardSize
        {
            get { return this.m_BoardSize; }
            set { this.m_BoardSize = value; }
        }

        public bool AgainstComputer
        {
            get { return this.r_AgainstComputer; }
            set { this.r_AgainstComputer = value; }
        }

        public FormStart()
        {
            this.Size = new Size(400, 225);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Othello - Game Settings";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.InitControls();
        }

        private void InitControls()
        {
            this.m_ButtonIncreaseBoardSize.Text = "Board size: 6x6 (click to increase)";
            this.m_ButtonIncreaseBoardSize.Location = new Point(10, 10);
            this.m_ButtonIncreaseBoardSize.Size = new Size(365, 75);

            this.m_ButtonAgainstComputer.Text = "Play against the computer";
            this.m_ButtonAgainstComputer.Location = new Point(10, 95);
            this.m_ButtonAgainstComputer.Size = new Size(180, 75);

            this.m_ButtonAgainstFriend.Text = "Play against your friend";
            this.m_ButtonAgainstFriend.Location = new Point(200, 95);
            this.m_ButtonAgainstFriend.Size = new Size(175, 75);

            this.Controls.AddRange(
                new System.Windows.Forms.Control[]
                    {
                        this.m_ButtonIncreaseBoardSize, this.m_ButtonAgainstComputer, this.m_ButtonAgainstFriend 
                    });

            this.m_ButtonIncreaseBoardSize.Click += this.m_ButtonIncreaseBoardSize_Click;
            this.m_ButtonAgainstComputer.Click += this.m_ButtonAgainstComputer_Click;
            this.m_ButtonAgainstFriend.Click += this.m_ButtonAgainstFriend_Click;
        }

        private void m_ButtonIncreaseBoardSize_Click(object sender, EventArgs e)
        {
            if (this.m_BoardSize != eBoardSize.Twelve)
            {
                this.m_BoardSize += 2;
            }

            this.m_ButtonIncreaseBoardSize.Text = string.Format("Board size: {0}x{0} (click to increase)", (int)this.m_BoardSize);
        }

        private void m_ButtonAgainstComputer_Click(object sender, EventArgs e)
        {
           // TODO: Close login window
            this.Close();
            this.DialogResult = DialogResult.OK;
            this.AgainstComputer = true;
        }

        private void m_ButtonAgainstFriend_Click(object sender, EventArgs e)
        {
            // TODO: Close login window
            this.Close();
            this.DialogResult = DialogResult.OK;
            this.AgainstComputer = false;
        }
    }
}
