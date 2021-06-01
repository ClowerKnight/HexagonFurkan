using UnityEngine;


namespace Hexagon
{
    public class Hex : MonoBehaviour
    {
        #region Inspector
        [HideInInspector]
        public Vector2Int index { get; private set; }
        [HideInInspector]
        public int hexType { get; private set; }
        [HideInInspector]
        public HexAnimator animator;
        [SerializeField] 
        private SpriteRenderer _spriteRenderer;
        #endregion

        #region Initialize-ChangeIndex-HasNeighborHex-HexagonVertexes-GetNeighbor
        public void Initialize(Vector2Int index, int hexType)
        {
            this.index = index;
            this.hexType = hexType;
            
            _spriteRenderer.sprite = GameManager.instance.hexSprites[hexType];
        }

        public void ChangeIndex(Vector2Int index)
        {
            this.index = index;
        }

        public bool HasNeighborHex(HexagonEdges hexagonEdges)
        {
            Vector2Int boardSize = GameManager.instance.positionCalculator.boardSize;
            // Ilk donus eslesme yok.
            if (index.y == 0 && (hexagonEdges == HexagonEdges.TopLeft || 
                                    hexagonEdges == HexagonEdges.Top ||
                                    hexagonEdges == HexagonEdges.TopRight))
            {
                return false;
            }
            // Ikinci donus eslesme yok.
            if (index.y == 1 && (hexagonEdges == HexagonEdges.Top))
            {
                return false;
            }
            // Ucuncu donus eslesme yok.
            if (index.y >= boardSize.y * 2 - 1 && (hexagonEdges == HexagonEdges.BottomLeft || 
                                                           hexagonEdges == HexagonEdges.Bottom || 
                                                           hexagonEdges == HexagonEdges.BottomRight))
            {
                return false;
            }
            // Alt ikinci satir eslesme yok.
            if (index.y == boardSize.y * 2 - 2 && (hexagonEdges == HexagonEdges.Bottom))
            {
                return false;
            }
            // Ilk sutun eslesme yok.
            if (index.x == 0 && (hexagonEdges == HexagonEdges.BottomLeft || 
                                    hexagonEdges == HexagonEdges.TopLeft))
            {
                return false;
            }
            // Son sutun sag komsular yok.
            if (index.x == boardSize.x - 1 && (hexagonEdges == HexagonEdges.BottomRight || 
                                                  hexagonEdges == HexagonEdges.TopRight))
            {
                return false;
            }
            return true;
        }
        
        public HexagonVertexes GetSelectionSide(Vector2 mousePos)
        {
            // Hexagonun 6 kenari.
            Vector2 spriteSize = GameManager.instance.positionCalculator.hexagonSize;
            Vector2 centerPos = (Vector2) transform.position + new Vector2(spriteSize.x / 2, -spriteSize.y / 2 ),
                collisionBox = new Vector2(spriteSize.x / 2, spriteSize.y / 6);
            

            if (mousePos.y >= centerPos.y + collisionBox.y)
            {
                if (mousePos.x >= centerPos.x)
                {
                    return HexagonVertexes.TopRight;
                }
                else
                {
                    return HexagonVertexes.TopLeft;
                }
            } else if (mousePos.y >= centerPos.y - collisionBox.y)
            {
                if (mousePos.x >= centerPos.x)
                {
                    return HexagonVertexes.Right;
                }
                else
                {
                    return HexagonVertexes.Left;
                }
            }
            else
            {
                if (mousePos.x >= centerPos.x)
                {
                    return HexagonVertexes.BottomRight;
                }
                else
                {
                    return HexagonVertexes.BottomLeft;
                }
            }
        }

        public Hex GetNeighbor(HexagonEdges hexagonEdges)
        {
            Vector2Int neighborPosition = index + hexagonEdges.GetVector();
            return GameManager.instance.GetElement(neighborPosition);
        }
    }
    #endregion
}