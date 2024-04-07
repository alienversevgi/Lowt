using UnityEngine;

namespace GamePlay.Characters.Enemys
{
    [CreateAssetMenu(fileName = nameof(ZombieData), menuName = "SOData/" + nameof(ZombieData))]
    public class ZombieData : ScriptableObject
    {
        [Header("General")] public float Speed;
    }
}