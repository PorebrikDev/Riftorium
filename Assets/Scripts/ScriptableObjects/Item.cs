using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public string Name = "";
    public string Description = "Описание предмета";
    public Sprite icon = null;
    public AudioClip Clip = null;

    public bool isHealing;
    public int HealingPower;

    public bool IsManaRestore = false;
    public float ManaRestorePower;

    public bool isTool;
    public int toolIndex;

}
