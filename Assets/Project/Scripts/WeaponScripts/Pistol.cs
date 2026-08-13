using UnityEngine;
using UnityEngine.InputSystem;

public class Pistol : Weapon
{
    public override void StartFire()
    {
        Shoot();
    }

    public override void StopFire()
    {
    }
}