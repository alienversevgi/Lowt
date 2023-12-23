using UnityEngine;

namespace GamePlay.Character
{
    public abstract class State : MonoBehaviour
    {
        public abstract void Initialize();
        public abstract void Enter();
        public abstract void Tick();
        public abstract void Exit();
    }
}