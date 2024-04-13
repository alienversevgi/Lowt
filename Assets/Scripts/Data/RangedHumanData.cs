using UnityEngine;

namespace GamePlay.Characters.Enemys.RangedHuman
{
    [CreateAssetMenu(fileName = nameof(RangedHumanData), menuName = "SOData/" + nameof(RangedHumanData))]
    public class RangedHumanData : CharacterData
    {
        [Header("Projectile")] public float ProjectileLifeTime;
        public float ProjectileForce;
    }
}