using System;
using System.Drawing;

namespace CheckersWinForms
{
    public partial class BoardForm
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
            this.LabelPlayerOneName = new System.Windows.Forms.Label();
            this.LabelPlayerTwoName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LabelPlayerOneName
            // 
            this.LabelPlayerOneName.AutoSize = true;
            this.LabelPlayerOneName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPlayerOneName.Location = new System.Drawing.Point(100, 30);
            this.LabelPlayerOneName.Name = "LabelPlayerOneName";
            this.LabelPlayerOneName.Size = new System.Drawing.Size(84, 20);
            this.LabelPlayerOneName.TabIndex = 0;
            this.LabelPlayerOneName.Text = "Player 1:";
            // 
            // LabelPlayerTwoName
            // 
            this.LabelPlayerTwoName.AutoSize = true;
            this.LabelPlayerTwoName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPlayerTwoName.Location = new System.Drawing.Point(242, 30);
            this.LabelPlayerTwoName.Name = "LabelPlayerTwoName";
            this.LabelPlayerTwoName.Size = new System.Drawing.Size(84, 20);
            this.LabelPlayerTwoName.TabIndex = 1;
            this.LabelPlayerTwoName.Text = "Player 2:";
            // 
            // BoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(382, 379);
            this.Controls.Add(this.LabelPlayerTwoName);
            this.Controls.Add(this.LabelPlayerOneName);
            this.MaximizeBox = false;
            this.Name = "BoardForm";
            this.Text = "Board";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelPlayerOneName;
        private System.Windows.Forms.Label LabelPlayerTwoName;
    }
}