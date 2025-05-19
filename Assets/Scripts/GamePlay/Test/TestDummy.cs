using System;
using GamePlay.Characters;
using GamePlay.Weapons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay
{
    public class TestDummy : Enemy, IDamageable, IFocusable
    {
        [SerializeField] private GameObject hearth;
        [SerializeField] private TestHearthCanvas canvas;

        public bool FocusCompleted { get; set; }

        public void ApplyDamage(DamageData data)
        {
            Debug.Log($"{data.Owner.name} damaged to {name}");
        }

        [Button]
        public void FocusStarted()
        {
            FocusCompleted = false;
            canvas.StartTimer(() => FocusCompleted = true);
        }

        [Button]
        public void FocusEnded()
        {
            FocusCompleted = false;
            canvas.ResetTimer();
        }
    }

    public interface IFocusable
    {
        bool FocusCompleted { get; set; }
        void FocusStarted();
        void FocusEnded();
    }
}