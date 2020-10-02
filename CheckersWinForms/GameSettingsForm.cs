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

        // Event Handlers
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            m_TextBoxPlayerTwoName.Enabled = m_CheckBoxPlayer2.Checked;
        }

        private void m_TextBoxPlayerOneName_TextChanged(object sender, EventArgs e)
        {
            m_ButtonDone.Enabled = m_TextBoxPlayerOneName.Text.Length > 0;
        }
       
        // Properties
        public string TextBoxPlayerOneName
        {
            get
            {
                return m_TextBoxPlayerOneName.Text;
            }
        }

        public string TextBoxPlayerTwoName
        {
            get
            {
                return m_TextBoxPlayerTwoName.Text;
            }
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

        public bool AiMode
        {
            get
            {
                return m_CheckBoxPlayer2.Enabled;
            }
        }
    }
}