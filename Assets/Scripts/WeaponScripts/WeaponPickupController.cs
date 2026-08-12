using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponPickupController : MonoBehaviour
{
    [SerializeField] private WeaponInventory _inventory;
    [SerializeField] private Transform _pickupPoint;

    private WeaponPickup _nearbyPickup;

    private void OnTriggerEnter(Collider other)
    {
        WeaponPickup pickup =
            other.GetComponent<WeaponPickup>();

        if (pickup == null)
            return;

        _nearbyPickup = pickup;
    }

    private void OnTriggerExit(Collider other)
    {
        WeaponPickup pickup =
            other.GetComponent<WeaponPickup>();

        if (pickup == null)
            return;

        if (_nearbyPickup == pickup)
            _nearbyPickup = null;
    }

    public void OnPickup(InputValue value)
    {
        if (!value.isPressed)
            return;

        if (_nearbyPickup == null)
            return;

        PickupWeapon(_nearbyPickup);
    }

   private void PickupWeapon(WeaponPickup pickup)
    {
        Weapon newWeapon = pickup.CreateWeapon();

        if (_inventory.HasFreeSlot())
        {
            _inventory.AddWeapon(newWeapon);
        }
        else
        {
            Weapon oldWeapon = _inventory.ReplaceCurrentWeapon(newWeapon);

            DropWeapon(oldWeapon);
        }

        Destroy(pickup.gameObject);
    }

    private void DropWeapon(Weapon weapon)
    {
        weapon.gameObject.SetActive(true);

        weapon.transform.SetParent(null);

        weapon.transform.position =
            transform.position + transform.forward;

        weapon.transform.rotation =
            Quaternion.identity;

        Rigidbody rb =
            weapon.GetComponent<Rigidbody>();

        if (rb != null)
            rb.isKinematic = false;

        Collider[] colliders =
            weapon.GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
            collider.enabled = true;
    }
}