using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("General")]
    [SerializeField] private string _weaponName;

    [Header("Damage")]
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _range = 100f;

    [Header("Fire")]
    [SerializeField] private float _fireRate = 2f;

    [Header("Magazine")]
    [SerializeField] private int _magazineSize = 12;
    [SerializeField] private int _maxAmmo = 60;
    [SerializeField] private float _reloadTime = 1.5f;

    public string WeaponName => _weaponName;
    public float Damage => _damage;
    public float Range => _range;
    public float FireRate => _fireRate;
    public int MagazineSize => _magazineSize;
    public int MaxAmmo => _maxAmmo;
    public float ReloadTime => _reloadTime;
}