using UnityEngine;

namespace GamePlay.Characters.Enemys
{
    [CreateAssetMenu(fileName = nameof(CharacterData), menuName = "SOData/" + nameof(CharacterData))]
    public class CharacterData : EntityData
    {
        [Header("Character")] public float Speed;
        public float HP;
        
        public bool IsDead => HP <= 0;
    }
}