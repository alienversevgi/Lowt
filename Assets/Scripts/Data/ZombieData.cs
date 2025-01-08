using UnityEngine;

namespace GamePlay.Characters.Enemys
{
    [CreateAssetMenu(fileName = nameof(ZombieData), menuName = "SOData/" + nameof(ZombieData))]
    public class ZombieData : CharacterData
    {
        [Header("Zombie")] public float ChaseSpeed;

        public int EntityDamage;
        public int BuildingDamage;
        public float ChaseIgnoreDuration;
        public float DestructRate;
        public float AttackRange;
        public float AttackMoveUnit;
        public float AttackMoveSpeed;
        public float StartSearchTime;
    }
}