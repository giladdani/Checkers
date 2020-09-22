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
            WinFormsUi ui = new WinFormsUi();
            gameInterface.CreateGame();
            gameInterface.StartRound();
        }
    }
}