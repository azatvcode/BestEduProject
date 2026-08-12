using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon _weaponPrefab;

    public Weapon WeaponPrefab => _weaponPrefab;

    public Weapon CreateWeapon()
    {
        return Instantiate(_weaponPrefab);
    }
}