using UnityEngine;

namespace Tools
{
    public static class Extensions
    {
        public static Vector2 GetRandomDirection()
        {
            float randomX = Random.Range(-1f, 1f);
            float randomY = Random.Range(-1f, 1f);

            return new Vector2(randomX, randomY).normalized;
        }
    }
}
