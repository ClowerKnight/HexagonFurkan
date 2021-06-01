using UI;
using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

#region Hexagon
namespace Hexagon.HexCreator
{
    #region Class
    public class DefaultRandomHexCreator : RandomHexCreator
    {
        [SerializeField] 
        private int createBombForEachScore = 1000;
        [HideInInspector]
        private HexPositionCalculator _hexPositionCalculator;
        [HideInInspector]
        private int _lastBombCreatedAt = 0;
        [HideInInspector]
        private float _heightDifferenceToTop;
        

        #region Awake
        private void Awake()
        {
            _hexPositionCalculator = GameManager.instance.positionCalculator;
            Vector3 cameraTopPostion = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height, 0));
            _hexPositionCalculator = GameManager.instance.positionCalculator;
            _heightDifferenceToTop = cameraTopPostion.y - _hexPositionCalculator.lastPosition.y;
        }
        #endregion

        #region CreateHex
        public override Hex CreateHexGameObject(Vector2Int indexes)
        {
            int type = Random.Range(0, GameManager.instance.hexTypesCount);
            Vector2 position = GameManager.instance.positionCalculator.GetPosition(indexes) + new Vector3(0, _heightDifferenceToTop);
            Hex hex = Instantiate(GameManager.instance.hexPrefab, position, Quaternion.identity, transform);
            if (ScoreManager.instance.currentScore >= _lastBombCreatedAt + createBombForEachScore)
            {
                _lastBombCreatedAt += createBombForEachScore;
                hex.gameObject.AddComponent<Bomb>();
            }
            hex.Initialize(indexes, type);
            return hex;
        }
        #endregion
    }
    #endregion
}
#endregion