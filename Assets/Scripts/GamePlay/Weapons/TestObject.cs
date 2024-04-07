using Unity.VisualScripting;
using UnityEngine;

namespace GamePlay.Weapons
{
    public class TestObject : MonoBehaviour, IDamagable
    {
        public int Health;

        public void ApplyDamage(int value)
        {
            Health -= value;
            Debug.Log($"Take Damage : {Health}");

            if (Health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            Debug.Log("Died");
        }
    }
}