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

        if (_isFiring)
            Shoot();
    }
}