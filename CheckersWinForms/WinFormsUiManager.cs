using System;
using System.Windows.Forms;

namespace CheckersWinForms
{
    public class WinFormsUiManager
    {
        // Private Members
        private GameSettingsForm m_GameSettingsForm;
        private BoardForm m_BoardForm;
        private Game m_Game;
        private Timer m_AiThinkingTimer;

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
            else
            {
                Environment.Exit(-1);
            }
        }

        // Initialize first round settings
        public void StartGame()
        {
            m_Game.ResetBasicSettings();
            m_Game.MoveExecuted += AnalyzeMoveFeedback;
            m_BoardForm = new BoardForm(m_Game);
            if (m_Game.AiMode)
            {
                m_AiThinkingTimer = new Timer { Interval = 1250 };
                m_AiThinkingTimer.Tick += playAiTurn;
            }

            m_BoardForm.ShowDialog();
        }

        // Private Methods
        // Resets basic round settings (board, turn count, pieces positions)
        private void resetRound()
        {
            m_Game.ResetBasicSettings();
            m_BoardForm.ResetBoardSettings(m_Game.Board);
        }

        // Finalize current round- count each player's score and ask the user if he wants to play another round
        private void endRound()
        {
            m_Game.CalculateScores();
            m_BoardForm.UpdatePlayersNamesAndScores();
            if (playAgain())
            {
                resetRound();
            }
            else
            {
                Environment.Exit(-1);
            }
        }

        // Generates and executes a move for the AI (Player 2)
        private void playAiTurn(object i_Sender, EventArgs i_E)
        {
            m_AiThinkingTimer.Stop();
            Move move = m_Game.PlayerTwo.GenerateRandomMove(m_Game.Board);
            m_Game.ExecuteMove(move);
        }

        // Returns true if the user chooses to play another round
        private bool playAgain()
        {
            string roundResults;

            // Check round winner
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

            // Ask for another round
            string roundResultsMessage = string.Format("{0}{1}Another Round?", roundResults, Environment.NewLine);
            DialogResult playAgainDialogResult = MessageBox.Show(roundResultsMessage, "Round ended", MessageBoxButtons.YesNo);
            bool isPlayAgain = playAgainDialogResult == DialogResult.Yes;

            return isPlayAgain;
        }

        // Event Handlers
        // Handle feedback received from last move executed (notify user of error or continue to next turn)
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
                        MessageBox.Show("Capture available, you must perform it.");
                        break;
                    }

                // case the move was successfully executed
                default:
                    {
                        m_BoardForm.HighlightCurrentPlayerLabel();
                        if (m_Game.IsOver())
                        {
                            endRound();
                        }

                        // if the AI is about to play- use a timer to pause so we can see his move
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
