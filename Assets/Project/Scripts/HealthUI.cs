using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Image _mainFill;  
    [SerializeField] private Image _delayedFill; 
    [SerializeField] private float _delaySpeed = 2f;
    [SerializeField] private float _delayPause = 0.4f;

    private float _pauseTimer;

    private void OnEnable()
    {
        _health.OnHealthChanged += UpdateUI;
        SetInstant(_health.MaxHP, _health.MaxHP);
    }

    private void OnDisable()
    {
        _health.OnHealthChanged -= UpdateUI;
    }

    private void UpdateUI(float current, float max)
    {
        _mainFill.fillAmount = current / max;
        _pauseTimer = _delayPause;
        Debug.Log(_mainFill.fillAmount);
    }

    private void Update()
    {
        if (_delayedFill.fillAmount <= _mainFill.fillAmount) return;

        if (_pauseTimer > 0f)
        {
            _pauseTimer -= Time.deltaTime;
            return;
        }

        _delayedFill.fillAmount = Mathf.MoveTowards(
            _delayedFill.fillAmount, _mainFill.fillAmount, _delaySpeed * Time.deltaTime);
    }

    private void SetInstant(float current, float max)
    {
        _mainFill.fillAmount = current / max;
        _delayedFill.fillAmount = _mainFill.fillAmount;
    }
}