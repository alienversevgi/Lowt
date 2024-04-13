using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RagdollEnabler : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody[] rigidbodies;
    private Collider[] colliders;

    private bool _isEnable;
    private void Awake()
    {
        _animator = this.GetComponent<Animator>();
        rigidbodies = this.GetComponentsInChildren<Rigidbody>();
        colliders = this.GetComponentsInChildren<Collider>();
        SetEnable(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetEnable(!_isEnable);
        }
    }

    public void SetEnable(bool isEnable)
    {
        _isEnable = isEnable;
        _animator.enabled = !isEnable;
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].useGravity = isEnable;
            rigidbodies[i].isKinematic = !isEnable;
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = isEnable;
        }
    }

    public void Enable()
    {
        SetEnable(true);
    }

    public void Disable()
    {
        SetEnable(false);
    }
}