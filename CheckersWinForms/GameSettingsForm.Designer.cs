using System.Windows.Forms;

namespace CheckersWinForms
{
    partial class GameSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_ButtonDone = new System.Windows.Forms.Button();
            this.m_LabelPlayers = new System.Windows.Forms.Label();
            this.m_LabelPlayer1 = new System.Windows.Forms.Label();
            this.m_TextBoxPlayerOneName = new System.Windows.Forms.TextBox();
            this.m_TextBoxPlayerTwoName = new System.Windows.Forms.TextBox();
            this.m_CheckBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.m_LabelBoardSize = new System.Windows.Forms.Label();
            this.m_RadioButtonSizeSmall = new System.Windows.Forms.RadioButton();
            this.m_RadioButtonSizeMedium = new System.Windows.Forms.RadioButton();
            this.m_RadioButtonSizeLarge = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // m_ButtonDone
            // 
            this.m_ButtonDone.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_ButtonDone.Enabled = false;
            this.m_ButtonDone.Location = new System.Drawing.Point(222, 210);
            this.m_ButtonDone.Name = "m_ButtonDone";
            this.m_ButtonDone.Size = new System.Drawing.Size(98, 31);
            this.m_ButtonDone.TabIndex = 0;
            this.m_ButtonDone.Text = "Done";
            this.m_ButtonDone.UseVisualStyleBackColor = true;
            // 
            // m_LabelPlayers
            // 
            this.m_LabelPlayers.AutoSize = true;
            this.m_LabelPlayers.Location = new System.Drawing.Point(33, 99);
            this.m_LabelPlayers.Name = "m_LabelPlayers";
            this.m_LabelPlayers.Size = new System.Drawing.Size(59, 17);
            this.m_LabelPlayers.TabIndex = 2;
            this.m_LabelPlayers.Text = "Players:";
            // 
            // m_LabelPlayer1
            // 
            this.m_LabelPlayer1.AutoSize = true;
            this.m_LabelPlayer1.Location = new System.Drawing.Point(25, 133);
            this.m_LabelPlayer1.Name = "m_LabelPlayer1";
            this.m_LabelPlayer1.Size = new System.Drawing.Size(64, 17);
            this.m_LabelPlayer1.TabIndex = 3;
            this.m_LabelPlayer1.Text = "Player 1:";
            // 
            // m_TextBoxPlayerOneName
            // 
            this.m_TextBoxPlayerOneName.Location = new System.Drawing.Point(123, 133);
            this.m_TextBoxPlayerOneName.Name = "m_TextBoxPlayerOneName";
            this.m_TextBoxPlayerOneName.Size = new System.Drawing.Size(100, 22);
            this.m_TextBoxPlayerOneName.TabIndex = 5;
            this.m_TextBoxPlayerOneName.TextChanged += new System.EventHandler(this.m_TextBoxPlayerOneName_TextChanged);
            // 
            // m_TextBoxPlayerTwoName
            // 
            this.m_TextBoxPlayerTwoName.Enabled = false;
            this.m_TextBoxPlayerTwoName.Location = new System.Drawing.Point(123, 165);
            this.m_TextBoxPlayerTwoName.Name = "m_TextBoxPlayerTwoName";
            this.m_TextBoxPlayerTwoName.Size = new System.Drawing.Size(100, 22);
            this.m_TextBoxPlayerTwoName.TabIndex = 6;
            this.m_TextBoxPlayerTwoName.Text = "Computer";
            // 
            // m_CheckBoxPlayer2
            // 
            this.m_CheckBoxPlayer2.AutoSize = true;
            this.m_CheckBoxPlayer2.Location = new System.Drawing.Point(28, 165);
            this.m_CheckBoxPlayer2.Name = "m_CheckBoxPlayer2";
            this.m_CheckBoxPlayer2.Size = new System.Drawing.Size(86, 21);
            this.m_CheckBoxPlayer2.TabIndex = 7;
            this.m_CheckBoxPlayer2.Text = "Player 2:";
            this.m_CheckBoxPlayer2.UseVisualStyleBackColor = true;
            this.m_CheckBoxPlayer2.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // m_LabelBoardSize
            // 
            this.m_LabelBoardSize.AutoSize = true;
            this.m_LabelBoardSize.Location = new System.Drawing.Point(33, 13);
            this.m_LabelBoardSize.Name = "m_LabelBoardSize";
            this.m_LabelBoardSize.Size = new System.Drawing.Size(81, 17);
            this.m_LabelBoardSize.TabIndex = 8;
            this.m_LabelBoardSize.Text = "Board Size:";
            // 
            // m_RadioButtonSizeSmall
            // 
            this.m_RadioButtonSizeSmall.AutoSize = true;
            this.m_RadioButtonSizeSmall.Checked = true;
            this.m_RadioButtonSizeSmall.Location = new System.Drawing.Point(28, 46);
            this.m_RadioButtonSizeSmall.Name = "m_RadioButtonSizeSmall";
            this.m_RadioButtonSizeSmall.Size = new System.Drawing.Size(51, 21);
            this.m_RadioButtonSizeSmall.TabIndex = 9;
            this.m_RadioButtonSizeSmall.TabStop = true;
            this.m_RadioButtonSizeSmall.Text = "6x6";
            this.m_RadioButtonSizeSmall.UseVisualStyleBackColor = true;
            // 
            // m_RadioButtonSizeMedium
            // 
            this.m_RadioButtonSizeMedium.AutoSize = true;
            this.m_RadioButtonSizeMedium.Location = new System.Drawing.Point(123, 46);
            this.m_RadioButtonSizeMedium.Name = "m_RadioButtonSizeMedium";
            this.m_RadioButtonSizeMedium.Size = new System.Drawing.Size(51, 21);
            this.m_RadioButtonSizeMedium.TabIndex = 10;
            this.m_RadioButtonSizeMedium.TabStop = true;
            this.m_RadioButtonSizeMedium.Text = "8x8";
            this.m_RadioButtonSizeMedium.UseVisualStyleBackColor = true;
            // 
            // m_RadioButtonSizeLarge
            // 
            this.m_RadioButtonSizeLarge.AutoSize = true;
            this.m_RadioButtonSizeLarge.Location = new System.Drawing.Point(219, 46);
            this.m_RadioButtonSizeLarge.Name = "m_RadioButtonSizeLarge";
            this.m_RadioButtonSizeLarge.Size = new System.Drawing.Size(67, 21);
            this.m_RadioButtonSizeLarge.TabIndex = 11;
            this.m_RadioButtonSizeLarge.TabStop = true;
            this.m_RadioButtonSizeLarge.Text = "10x10";
            this.m_RadioButtonSizeLarge.UseVisualStyleBackColor = true;
            // 
            // GameSettingsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(332, 253);
            this.Controls.Add(this.m_RadioButtonSizeLarge);
            this.Controls.Add(this.m_RadioButtonSizeMedium);
            this.Controls.Add(this.m_RadioButtonSizeSmall);
            this.Controls.Add(this.m_LabelBoardSize);
            this.Controls.Add(this.m_CheckBoxPlayer2);
            this.Controls.Add(this.m_TextBoxPlayerTwoName);
            this.Controls.Add(this.m_TextBoxPlayerOneName);
            this.Controls.Add(this.m_LabelPlayer1);
            this.Controls.Add(this.m_LabelPlayers);
            this.Controls.Add(this.m_ButtonDone);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(380, 334);
            this.MinimumSize = new System.Drawing.Size(350, 300);
            this.Name = "GameSettingsForm";
            this.Text = "Game Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button m_ButtonDone;
        private Label m_LabelPlayers;
        private Label m_LabelPlayer1;
        private TextBox m_TextBoxPlayerOneName;
        private TextBox m_TextBoxPlayerTwoName;
        private Label m_LabelBoardSize;
        private RadioButton m_RadioButtonSizeSmall;
        private RadioButton m_RadioButtonSizeMedium;
        private RadioButton m_RadioButtonSizeLarge;
        private CheckBox m_CheckBoxPlayer2;
    }
}