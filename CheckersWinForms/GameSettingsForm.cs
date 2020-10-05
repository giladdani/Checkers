using System;
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

        // Private Methods
        private bool isFormValid()
        {
            bool isValid = true;

            if (m_TextBoxPlayerOneName.Text.Length == 0)
            {
                isValid = false;
            }

            if (m_TextBoxPlayerTwoName.Text.Length == 0 && m_CheckBoxPlayer2.CheckState == CheckState.Checked)
            {
                isValid = false;
            }

            return isValid;
        }

        // Event Handlers
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            m_TextBoxPlayerTwoName.Enabled = m_CheckBoxPlayer2.Checked;
            m_ButtonDone.Enabled = isFormValid();
        }

        private void m_TextBoxPlayerOneName_TextChanged(object sender, EventArgs e)
        {
            m_ButtonDone.Enabled = isFormValid();
        }

        private void m_TextBoxPlayerTwoName_TextChanged(object sender, EventArgs e)
        {
            m_ButtonDone.Enabled = m_TextBoxPlayerTwoName.Text.Length > 0;
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

        public bool TwoPlayersMode
        {
            get
            {
                return m_CheckBoxPlayer2.CheckState == CheckState.Checked;
            }
        }
    }
}