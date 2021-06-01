using UnityEngine;

namespace Hexagon.HexCreator
{
    #region CreateHex
    public abstract class RandomHexCreator : MonoBehaviour
    {
        public abstract Hex CreateHexGameObject(Vector2Int indexes);
    }
    #endregion
}