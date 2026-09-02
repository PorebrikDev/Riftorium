using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "NPC/Dialogue")]
public class DialogueData : ScriptableObject
{
    public DialogueStage[] stages;
}

[Serializable]
public class DialogueStage
{
    public DialogueLine[] lines;
}

[Serializable]
public class DialogueLine
{
    [TextArea(2, 5)]
    public string text;
}