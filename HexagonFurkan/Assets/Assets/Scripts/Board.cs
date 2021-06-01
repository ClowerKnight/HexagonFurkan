using System.Collections.Generic;
using UnityEngine;

#region Hexagon
namespace Hexagon
{
    public class Board
    {
        #region Private/public
        private Hex[,] _board;
        public Vector2Int boardSize;
        #endregion

        #region Board
        public Board(Vector2Int boardSize)
        {
            this.boardSize = boardSize;
            _board = new Hex[this.boardSize.y, this.boardSize.x];
        }
        #endregion

        #region GetElements
        public Hex GetElement(int row, int column)
        {
            row = row / 2;
            return _board[row, column];
        }
               
        public Hex GetElement(Vector2Int position)
        {
            return GetElement(position.y, position.x);
        }
        #endregion

        #region SetElements
        public void SetElement(int row, int column, Hex hex)
        {
            hex.ChangeIndex(new Vector2Int(column, row));
            row = row / 2;
            _board[row, column] = hex;
        }

        public void SetElement(Vector2Int pos, Hex hex)
        {
            SetElement(pos.y, pos.x, hex);
        }
        #endregion

        #region IEnumerable
        public IEnumerable<Hex> GetTopRowIterator()
        {
            return new BoardTopRowIterator(this);
        }
        #endregion
    }
}
#endregion