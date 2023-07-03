using System;
using UnityEngine;

namespace Code.Data.StaticData
{
    [Serializable]
    public class RocketData
    {
        public float MaxRocketSpeed;
        public float MaxFuel;
        public float ConsumeCoefficient;
        public float RestoreDelay;
        [Range(1, 10)] public float ClampAngle;
    }
}