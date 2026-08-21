using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "NPC/NPC Data")]
public class NPCData : ScriptableObject
{
    public string Name;
    public Sprite Portrait;
    public Color PortraitColor;
    public string Work;

}
