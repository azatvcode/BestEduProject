using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHP = 100f;

    public float MaxHP => _maxHP;
    public float CurrentHP { get; private set; }
    public bool IsDead => CurrentHP <= 0f;

    public event System.Action<float, float> OnHealthChanged;
    public event System.Action OnDeath;

    private void Awake()
    {
        CurrentHP = _maxHP;
    }

    public void TakeDamage(float amount)
    {
        if (IsDead) return;

        CurrentHP = Mathf.Max(0f, CurrentHP - amount);
        OnHealthChanged?.Invoke(CurrentHP, _maxHP);

        if (CurrentHP <= 0f)
            OnDeath?.Invoke();
    }

    public void Heal(float amount)
    {
        if (IsDead) return;

        CurrentHP = Mathf.Min(_maxHP, CurrentHP + amount);
        OnHealthChanged?.Invoke(CurrentHP, _maxHP);
    }
}