using UI;
using UnityEngine;

namespace Hexagon
{
    public class Bomb : MonoBehaviour
    {
        #region Inspector
        [HideInInspector]
        private BombWidget _bombWidget;
        [HideInInspector]
        private int roundLeft = 7;
        [HideInInspector]
        private bool _skipFirstPlayerActionEnd = false;
        #endregion


        #region Start
        private void Start()
        {
            Vector2 hexagonSize = GameManager.instance.positionCalculator.hexagonSize;
            Vector3 centerPosition = transform.position + new Vector3(hexagonSize.x / 2, -hexagonSize.y / 2) + GameManager.instance.bombWidgetPrefab.transform.position; 
            _bombWidget = Instantiate(GameManager.instance.bombWidgetPrefab, centerPosition, Quaternion.identity, transform);
            _bombWidget.bombText.text = roundLeft.ToString();

            GameManager.instance.OnPlayerActionEnd += OnPlayerActionEnd;
            _skipFirstPlayerActionEnd = !GameManager.instance.canTakeAction;
        }
        #endregion

        #region ActionEnd
        private void OnPlayerActionEnd()
        {
            if (_skipFirstPlayerActionEnd)
            {
                _skipFirstPlayerActionEnd = false;
                return;
            }
            roundLeft--;
            _bombWidget.bombText.text = roundLeft.ToString();
            if (roundLeft <= 0)
            {
                GameManager.instance.TriggerGameOver();
                GameManager.instance.OnPlayerActionEnd -= OnPlayerActionEnd;
            }
        }
        #endregion

        #region Destroy
        private void OnDestroy()
        {
            GameManager.instance.OnPlayerActionEnd -= OnPlayerActionEnd;
        }
        #endregion
    }
}