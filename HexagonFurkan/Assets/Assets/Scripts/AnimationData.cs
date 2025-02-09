﻿using UnityEngine;

namespace Utility
{
    public class AnimationData : MonoBehaviour
    {
        #region Inspector
        [SerializeField] 
        private float _delayAfterExplosion = 1f;
        [SerializeField] 
        private float _delayAfterRound = 1f;
        [SerializeField] 
        private float _hexMoveSpeed = 8f;
        [SerializeField] 
        private float _hexMoveDelayForEachRow = 0.1f;
        [SerializeField] 
        private float _rotationSpeed = 120f;
        [SerializeField] 
        private float _delayAfterRotation = 1f;
        #endregion

        #region Public
        public static float delayAfterExplosion,
            delayAfterRound,
            delayAfterRotation,
            hexMoveSpeed,
            hexMoveDelayForEachRow,
            rotationSpeed,
            rotationAmount = 120f;
        #endregion

        #region Awake
        private void Awake()
        {
            delayAfterExplosion = _delayAfterExplosion;
            delayAfterRotation = _delayAfterRotation + rotationAmount / _rotationSpeed;
            delayAfterRound = _delayAfterRound;
            hexMoveSpeed = _hexMoveSpeed;
            hexMoveDelayForEachRow = _hexMoveDelayForEachRow;
            rotationSpeed = _rotationSpeed;
        }
        #endregion
    }
}