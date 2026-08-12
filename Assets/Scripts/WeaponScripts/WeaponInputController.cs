using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponInputController : MonoBehaviour
{
    [SerializeField] private WeaponInventory _inventory;
    [SerializeField] private InputActionAsset _inputActions;

    private InputAction _fireAction;

        private void Awake()
    {
        _fireAction = _inputActions.FindAction("Fire");
    }

    private void OnEnable()
    {
        _fireAction.started += OnFireStarted;
        _fireAction.canceled += OnFireCanceled;

        _fireAction.Enable();
    }

    private void OnDisable()
    {
        _fireAction.started -= OnFireStarted;
        _fireAction.canceled -= OnFireCanceled;

        _fireAction.Disable();
    }

    private void OnFireStarted(InputAction.CallbackContext context)
    {
        Weapon weapon = _inventory.CurrentWeapon;

        if (weapon == null)
            return;

        weapon.StartFire();
    }

    private void OnFireCanceled(InputAction.CallbackContext context)
    {
        Weapon weapon = _inventory.CurrentWeapon;

        if (weapon == null)
            return;

        weapon.StopFire();
    }

    public void OnReload(InputValue value)
    {
        if (!value.isPressed)
            return;

        Weapon weapon = _inventory.CurrentWeapon;

        if (weapon == null)
            return;

        weapon.Reload();
    }
}