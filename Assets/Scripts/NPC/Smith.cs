using UnityEngine;

public class Smith : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _player;
    [SerializeField] private TolkData _tolkData;
    [SerializeField] private NPCData _npcData;
    [SerializeField] private DialogueData _dialogeData;
    [SerializeField] private UiTolk _uiTolk;

    [SerializeField] private DialogueManager _dialogue;


    public bool IsTolking { get; private set; }
    public bool IsReadyTyTolk { get; private set; }

    private int rep = 0;

    private void Awake()
    {
        IsReadyTyTolk = true;
        IsTolking = false;
    }
    public void Interact()
    {
        if (IsTolking) return;

        if (IsReadyTyTolk)
        {
            SetTolking(true);

            _dialogue.OnDialogueClose += DialogueClose;
            _dialogue.StartDialogue(_dialogeData, _npcData);
            return;
        }

        SetTolking(true);

        _uiTolk.ShowText(transform.position, _tolkData.lines[rep].text, _tolkData.lines[rep].duration, () => SetTolking(false));
        nextDialog();
    }

    public void SetTolking(bool value) => IsTolking = value;

    private void nextDialog()
    {
        rep++;
        if (rep >= _tolkData.lines.Length) rep = 0;
    }

    private void DialogueClose()
    {
        SetTolking(false);
        IsReadyTyTolk = false;

        _dialogue.OnDialogueClose -= DialogueClose;
    }

}
