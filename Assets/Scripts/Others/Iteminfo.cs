using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


public class Iteminfo : MonoBehaviour
{
    [Inject] private readonly UseOfItems _useOfItems;
    [Inject] private readonly Player _player;
    [Inject] private readonly LocationManager _locationManager;

    [SerializeField] private ItemSO _currentItem;
    [SerializeField] private GameObject _itemObj;

    private Image _backGround;
    private Image _icon;

    private TMP_Text _title;
    private TMP_Text _description;

    private Button _closeButton;
    private Button _useButton;
    private Button _dropButton;

    private InventorySlot _currentSlot;

    private void Awake()
    {
        _backGround = GetComponent<Image>();

        _title = transform.GetChild(0).GetComponent<TMP_Text>();
        _description = transform.GetChild(1).GetComponent<TMP_Text>();

        _icon = transform.GetChild(2).GetComponent<Image>();

        _closeButton = transform.GetChild(3).GetComponent<Button>();
        _useButton = transform.GetChild(4).GetComponent<Button>();
        _dropButton = transform.GetChild(5).GetComponent<Button>();

        _closeButton.onClick.AddListener(Close);
        _useButton.onClick.AddListener(Use);
        _dropButton.onClick.AddListener(Drop);
    }

    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(Close);
        _useButton.onClick.RemoveListener(Use);
        _dropButton.onClick.RemoveListener(Drop);
    }

    public void Open(ItemSO item, GameObject itemObject, InventorySlot _currentSlot)
    {
        _currentItem = item;
        _itemObj = itemObject;
        ChangeInfo(item);
        gameObject.SetActive(true);
        this._currentSlot = _currentSlot;
    }

    public void ChangeInfo(ItemSO item)
    {
        _title.text = item.Name;
        _description.text = item.Description;
        _icon.sprite = item.icon;
    }

    public void Drop()
    {
        if (_currentItem.isTool && (int)ActiveWeapon.Instance.CurrentToolType == _currentItem.toolIndex)
        {
            UIActiveWeapon.Instance.ZeroIcone();
            WeaponZero();
        }

        Vector2 dropPos = (Vector2) _player.transform.position +
            _player.LastMoveDirection;

        _itemObj.transform.SetParent(_locationManager.CurrentLocation);
        _itemObj.transform.position = dropPos;

        _itemObj.SetActive(true);
        _itemObj.transform.position = dropPos;

        _currentSlot.ClearSlot();

        Close();
    }

    public void Use() => _useOfItems.Use(_currentItem);

    public void WeaponZero() => ActiveWeapon.Instance.SetActiveWeapon(3);

    public void Close() => gameObject.SetActive(false);
}
