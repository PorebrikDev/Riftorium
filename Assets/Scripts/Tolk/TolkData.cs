using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Hero", menuName = "NPC/Tolk")]
public class TolkData : ScriptableObject
{
    public TolkLine[] lines;
}

[Serializable]
public class TolkLine
{
    [TextArea(2, 5)]
    public string text;

    [Min(0.1f)]
    public float duration = 4f;
}