using UnityEngine;
using Zenject;

public class SwordVisual : MonoBehaviour
{
    private const string Attack = "Attack";

    [SerializeField] private Tool _tool;
    [SerializeField] private HitBoxNew _hitBox;

    [Inject] InputService _input;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _tool.OnToolUse += Tool_OnToolUse;
    }

    private void OnDisable()
    {
        _tool.OnToolUse -= Tool_OnToolUse;
    }

    public void Update()
    {
        Vector2 movement = _input.GetMovementVector();
        UpdateAnimator(movement);
    }

    private void UpdateAnimator(Vector2 movement)
    {
        _animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.sqrMagnitude <= 0.01f)
            return;

        _animator.SetFloat("MoveX", movement.x);
        _animator.SetFloat("MoveY", movement.y);
    }

    private void Tool_OnToolUse() => _animator.SetTrigger(Attack);
}





