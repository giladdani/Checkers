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
        // Constructors
        public GameSettingsForm()
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // hide window resizing
            InitializeComponent();
        }

        // Properties
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

        // Events
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            m_TextBoxPlayerTwoName.Enabled = m_CheckBoxPlayer2.Checked;
        }

        public string PlayerOneName
        {
            get
            {
                return m_TextBoxPlayerOneName.Text;
            }
        }
    }
}