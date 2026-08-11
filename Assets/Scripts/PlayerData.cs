using System;
using UnityEngine;

[Serializable]
public class PlayerData : MonoBehaviour, IDamageable
{
    [SerializeField] private int id;
    [SerializeField] private string playerName;
    [SerializeField] private float maxHP = 100f;

    public float CurrentHP { get; private set; }

    void Awake()
    {
        CurrentHP = maxHP;
    }

    public void TakeDamage(float amount)
    {
        CurrentHP = Mathf.Max(0f, CurrentHP - amount);
        Debug.Log($"Игрок получил {amount} урона. HP: {CurrentHP}/{maxHP}");

        if (CurrentHP <= 0f)
        {
            Debug.Log("Игрок погиб");
        }
    }
}