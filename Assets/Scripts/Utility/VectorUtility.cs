using UnityEngine;

namespace Utility
{
    public static class VectorUtility
    {
        public static Vector3 RandomPointInBounds(Collider collider)
        {
            var bounds = collider.bounds;

            var randomPoint = new Vector3(Random.Range(bounds.min.x, bounds.max.x),
                                          Random.Range(bounds.min.y, bounds.max.y),
                                          Random.Range(bounds.min.z, bounds.max.z)
            );

            return collider.transform.TransformPoint(randomPoint);
        }
    }
}