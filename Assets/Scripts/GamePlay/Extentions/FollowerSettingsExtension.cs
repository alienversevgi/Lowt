using GamePlay.Handler;
using UnityEngine;
using UnityEngine.Events;

namespace GamePlay.Extentions
{
    public static class FollowerSettingsExtension
    {
        public static FollowerData SetTarget(this FollowerData data, Transform target)
        {
            if (data == null)
                return data;
            
            data.Target = target;

            return data;
        }

        public static FollowerData OnMoveStarted(this FollowerData data, UnityAction action)
        {
            if (data == null)
                return data;

            data.OnMoveStarted.AddListener(action);
            return data;
        }

        public static FollowerData OnMoving(this FollowerData data, UnityAction action)
        {
            if (data == null)
                return data;

            data.OnMoving.AddListener(action);
            return data;
        }

        public static FollowerData OnReachedToIgnoreDuration(this FollowerData data, UnityAction action)
        {
            if (data == null)
                return data;

            data.OnReachedToIgnoreDuration.AddListener(action);
            return data;
        }

        public static FollowerData OnReachedToIgnoreDistance(this FollowerData data, UnityAction action)
        {
            if (data == null)
                return data;

            data.ReachedToIgnoreDistance.AddListener(action);
            return data;
        }
    }
}