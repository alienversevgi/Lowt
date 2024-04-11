using System;
using GamePlay.Components;
using UnityEngine;

namespace GamePlay.Characters
{
    [RequireComponent(typeof(PathfActor))]
    public class Character : MonoBehaviour
    {
        private PathfActor _pathfActor;

        private void Awake()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            _pathfActor = this.GetComponent<PathfActor>();
        }

        public virtual void Move(Vector3 point, Action onCompleted)
        {
            _pathfActor.MovePoint(point, onCompleted);
        }

        public virtual void Stop()
        {
            _pathfActor.Stop();
        }

        public void SetSpeed(float speed)
        {
            _pathfActor.SetSpeed(speed);
        }
    }
}