using UnityEngine;
using Zenject;


public class ActiveWeapon : MonoBehaviour
{
    public static ActiveWeapon Instance { get; private set; }

    public ToolType CurrentToolType => _currentToolType;

    [SerializeField] private Animator _weaponAnimator;
    [SerializeField] private PlayerVisual _playerVisual;

    [Header("Tools")]
    [SerializeField] private Tool _swordTool;
    [SerializeField] private Tool _pickaxeTool;
    [SerializeField] private Tool _axeTool;
    [SerializeField] private Tool _zeroTool;

    [Inject] private InputService _input;

    [SerializeField] private ToolType _currentToolType;
    [SerializeField] private Tool _currentTool = null;

    public void Awake()
    {
        Instance = this;
        _currentToolType = ToolType.Zero;
    }

    private void Update()
    {
        Vector2 move = _input.GetMovementVector();
        UpdateAnimator(_weaponAnimator, move);
        _playerVisual.UpdateWeaponAnimator(move);
    }

    public Tool GetActiveWeapon() => _currentTool;

    public void SetActiveWeapon(int index)
    {
        switch (index)
        {
            case 0:
                _currentTool = _swordTool;
                _currentToolType = ToolType.Sword;
                break;
            case 1:
                _currentTool = _pickaxeTool;
                _currentToolType = ToolType.Pickaxe;
                break;
            case 2:
                _currentTool = _axeTool;
                _currentToolType = ToolType.Axe;
                break;
            case 3:
                _currentTool = _zeroTool;
                _currentToolType = ToolType.Zero;
                break;
        }
    }

    private void UpdateAnimator(Animator anim, Vector2 move)
    {
        anim.SetFloat("Speed", move.sqrMagnitude);
        if (move.sqrMagnitude > 0.01f)
        {
            anim.SetFloat("MoveX", move.x);
            anim.SetFloat("MoveY", move.y);
        }
    }

    public void VisualAttack()
    {
        _weaponAnimator.SetInteger("ToolType", (int)_currentTool.Type);
        _weaponAnimator.SetTrigger("Attack");

        Animator bodyanimator = _playerVisual.GetAnimator();
        bodyanimator.SetInteger("ToolType", (int)_currentTool.Type);
        bodyanimator.SetTrigger("Attack");
    }
}
