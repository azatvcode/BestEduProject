using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected CharacterAnimator _charAnimator;
    protected CharacterMovement _charMovement;
    protected CharacterAttacker _charAttacker;
    protected Rigidbody _charRigidbody;
    protected Collider _charCollision;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Awake()
    {
        _charAnimator = GetComponent<CharacterAnimator>();
        _charMovement = GetComponent<CharacterMovement>();
        _charAttacker = GetComponent<CharacterAttacker>();
        _charCollision = GetComponent<Collider>();
        _charRigidbody = GetComponent<Rigidbody>();
    }
}
