using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CheckersWinForms
{
    public class WinFormsUiManager
    {
        // Private Members
        private static Timer s_AiThinkingTimer;
        private GameSettingsForm m_GameSettingsForm;
        private BoardForm m_BoardForm;
        private Game m_Game;

        // Constructors
        public WinFormsUiManager()
        {
            m_GameSettingsForm = new GameSettingsForm();
        }

        // Public Methods
        public void CreateGame()
        {
            if (m_GameSettingsForm.ShowDialog() == DialogResult.OK)
            {
                string playerOneName = m_GameSettingsForm.TextBoxPlayerOneName;
                string playerTwoName = m_GameSettingsForm.TextBoxPlayerTwoName;

                m_Game = new Game(playerOneName, playerTwoName, m_GameSettingsForm.SelectedBoardSize, m_GameSettingsForm.AiMode);
            }
        }

        public void StartGame()
        {
            m_Game.TurnCount = 0;
            m_Game.Board.Build();
            m_Game.SetPiecesStartingPositions();
            m_Game.MoveExecuted += AnalyzeMoveFeedback;
            m_BoardForm = new BoardForm(m_Game);
            if(m_Game.AiMode)
            {
                s_AiThinkingTimer = new Timer { Interval = 1000 };
                s_AiThinkingTimer.Tick += playAiTurn;
            }

            m_BoardForm.ShowDialog();
        }

        private void playAiTurn(object i_Sender, EventArgs i_E)
        {
            s_AiThinkingTimer.Stop();
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
            m_BoardForm.UpdatePlayersNames();
            if (playAgain())
            {
                resetRound();
            }
        }

        private bool playAgain()
        {
            string roundResults;
            if(m_Game.PlayerOne.TotalScore > m_Game.PlayerTwo.TotalScore)
            {
                roundResults = string.Format("{0} Won!", m_Game.PlayerOne.Name);
            }
            else if(m_Game.PlayerOne.TotalScore < m_Game.PlayerTwo.TotalScore)
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
                case eMoveFeedback.Success:
                    {
                        m_BoardForm.HighlightCurrentPlayerLabel();
                        if (m_Game.IsOver())
                        {
                            endRound();
                        }

                        if(m_Game.CurrentPlayer.IsAi)
                        {
                            s_AiThinkingTimer.Start();
                        }

                        break;
                    }

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

                case eMoveFeedback.Quit:
                    {
                        endRound();
                        break;
                    }
            }
        }
    }
}
