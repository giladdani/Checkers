namespace CheckersWinForms
{
    public class Program
    {
        public static void Main()
        {
            UiManager ui = new UiManager();
            ui.CreateGame();
            ui.StartGame();
        }
    }
}