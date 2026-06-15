using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TapSlotHot : MonoBehaviour
{
    [SerializeField] private int _buttonIndex;
    [SerializeField] private Image _image;

    [Inject] private readonly InputService _input;
    [Inject] private readonly Inventory _inventory;
    [Inject] private readonly UseOfItems _useOfItems;

    private Button _clickButton;

    private void Awake()
    {
        _clickButton = GetComponent<Button>();
        _clickButton.onClick.AddListener(OnButtonClicked);
        _input.OnNumberKeyPressed += ClickHotSlot;
        _image = transform.GetChild(0).GetComponent<Image>();
    }

    private void Start()
    {
        _inventory.OnInventoryChanged += UpdateIcon;

        UpdateIcon();
    }

    private void OnDestroy()
    {
        _input.OnNumberKeyPressed -= ClickHotSlot;
        _clickButton.onClick.RemoveListener(OnButtonClicked);
        _inventory.OnInventoryChanged -= UpdateIcon;
    }

    private void UpdateIcon()
    {
        InventorySlot slot = _inventory.InventorySlots[_buttonIndex];

        if (slot == null || slot.SlotItem == null)
        {
            _image.sprite = null;
            _image.enabled = false;
            return;
        }

        _image.sprite = slot.Icon.sprite;
        _image.enabled = true;
    }

    private void OnButtonClicked()
    {
        ClickHotSlot(_buttonIndex);
    }

    private void ClickHotSlot(int index)
    {
        if (index != _buttonIndex)
            return;

        InventorySlot slot = _inventory.InventorySlots[_buttonIndex];

        if (slot == null || slot.SlotItem == null)
        {
            _image.enabled = false;
            return;
        }

        _image.sprite = slot.Icon.sprite;
        _image.enabled = true;

        _useOfItems.Use(slot.SlotItem);
    }
}
