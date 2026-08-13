using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private WeaponInventory _inventory;
    [SerializeField] private TextMeshProUGUI _ammoText;

    private Weapon _currentWeapon;

    private void OnEnable()
    {
        _inventory.OnCurrentWeaponChanged += HandleWeaponChanged;
        HandleWeaponChanged(_inventory.CurrentWeapon);
    }

    private void OnDisable()
    {
        _inventory.OnCurrentWeaponChanged -= HandleWeaponChanged;
        if (_currentWeapon != null)
            _currentWeapon.OnAmmoChanged -= UpdateText;
    }

    private void HandleWeaponChanged(Weapon weapon)
    {
        if (_currentWeapon != null)
            _currentWeapon.OnAmmoChanged -= UpdateText;

        _currentWeapon = weapon;

        if (_currentWeapon == null)
        {
            _ammoText.text = "-";
            return;
        }

        _currentWeapon.OnAmmoChanged += UpdateText;
        UpdateText(_currentWeapon.CurrentMagazine, _currentWeapon.CurrentAmmo);
    }

    private void UpdateText(int magazine, int reserve)
    {
        _ammoText.text = $"{magazine} / {reserve}";
    }
}