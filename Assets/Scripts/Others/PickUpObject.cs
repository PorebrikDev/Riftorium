using UnityEngine;

public class PickUpObject : MonoBehaviour, IInteractable
{
    public ItemSO Item;
    public int Amount = 1;



    private GameObject self;

    private void Awake()
    {
        self = gameObject;
    }

    public void Init(ItemSO data, int amount)
    {
        Item = data;
        Amount = amount;
    }
 

    public void Interact()
    {
        Debug.Log("Предмет получен");
        Inventory.Instance.AddItem(Item, self, Amount );
        gameObject.SetActive(false);
    }


}
