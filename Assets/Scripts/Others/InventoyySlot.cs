using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InventorySlot : MonoBehaviour
{
    public ItemSO SlotItem => _slotItem;
    public Image Icon => icon;

    [SerializeField] private Image icon;

    [SerializeField] private ItemSO _slotItem;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject ItemObj;

    [SerializeField] private Transform _inventoryRoot;

    [Inject] private readonly Inventory _inventory;
    [Inject] private readonly Iteminfo _iteminfo;

    private void Awake()
    {
        icon = gameObject.transform.GetChild(0).GetComponent<Image>();
        _button = gameObject.GetComponent<Button>();

        _button.onClick.AddListener(ShowInfo);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ShowInfo);
    }

    public void PutInSlot(ItemSO item, GameObject obj)
    {
        icon.sprite = item.icon;
        _slotItem = item;
        icon.enabled = true;

        ItemObj = obj;

        obj.transform.SetParent(_inventoryRoot);

        _inventory.NotifyInventoryChanged();
    }

    public void ShowInfo()
    {
        if (_slotItem != null)
        {
            _iteminfo.Open(_slotItem, ItemObj, this);
        }
        else { _iteminfo.Close(); }
    }

    public void ClearSlot()
    {
        _slotItem = null;
        ItemObj = null;

        icon.sprite = null;
        icon.enabled = false;

        _inventory.NotifyInventoryChanged();
    }
}