using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using UnityTimer;

namespace GamePlay
{
    public class TestHearthCanvas : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private float duration;
        [SerializeField] private float heartDuration;
        [SerializeField] private float scaleMultipilier;
        [SerializeField] private int vibrato;
        [SerializeField] private float elasticity;
        [SerializeField] private Transform hearth;
        [SerializeField] private Color from;
        [SerializeField] private Color to;


        private Timer _timer;
        private Tweener _hearthAnim;


        private void Awake()
        {
        }

        public void StartTimer(Action onCompleted)
        {
            ResetTimer();
            _timer = Timer.Register(duration, onCompleted, OnTimerUpdate);
            PlayHearth();
        }

        public void ResetTimer()
        {
            slider.minValue = 0;
            slider.maxValue = duration;
            slider.value = 0;

            _hearthAnim.Kill();
        }

        private void OnTimerUpdate(float seconds)
        {
            slider.minValue = 0;
            slider.maxValue = duration;
            slider.value = seconds;
            var value = ConvertRange(seconds, slider.minValue, slider.maxValue, 0, 1);
            Debug.Log($"seconds: {seconds} value {value})");
            Test(value);
        }

        public float ConvertRange(float value, float oldMin, float oldMax, float newMin, float newMax)
        {
            // Normalize the value to a 0-1 range based on the old range
            float normalizedValue = (value - oldMin) / (oldMax - oldMin);

            // Scale it to the new range
            return newMin + normalizedValue * (newMax - newMin);
        }

        [Button]
        private void PlayHearth()
        {
            _hearthAnim = hearth.DOPunchScale(Vector3.one * scaleMultipilier, heartDuration, vibrato, elasticity)
                .SetLoops(-1, LoopType.Yoyo);
        }

        [Button]
        private void StopHearth()
        {
            _hearthAnim.Kill();
        }

        [Button]
        public void Test(float x)
        {
            var h = hearth.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Renderer>();
            var color = Color.Lerp(from, to, x);
            h.material.SetColor("_BaseColor", color);
        }
    }
}