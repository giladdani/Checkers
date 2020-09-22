using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersWinForms
{
    public partial class GameSettingsForm : Form
    {
        public GameSettingsForm()
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // hide window resizing
            InitializeComponent();
            Game theGame = new Game(m_TextBoxPlayerOneName.Text, m_TextBoxPlayerTwoName.Text, SelectedBoardSize, !m_CheckBoxPlayer2.Checked);

        }

        public int SelectedBoardSize
        {
            get
            {
                if (m_RadioButtonSizeSmall.Checked)
                {
                    return 6;
                }
                else if (m_RadioButtonSizeMedium.Checked)
                {
                    return 8;
                }
                else
                {
                    return 10;
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            m_TextBoxPlayerTwoName.Enabled = m_CheckBoxPlayer2.Checked;
        }

        private void m_TextBoxPlayerOneName_TextChanged(object sender, EventArgs e)
        {
            m_ButtonDone.Enabled = m_TextBoxPlayerOneName.Text.Length > 0;
        }
       
        public String TextBoxPlayerOneName
        {
            get
            {
                return m_TextBoxPlayerOneName.Text;
            }
        }
        public String TextBoxPlayerTwoName
        {
            get
            {
                return m_TextBoxPlayerTwoName.Text;
            }
        }
    }
}