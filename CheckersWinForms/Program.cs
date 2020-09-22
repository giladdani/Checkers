using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersWinForms
{
    public class Program
    {
        public static void Main()
        {
            Application.EnableVisualStyles();       // just for aesthetics
            GameSettingsForm gameSettingsForm = new GameSettingsForm();
            if (gameSettingsForm.ShowDialog() == DialogResult.OK)
            {
                BoardForm boardForm = new BoardForm(gameSettingsForm.TextBoxPlayerOneName,gameSettingsForm.TextBoxPlayerTwoName, gameSettingsForm.SelectedBoardSize);
                boardForm.ShowDialog();
            }
        }
    }
}