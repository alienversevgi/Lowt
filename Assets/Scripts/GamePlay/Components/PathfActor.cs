using System;
using Pathfinding;
using UnityEngine;

namespace GamePlay.Components
{
    [RequireComponent(typeof(FunnelModifier))]
    public class PathfActor : AIPath
    {
        private Seeker _seeker;
        private Action _onComplete;
        
        protected override void Awake()
        {
            _seeker = this.GetComponent<Seeker>();
            canMove = false;
        }

        public void MovePoint(Vector3 point, Action callback)
        {
            destination = point;
            _onComplete = callback;
            canMove = true;
        }

        public override void OnTargetReached()
        {
            base.OnTargetReached();
            _onComplete?.Invoke();
            _onComplete = null;
            canMove = false;
        }
    }
}