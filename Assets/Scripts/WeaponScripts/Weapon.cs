using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponData _data;
    [SerializeField] protected Transform _muzzle;
    [SerializeField] protected BulletTracer _tracerPrefab;
    [SerializeField] protected PlayerCamera _camera;
    [SerializeField] private LayerMask _hitMask = ~0;

    protected int _currentMagazine;
    protected int _currentAmmo;

    protected bool _isReloading;
    protected float _fireCooldown;

    public event System.Action<int, int> OnAmmoChanged;

    protected virtual void Awake()
    {
        if (_data == null)
        {
            Debug.LogError($"WeaponData не назначен у оружия {name}");
            return;
        }

        _currentMagazine = _data.MagazineSize;
        _currentAmmo = _data.MaxAmmo;
        OnAmmoChanged?.Invoke(_currentMagazine, _currentAmmo);
    }

    protected virtual void Update()
    {
        if (_fireCooldown > 0f)
            _fireCooldown -= Time.deltaTime;
    }

    public abstract void StartFire();

    public virtual void StopFire()
    {
    }

    public virtual void Reload()
    {
        if (_isReloading)
            return;

        if (_currentMagazine >= _data.MagazineSize)
            return;

        if (_currentAmmo <= 0)
            return;

        StartCoroutine(ReloadCoroutine());
    }

    protected virtual System.Collections.IEnumerator ReloadCoroutine()
    {
        _isReloading = true;

        yield return new WaitForSeconds(_data.ReloadTime);

        int neededAmmo = _data.MagazineSize - _currentMagazine;
        int ammoToReload = Mathf.Min(neededAmmo, _currentAmmo);

        _currentMagazine += ammoToReload;
        _currentAmmo -= ammoToReload;

        OnAmmoChanged?.Invoke(_currentMagazine, _currentAmmo);
        _isReloading = false;
    }

    protected virtual bool CanShoot()
    {
        if (_isReloading)
            return false;

        if (_fireCooldown > 0f)
            return false;

        if (_currentMagazine <= 0)
        {
            Reload();
            return false;
        }

        return true;
    }

    protected virtual void Shoot()
    {
        if (!CanShoot())
            return;

        _currentMagazine--;
        OnAmmoChanged?.Invoke(_currentMagazine, _currentAmmo);
        _fireCooldown = 1f / _data.FireRate;

        RaycastHit hit;

        Vector3 endPoint;

        if (Physics.Raycast(
            _camera.transform.position,
            _camera.transform.forward,
            out hit,
            _data.Range,
            _hitMask))
        {
            Debug.Log("Попал в: " + hit.collider.gameObject.name);

            IDamageable damageable =
                hit.collider.GetComponent<IDamageable>();

            damageable?.TakeDamage(_data.Damage);

            endPoint = hit.point;
        }
        else
        {
            Debug.Log("Промах");

            endPoint =
                _camera.transform.position +
                _camera.transform.forward * _data.Range;
        }

        SpawnTracer(
            _muzzle != null ? _muzzle.position : _camera.transform.position,
            endPoint);
    }

    protected void SpawnTracer(Vector3 start, Vector3 end)
    {
        if (_tracerPrefab == null)
            return;

        BulletTracer tracer =
            Instantiate(_tracerPrefab, start, Quaternion.identity);

        tracer.Show(start, end);
    }

    public virtual void OnEquip(Transform holder)
    {
    transform.SetParent(holder);
    
    transform.localPosition = Vector3.zero;
    transform.localRotation = Quaternion.identity;

    Rigidbody rb = GetComponent<Rigidbody>();

    if (rb != null)
        rb.isKinematic = true;

    Collider[] colliders =
        GetComponentsInChildren<Collider>();

    foreach (Collider collider in colliders)
        collider.enabled = false;

    gameObject.SetActive(true);
    }

    public virtual void OnUnequip()
    {
        StopFire();
        gameObject.SetActive(false);
    }

    public virtual void OnDropped()
    {
        transform.SetParent(null);

        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
            rb.isKinematic = false;

        Collider[] colliders =
            GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
            collider.enabled = true;
    }


    public WeaponData Data => _data;

    public int CurrentMagazine => _currentMagazine;

    public int CurrentAmmo => _currentAmmo;
}