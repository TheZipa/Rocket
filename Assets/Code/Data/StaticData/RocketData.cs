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
        [Range(1, 10)] public float ClampAngle;
    }
}