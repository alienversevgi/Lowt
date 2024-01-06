using UnityEngine;

namespace GamePlay.Characters
{
    public abstract class State : MonoBehaviour
    {
        public abstract void Initialize();
        public abstract void Enter();
        public abstract void Tick();
        public abstract void Exit();
    }
}