using UnityEngine;

namespace GamePlay.Characters.Enemys
{
    [CreateAssetMenu(fileName = nameof(ZombieData), menuName = "SOData/" + nameof(ZombieData))]
    public class ZombieData : CharacterData
    {
        [Header("Zombie")] public float ChaseSpeed;
        public float ChaseIgnoreDuration;
        public float AttackRange;
        public float AttackMoveUnit;
        public float AttackMoveSpeed;
        public float StartSearchTime;
    }
}