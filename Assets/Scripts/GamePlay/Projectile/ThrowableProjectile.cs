using UnityEngine;

namespace GamePlay.Projectile
{
    public class ThrowableProjectile : MonoBehaviour, IProjectilable
    {
        [SerializeField] private float lifeTime;

        private Rigidbody _rigidBody;

        private void Awake()
        {
            _rigidBody = this.GetComponent<Rigidbody>();
        }

        public void Fire(Vector3 direction, float magnitude)
        {
            direction = direction.normalized;
            direction.y = .5f;
            Vector3 forcePosition = this.transform.position - new Vector3(0f, .5f, 0f);

            _rigidBody.AddForceAtPosition(direction * magnitude, forcePosition, ForceMode.Force);
        }
    }
}