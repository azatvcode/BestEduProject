using UnityEngine;

public class WeaponInventory : MonoBehaviour
{

    [SerializeField] private Transform _weaponHolder;
    private Weapon[] _slots = new Weapon[3];

    public int SlotCount => _slots.Length;

    private int _currentSlot = -1;

    public int CurrentSlot => _currentSlot;

    public Weapon CurrentWeapon
    {
        get
        {
            if (_currentSlot < 0)
                return null;

            return _slots[_currentSlot];
        }
    }

    public bool HasFreeSlot()
    {
        for (int i = 0; i < SlotCount; i++)
        {
            if (_slots[i] == null)
                return true;
        }

        return false;
    }

    public int GetFirstFreeSlot()
    {
        for (int i = 0; i < SlotCount; i++)
        {
            if (_slots[i] == null)
                return i;
        }

        return -1;
    }

    public void AddWeapon(Weapon weapon)
    {
        if (weapon == null)
            return;

        int slot;

        if (HasFreeSlot())
        {
            slot = GetFirstFreeSlot();
        }
        else
        {
            slot = _currentSlot;
        }

        EquipWeaponToSlot(weapon, slot);
    }

    private void EquipWeaponToSlot(Weapon weapon, int slot)
    {
        if (slot < 0 || slot >= SlotCount)
            return;

        Weapon oldWeapon = _slots[slot];

        if (oldWeapon != null)
        {
            Destroy(oldWeapon.gameObject);
        }

        _slots[slot] = weapon;

        weapon.transform.SetParent(transform);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;

        SetActiveSlot(slot);
    }

        public Weapon ReplaceCurrentWeapon(Weapon newWeapon)
    {
        if (_currentSlot < 0)
            return null;

        Weapon oldWeapon = _slots[_currentSlot];

        _slots[_currentSlot] = newWeapon;

        newWeapon.transform.SetParent(transform);
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localRotation = Quaternion.identity;

        SetActiveSlot(_currentSlot);

        return oldWeapon;
    }

    public void SetActiveSlot(int slot)
    {
        if (slot < 0 || slot >= SlotCount)
            return;

        if (_slots[slot] == null)
            return;

        if (CurrentWeapon != null)
            CurrentWeapon.OnUnequip();

        _currentSlot = slot;

        for (int i = 0; i < SlotCount; i++)
        {
            if (_slots[i] == null)
                continue;

            if (i == _currentSlot)
                _slots[i].OnEquip(_weaponHolder);
            else
                _slots[i].OnUnequip();
        }
    }

    public void DropCurrentWeapon()
    {
        if (CurrentWeapon == null)
            return;

        Weapon weapon = CurrentWeapon;

        _slots[_currentSlot] = null;
        _currentSlot = -1;

        weapon.transform.SetParent(null);
    }

    public Weapon GetWeapon(int slot)
    {
        if (slot < 0 || slot >= SlotCount)
            return null;

        return _slots[slot];
    }
}