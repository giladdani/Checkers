using System.Windows.Forms;

namespace CheckersWinForms
{
    public class Program
    {
        public static void Main()
        {
            WinFormsUiManager ui = new WinFormsUiManager();
            ui.CreateGame();
            ui.StartGame();
        }
    }
}