using System;
using UnityEngine;
using Zenject;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public event Action OnInventoryChanged;
    public InventorySlot[] InventorySlots => _inventorySlots;

    [Inject] private readonly InputService _input;
    [Inject] private readonly Iteminfo _iteminfo;

    private InventorySlot[] _inventorySlots = new InventorySlot[14];
    public Transform _slotsParent;

    private bool _isInventory;

    public bool IsInventory
    {
        get => _isInventory;

        set
        {
            _isInventory = value;

            if (_isInventory)
            {
                gameObject.SetActive(true);
                _input.DisableCombat();
            }
            else
            {
                gameObject.SetActive(false);
                _input.EnableCombat();
            }
        }
    }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        { Destroy(gameObject); }

        Instance = this;
    }

    private void Start()
    {
        IsInventory = true;
        InventoryOpenClose();

        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            _inventorySlots[i] = _slotsParent.GetChild(i).GetComponent<InventorySlot>();
        }
    }

    public void PutInEmptySlot(ItemSO item, GameObject obj)
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            if (_inventorySlots[i].SlotItem == null)
            {
                _inventorySlots[i].PutInSlot(item, obj);

                NotifyInventoryChanged();
                return;
            }
        }
    }

    public void NotifyInventoryChanged() => OnInventoryChanged?.Invoke();

    public void InventoryOpenClose()
    {
        if (IsInventory)
        {
            IsInventory = false;
            if (_iteminfo != null)
            {
                _iteminfo.Close();
            }
        }

        else
        {
            IsInventory = true;
        }
    }
}

