using UnityEngine;

public class PickUpObject : MonoBehaviour, IInteractable
{
    public ItemSO item;

    private GameObject self;

    private void Awake()
    {
        self = gameObject;
    }

    public void Init(ItemSO data) => item = data;

    public void Interact()
    {
        Debug.Log("Предмет получен");
        Inventory.Instance.PutInEmptySlot(item, self);
        gameObject.SetActive(false);
    }
}
