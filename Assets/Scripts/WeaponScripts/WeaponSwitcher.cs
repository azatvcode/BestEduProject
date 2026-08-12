using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] private WeaponInventory _inventory;

    public void OnSwitchWeapon(InputValue value)
    {
        float scroll = value.Get<float>();

        if (scroll > 0f)
        {
            SwitchNext();
        }
        else if (scroll < 0f)
        {
            SwitchPrevious();
        }
    }

    private void SwitchNext()
    {
        SwitchDirection(1);
    }

    private void SwitchPrevious()
    {
        SwitchDirection(-1);
    }

    private void SwitchDirection(int direction)
    {
        if (_inventory == null)
            return;

        int currentSlot = _inventory.CurrentSlot;

        if (currentSlot < 0)
            return;

        for (int i = 1; i <= _inventory.SlotCount; i++)
        {
            int slotCount = _inventory.SlotCount;

            int nextSlot =
                (currentSlot + direction * i + slotCount) % slotCount;

            if (_inventory.GetWeapon(nextSlot) != null)
            {
                _inventory.SetActiveSlot(nextSlot);
                return;
            }
        }
    }
}