using UnityEngine;

namespace GamePlay.Weapons
{
    public interface IProjectilable
    {
        public void Shoot(Vector3 direction, float magnitude);
    }
}