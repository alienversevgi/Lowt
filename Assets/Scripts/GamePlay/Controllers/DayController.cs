using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace GamePlay.Controllers
{
    public class DayController : MonoBehaviour
    {
        [SerializeField] private Color dayColor;
        [SerializeField] private Color nightColor;
        [SerializeField] private Light light;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                MakeDaylight();
            }
            
            if (Input.GetKeyDown(KeyCode.Y))
            {
                MakeNight();
            }
        }

        public void MakeDaylight()
        {
            light.DOColor(dayColor, 1f);
        }

        public void MakeNight()
        {
            light.DOColor(nightColor, 1f);
        }
    }
}