using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Events;

namespace GamePlay.Components
{
    public class SightOfHandler : MonoBehaviour
    {
        [SerializeField] private Transform rayPoint;

        public List<GameObject> SightOfObjects { get; private set; }

        public UnityEvent<GameObject> OnEnterToSight;
        public UnityEvent<GameObject> OnExitToSight;

        private void Awake()
        {
            SightOfObjects = new List<GameObject>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (IsOnSight(other))
            {
                AddToSight(other);
            }
            else
            {
                RemoveToSight(other);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            RemoveToSight(other);
        }

        private void AddToSight(Collider other)
        {
            if (!SightOfObjects.Contains(other.gameObject))
            {
                SightOfObjects.Add(other.gameObject);
                OnEnterToSight.Invoke(other.gameObject);
            }
        }
        
        private void RemoveToSight(Collider other)
        {
            if (!SightOfObjects.Contains(other.gameObject))
                return;

            SightOfObjects.Remove(other.gameObject);
            OnExitToSight.Invoke(other.gameObject);
        }

        private bool IsOnSight(Collider other)
        {
            bool isOnSight = false;
            var direction = other.transform.position - rayPoint.position;
            Ray ray = new Ray(rayPoint.position, direction);
            RaycastHit hit;
            Debug.DrawRay(ray.origin,ray.direction);
            if (Physics.Raycast(ray, out hit))
            {
                isOnSight = hit.transform.gameObject == other.gameObject;
            }

            return isOnSight;
        }
    }
}