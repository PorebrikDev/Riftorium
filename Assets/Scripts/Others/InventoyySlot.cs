using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InventorySlot : MonoBehaviour
{
    public ItemSO SlotItem => _slotItem;
    public Image Icon => icon;


    public int Amount => _amount;
    [SerializeField] private int _amount;

    [SerializeField] private Image icon;

    [SerializeField] private ItemSO _slotItem;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject ItemObj;

    [SerializeField] private Transform _inventoryRoot;

    [SerializeField] private TMP_Text _amountText;

    [Inject] private readonly Inventory _inventory;
    [Inject] private readonly Iteminfo _iteminfo;


    private void Awake()
    {
        icon = gameObject.transform.GetChild(0).GetComponent<Image>();
        _amountText = gameObject.transform.GetChild(1).GetComponent<TMP_Text>();
        _button = gameObject.GetComponent<Button>();

        _button.onClick.AddListener(ShowInfo);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ShowInfo);
    }

    public void PutInSlot(ItemSO item, GameObject obj, int amount)
    {
        icon.sprite = item.icon;
        _slotItem = item;
        _amount = amount;
        icon.enabled = true;

        ItemObj = obj;

        obj.transform.SetParent(_inventoryRoot);

        UpdateAmountText();

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
        _amount = 0;
        _amountText.text = string.Empty;

        icon.sprite = null;
        icon.enabled = false;

        _inventory.NotifyInventoryChanged();
    }
    public void AddAmount(int amount)
    {
        _amount += amount;

        UpdateAmountText();

        _inventory.NotifyInventoryChanged();
    }
    private void UpdateAmountText()
    {
        _amountText.text = _amount > 1 ? _amount.ToString() : "";
    }
}