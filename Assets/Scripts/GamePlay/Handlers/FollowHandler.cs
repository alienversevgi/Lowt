using System;
using System.Collections.Generic;
using GamePlay.Characters;
using UnityEngine;
using Timer = UnityTimer.Timer;

namespace GamePlay.Handler
{
    public class FollowHandler : MonoBehaviour
    {
        public static FollowHandler Instance;

        private Dictionary<Character, Timer> _followers;

        private void Awake()
        {
            Instance = this;
            _followers = new Dictionary<Character, Timer>();
        }

        public void Follow(FollowerData data)
        {
            if (_followers.ContainsKey(data.Owner))
                return;

            var timer = Timer.Register(data.CheckTime, () => RecalculateFollow(data), isLooped: true);

            _followers.Add(data.Owner, timer);
        }

        public void Ignore(Character owner)
        {
            if (!_followers.ContainsKey(owner))
                return;

            _followers[owner].Cancel();
            owner.Stop();
            _followers.Remove(owner);
        }

        private void RecalculateFollow(FollowerData data)
        {
            data.TotalFollowDuration += data.CheckTime;
            float distance = Vector3.Distance(data.Owner.transform.position, data.Target.position);

            if (distance <= data.IgnoreDistance)
            {
                Ignore(data.Owner);
                data.ReachedToIgnoreDistance?.Invoke();

            }
            else if (data.IgnoreDuration != -1 && data.TotalFollowDuration >= data.IgnoreDuration)
            {
                Ignore(data.Owner);
                data.ReachedToIgnoreDuration?.Invoke();
            }
            else
            {
                data.Owner.Move(data.Target.position, null);
            }
        }
    }

    public class FollowerData
    {
        public Character Owner;
        public Transform Target;
        public float CheckTime;
        public float IgnoreDistance;
        public float IgnoreDuration;
        public Action ReachedToIgnoreDistance;
        public Action ReachedToIgnoreDuration;
        public float TotalFollowDuration;

        public FollowerData
        (
            Character owner,
            Transform target,
            float ignoreDistance = -1,
            float ignoreDuration = -1,
            Action reachedToIgnoreDistance = null,
            Action reachedToIgnoreDuration = null,
            float checkTime = .1f
        )
        {
            Owner = owner;
            Target = target;
            CheckTime = checkTime;
            IgnoreDistance = ignoreDistance;
            IgnoreDuration = ignoreDuration;
            ReachedToIgnoreDistance = reachedToIgnoreDistance;
            ReachedToIgnoreDuration = reachedToIgnoreDuration;
            TotalFollowDuration = 0;
        }
    }
}