using UnityEngine;

namespace GamePlay.Characters.Enemys.RangedHuman
{
    [CreateAssetMenu(fileName = nameof(RangedHumanData), menuName = "SOData/"+nameof(RangedHumanData))]
    public class RangedHumanData : ScriptableObject
    {
        [Header("General")] public float Speed;
        
        [Header("Projectile")]
        public float ProjectileLifeTime;
        public float ProjectileForce;
    }
}