using UnityEngine;

public class Rifle : Weapon
{
    private bool _isFiring;

    public override void StartFire()
    {
        _isFiring = true;
    }

    public override void StopFire()
    {
        _isFiring = false;
    }

    protected override void Update()
    {
        base.Update();
        Debug.Log(_isFiring);

        if (_isFiring)
            Shoot();
    }
}