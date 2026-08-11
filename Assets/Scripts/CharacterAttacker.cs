using UnityEngine;

public class CharacterAttacker : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _cooldown = 1f;

    private float _cooldownTimer;

    void Update()
    {
        if (_cooldownTimer > 0f)
        {
            _cooldownTimer -= Time.deltaTime;
        }
    }

    public void Attack(GameObject target)
    {
        if (_cooldownTimer > 0f) return;

        IDamageable damageable = target.GetComponent<IDamageable>();
        damageable?.TakeDamage(_damage);

        _cooldownTimer = _cooldown;
    }
}