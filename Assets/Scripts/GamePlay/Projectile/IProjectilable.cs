using UnityEngine;

namespace GamePlay.Projectile
{
    public interface IProjectilable
    {
        public void Fire(Vector3 direction, float magnitude);
    }
}