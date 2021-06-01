using System.Collections.Generic;
using Hexagon;
using Selection;
using UnityEngine;
using Utility;

#region Controller
namespace Controller
{
    public class PlayerController : MonoBehaviour
    {
       
        #region Privates
        [SerializeField] 
        private SelectionHandler _selectionHandler;
        [HideInInspector]
        private Vector2 c;
        [HideInInspector]
        private bool _hasSelectedHexagon;
        [HideInInspector]
        private HexPositionCalculator _hexPositionCalculator;
        [HideInInspector]
        private Vector2 _firstHexPosition,
        _lastHexPosition,
        _selectedHexPosition;
        [HideInInspector]
        private Camera _camera;
        #endregion

        #region Start
        void Start()
        {
            _hasSelectedHexagon = false;
            _camera = Camera.main;
            _hexPositionCalculator = GameManager.instance.positionCalculator;
            _firstHexPosition = _hexPositionCalculator.firstPosition;
            _lastHexPosition = _hexPositionCalculator.lastPosition;

            TouchDetector.Swipe += OnSwipe;
            TouchDetector.OnClick += OnClick;
        }
        #endregion

        #region Swipe
        void OnSwipe(SwipeDirection direction, Vector2 a, Vector2 b)
        {
            if (_hasSelectedHexagon)
            {
                bool clockwise = ((b.x - a.x)*(c.y - a.y) - (b.y - a.y)*(c.x - a.x)) < 0;
                if (clockwise)
                {
                    _selectionHandler.RotateClockwise();
                }
                else
                {
                    _selectionHandler.RotateCounterClockwise();
                }
            }
        }
        #endregion

        #region Click
        void OnClick()
        {
            Vector2 mousePositionWorldPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
            if (mousePositionWorldPoint.x < _firstHexPosition.x || mousePositionWorldPoint.y > _firstHexPosition.y ||
                mousePositionWorldPoint.x > _lastHexPosition.x || mousePositionWorldPoint.y < _lastHexPosition.y)
            {
                return;
            }
            
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hitInfo = Physics2D.Raycast(_camera.ScreenToWorldPoint(mousePosition), Vector2.zero);
            
            if(hitInfo)
            {
                Hex hex = hitInfo.transform.gameObject.GetComponent<Hex>();
                
                HexagonVertexes hexagonVertex = hex.GetSelectionSide(mousePositionWorldPoint);
                hexagonVertex = HandleEdgeCases(hex, hexagonVertex);
                if (_selectionHandler.SelectHex(hex, hexagonVertex, out c))
                { 
                    _hasSelectedHexagon = true;
                }
            }
        }
        #endregion

        #region Vertexs
        HexagonVertexes HandleEdgeCases(Hex hex, HexagonVertexes hexagonVertex)
        {
            HexagonVertexes outHexagonVertex = hexagonVertex;
            foreach(var item in new SelectionAreaIterator(outHexagonVertex))
            {
                outHexagonVertex = item;
                if (HasRequiredNeighbors(hex, outHexagonVertex))
                {
                    break;
                }
            }
            return outHexagonVertex;
        }
        #endregion

        #region Neighbors
        bool HasRequiredNeighbors(Hex hex, HexagonVertexes hexagonVertex)
        {
            List<HexagonEdges> requiredNeighbors = hexagonVertex.GetNeighborEdges();
            foreach (var hexSide in requiredNeighbors)
            {
                if (!hex.HasNeighborHex(hexSide))
                {
                    return false;
                }
            }
            return true;
        }
        
    }
    #endregion
}
#endregion