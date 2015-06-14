using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace B15_Ex02_1
{
    public partial class FormGameOver : Form
    {
        Button m_ButtonaAnotherRoundYes = new Button();
        Button m_ButtonExitNo = new Button();

        public FormGameOver()
        {
            this.Size = new Size(400, 200);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Othello";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            InitControls();
        }

        private void InitControls()
        {
            m_ButtonaAnotherRoundYes.Text = "Yes";
            m_ButtonaAnotherRoundYes.Location = new Point(160, 130);
            m_ButtonaAnotherRoundYes.Size = new Size(90, 30);

            this.Controls.AddRange(new System.Windows.Forms.Control[] { m_ButtonaAnotherRoundYes });
            m_ButtonaAnotherRoundYes.Click += new EventHandler(m_ButtonaAnotherRoundYes_click);
        }

        private void m_ButtonaAnotherRoundYes_click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
