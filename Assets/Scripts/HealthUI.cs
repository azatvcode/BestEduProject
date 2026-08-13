using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _hpText; 

    private void OnEnable()
    {
        _playerHealth.OnHealthChanged += UpdateUI;
        UpdateUI(_playerHealth.MaxHP, _playerHealth.MaxHP);
    }

    private void OnDisable()
    {
        _playerHealth.OnHealthChanged -= UpdateUI;
    }

    private void UpdateUI(float current, float max)
    {
        _slider.maxValue = max;
        _slider.value = current;
        if (_hpText != null)
            _hpText.text = $"{current:0} / {max:0}";
    }
}