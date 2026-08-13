using UnityEngine;

public class DroppedWeapon : MonoBehaviour
{
    [SerializeField] private Weapon _weaponPrefab;

    public Weapon WeaponPrefab => _weaponPrefab;
}