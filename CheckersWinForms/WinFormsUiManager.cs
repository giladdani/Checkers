using System;
using System.Windows.Forms;

namespace CheckersWinForms
{
    public class WinFormsUiManager
    {
        // Private Members
        private Timer m_AiThinkingTimer;
        private GameSettingsForm m_GameSettingsForm;
        private BoardForm m_BoardForm;
        private Game m_Game;

        // Public Methods
        public void CreateGame()
        {
            m_GameSettingsForm = new GameSettingsForm();
            if (m_GameSettingsForm.ShowDialog() == DialogResult.OK)
            {
                string playerOneName = m_GameSettingsForm.TextBoxPlayerOneName;
                string playerTwoName = m_GameSettingsForm.TextBoxPlayerTwoName;

                m_Game = new Game(playerOneName, playerTwoName, m_GameSettingsForm.SelectedBoardSize, m_GameSettingsForm.TwoPlayersMode);
            }
        }

        public void StartGame()
        {
            m_Game.TurnCount = 0;
            m_Game.Board.Build();
            m_Game.SetPiecesStartingPositions();
            m_Game.MoveExecuted += AnalyzeMoveFeedback;
            m_BoardForm = new BoardForm(m_Game);
            if (m_Game.AiMode == true)
            {
                m_AiThinkingTimer = new Timer { Interval = 1200 };
                m_AiThinkingTimer.Tick += playAiTurn;
            }

            m_BoardForm.ShowDialog();
        }

        private void playAiTurn(object i_Sender, EventArgs i_E)
        {
            m_AiThinkingTimer.Stop();
            Move move = m_Game.PlayerTwo.GenerateRandomMove(m_Game.Board);
            m_Game.ExecuteMove(move);
        }

        // Private Methods
        private void resetRound()
        {
            m_Game.Board.Build();
            m_Game.TurnCount = 0;
            m_Game.SetPiecesStartingPositions();
            m_BoardForm.ResetBoardSettings(m_Game.Board);
        }

        public void endRound()
        {
            m_Game.CalculateScores();
            m_BoardForm.UpdatePlayersNamesAndScores();
            if (playAgain() == true)
            {
                resetRound();
            }
            else
            {
                Environment.Exit(-1);
            }
        }

        private bool playAgain()
        {
            string roundResults;

            if (m_Game.PlayerOne.TotalScore > m_Game.PlayerTwo.TotalScore)
            {
                roundResults = string.Format("{0} Won!", m_Game.PlayerOne.Name);
            }
            else if (m_Game.PlayerOne.TotalScore < m_Game.PlayerTwo.TotalScore)
            {
                roundResults = string.Format("{0} Won!", m_Game.PlayerTwo.Name);
            }
            else
            {
                roundResults = string.Format("Tie!");
            }

            string roundResultsMessage = string.Format("{0}{1}Another Round?", roundResults, Environment.NewLine);
            DialogResult playAgainDialogResult = MessageBox.Show(roundResultsMessage, "Round ended", MessageBoxButtons.YesNo);
            bool isPlayAgain = playAgainDialogResult == DialogResult.Yes;

            return isPlayAgain;
        }

        // Event Handlers
        public void AnalyzeMoveFeedback(eMoveFeedback i_MoveFeedback)
        {
            switch (i_MoveFeedback)
            {
                case eMoveFeedback.Failed:
                    {
                        MessageBox.Show("Invalid move.");
                        break;
                    }

                case eMoveFeedback.FailedCouldCapture:
                    {
                        MessageBox.Show("You can capture! Get him!");
                        break;
                    }

                default:
                    {
                        m_BoardForm.HighlightCurrentPlayerLabel();
                        if (m_Game.IsOver())
                        {
                            endRound();
                        }

                        if (m_Game.CurrentPlayer.IsAi)
                        {
                            m_AiThinkingTimer.Start();
                        }

                        break;
                    }
            }
        }
    }
}
