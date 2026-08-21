using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DialogueManager : MonoBehaviour
{
    public event Action OnDialogueClose;

    [Inject] private readonly InputService _input;

    [SerializeField] private Image _dialogueWindow;
    [SerializeField] private Image _portrait;
    [SerializeField] private Image _iconColor;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _cancelButton;

    private DialogueData _currentDialogue;
    private int _index;

    private void Awake()
    {
        _cancelButton.onClick.AddListener(NextLine);
        _dialogueWindow.gameObject.SetActive(false);
    }

    public void StartDialogue(DialogueData Tolkdata, NPCData npcData)
    {
        Debug.Log($"NPC: {npcData.Name}");
        Debug.Log($"Color: {npcData.PortraitColor}");
        _index = 0;
        _currentDialogue = Tolkdata;
        _name.text = npcData.Name;
        _iconColor.color = npcData.PortraitColor;
        _portrait.sprite = npcData.Portrait;

        _dialogueWindow.gameObject.SetActive(true);
        _input.SetControl(false);

        ShowLine();
    }


    private void ShowLine()
    {
        _text.text = _currentDialogue.lines[_index].text;
    }

    private void NextLine()
    {
        _index++;

        if (_index >= _currentDialogue.lines.Length)
        {
            CloseDialogue();
            return;
        }

        ShowLine();
    }

    private void CloseDialogue()
    {
        _dialogueWindow.gameObject.SetActive(false);
        _input.SetControl(true);

        OnDialogueClose?.Invoke();

    }


}
