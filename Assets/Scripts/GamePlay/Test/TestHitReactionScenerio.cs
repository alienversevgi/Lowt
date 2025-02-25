using System.Collections.Generic;
using System.Linq;
using GamePlay.Characters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay
{
    public class TestHitReactionScenario : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private List<TestDamageDealer> damageDealers;
        
        private void Awake()
        {
            damageDealers = this.GetComponentsInChildren<TestDamageDealer>().ToList();
            for (int i = 0; i < damageDealers.Count; i++)
            {
                damageDealers[i].Initialize(player,ResetPosition);   
            }
        }

        [Button]
        public void ResetPosition()
        {
            player.transform.position = this.transform.position;
        }
    }
}