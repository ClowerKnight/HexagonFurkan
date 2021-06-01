using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Hexagon
{
    #region BoardTopRowIterator
    public class BoardTopRowIterator : IEnumerable<Hex>
    {
        [SerializeField]
        private Board _board;

        #region BoardTopRowIterator
        public BoardTopRowIterator(Board board)
        {
            _board = board;
        }
        #endregion

        #region IEnumerator
        public IEnumerator<Hex> GetEnumerator()
        {
            for (int column = 0; column < _board.boardSize.x; column++)
            {
                int row = column % 2 == 1 ? 1 : 0;
                yield return _board.GetElement(row, column);
            }
        }
        #endregion

        #region IEnumerable.GetEnumerator
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
#endregion