using UnityEngine;

namespace GamePlay
{
    public abstract class Droper : MonoBehaviour
    {
        public DropItem ItemPrefab;
        
        public abstract void Drop();
    }
}