using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hexagon;

namespace Selection
{
    public class SelectionAreaIterator : IEnumerable
    {
        #region Inspector
        [HideInInspector]
        private HexagonVertexes _hexagonVertex;
        [HideInInspector]
        private int count = 0;
        #endregion

        #region SelectionAreaIterator-IEnumerator
        public SelectionAreaIterator(HexagonVertexes hexagonVertex)
        {
            _hexagonVertex = hexagonVertex;
        }


        public IEnumerator<HexagonVertexes> GetEnumerator()
        {
            HexagonVertexes clockwise = _hexagonVertex,
                counterClockwise = _hexagonVertex;
            yield return _hexagonVertex;
            for (count = 0; count < 5; count++)
            {
                if (count % 2 == 0)
                {
                    clockwise = clockwise.Next();
                    yield return clockwise;
                }
                else
                {
                    counterClockwise = counterClockwise.Next(false);
                    yield return counterClockwise;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    #endregion
}