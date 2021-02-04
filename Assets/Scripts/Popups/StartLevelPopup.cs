using UnityEngine.Assertions;

namespace Popups
{
    public class StartLevelPopup : Popup
    {
      
        private GameBoard _gameBoard;

        public void SetGameBoard(GameBoard gameBoard)
        {
            _gameBoard = gameBoard;
        }

        public void OnSinglePlayButtonClick()
        {
            Assert.IsNotNull(_gameBoard);
            _gameBoard.StartNewRound(GameMode.SinglePlayer);
            Close();
        }
        
        public void OnTwoPlayersButtonClick()
        {
            Assert.IsNotNull(_gameBoard);
            _gameBoard.StartNewRound(GameMode.TwoPlayerOnDevice);
            Close();
        }
    }
}