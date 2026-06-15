using UnityEngine;
using UnityEngine.UI;

public class HotBatInventory : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Image[] _hotbarIcons;

    private void Start()
    {
        _inventory.OnInventoryChanged += UpdateHotbar;
    }

    private void OnDestroy()
    {
        _inventory.OnInventoryChanged -= UpdateHotbar;
    }

    private void UpdateHotbar()
    {
        for (int i = 0; i < _hotbarIcons.Length; i++)
        {
            var slot = _inventory.InventorySlots[i];
            if (slot.SlotItem != null)
            {
                _hotbarIcons[i].sprite = slot.SlotItem.icon;
                _hotbarIcons[i].enabled = true;
            }

            else 
            {
                _hotbarIcons[i].sprite = null;
                _hotbarIcons[i].enabled = false;
            }
        }
    }
}
