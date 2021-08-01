using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Union.Services.Inputs
{
    public sealed class PlayerTwoAxisAction : IActionProvider
    {
        string _horizontalKey;
        string _verticalKey;

        float _stateThreshold;

        public PlayerTwoAxisAction(string horizontal, string vertical, float stateThreshold)
        {
            _horizontalKey = horizontal;
            _verticalKey = vertical;

            _stateThreshold = stateThreshold;
        }

        public float X { get; private set; }
        public float Y { get; private set; }

        public void Update()
        {
            X = 0;
            Y = 0;

            var tempX = Input.GetAxis(_horizontalKey);
            var tempY = Input.GetAxis(_verticalKey);

            if (Mathf.Abs(tempX) > _stateThreshold)
                X = tempX;

            if (Mathf.Abs(tempY) > _stateThreshold)
                Y = tempY;
        }
    }
}

