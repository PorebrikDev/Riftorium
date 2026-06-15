using UnityEngine;

namespace Game.Core.Math
{
    public static class RandomUtils
    {
        public static Vector2 RandomDirection2D()
        {
            return Random.insideUnitCircle.normalized;
        }
    }
}