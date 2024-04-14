using UnityEngine;

namespace GamePlay.Weapons
{
    public class DamageData
    {
        public GameObject Owner;
        public int Amount;

        public DamageData()
        {
        }

        public DamageData(GameObject owner, int amount)
        {
            Owner = owner;
            Amount = amount;
        }
    }
}