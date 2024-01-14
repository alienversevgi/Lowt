using System;
using Pathfinding;
using UnityEngine;

namespace GamePlay
{
    [RequireComponent(typeof(Seeker))]
    [RequireComponent(typeof(Funnel))]
    public class PathfActor : AIPath
    {
        private Seeker _seeker;
        private Action _onComplete;
        
        protected override void Awake()
        {
            _seeker = this.GetComponent<Seeker>();
        }

        public void MovePoint(Vector3 point, Action callback)
        {
            destination = point;
            _onComplete = callback;
        }

        public override void OnTargetReached()
        {
            base.OnTargetReached();
            _onComplete?.Invoke();
        }
    }
}