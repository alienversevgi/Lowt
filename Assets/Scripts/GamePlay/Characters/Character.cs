using System;
using UnityEngine;

namespace GamePlay.Characters
{
    [RequireComponent(typeof(PathfActor))]
    public class Character : MonoBehaviour
    {
        private PathfActor _pathfActor;

        private void Awake()
        {
            _pathfActor = this.GetComponent<PathfActor>();
        }

        public virtual void Move(Vector3 point, Action onCompleted)
        {
            _pathfActor.MovePoint(point, onCompleted);
        }
    }
}