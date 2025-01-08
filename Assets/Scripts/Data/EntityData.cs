using UnityEngine;

namespace GamePlay.Characters.Enemys
{
    [CreateAssetMenu(fileName = nameof(EntityData), menuName = "SOData/" + nameof(EntityData))]
    public class EntityData : ScriptableObject
    {
        [Header("Entity")]
        public string ShortCode;
        public float DamageDistance;
    }
}