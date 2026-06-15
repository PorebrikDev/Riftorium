using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Zenject;


[DefaultExecutionOrder(-100)]
public class InputService : MonoBehaviour
{
    private PlayerInputActions _input;

    public event Action OnAttack;
    public event Action OnDash;
    public event Action OnDashCanceled;
    public event Action OnJerk;
    public event Action OnTouchPerfomed;
    public event Action OnInventoryStarted;
    public event Action OnMenuStarted;
    public event Action OnF1Started;
    public event Action<int> OnNumberKeyPressed;

    [Inject] private readonly Inventory _inventory;
    [Inject] private readonly Player _player;

    private bool _isPaused = false;

    private Action<InputAction.CallbackContext> _number1Handler;
    private Action<InputAction.CallbackContext> _number2Handler;
    private Action<InputAction.CallbackContext> _number3Handler;
    private Action<InputAction.CallbackContext> _number4Handler;
    private Action<InputAction.CallbackContext> _number5Handler;
    private Action<InputAction.CallbackContext> _number6Handler;
    private Action<InputAction.CallbackContext> _number7Handler;
    private Action<InputAction.CallbackContext> _number8Handler;
    private Action<InputAction.CallbackContext> _number9Handler;
    private Action<InputAction.CallbackContext> _number0Handler;

    private PointerEventData _eventData;
    private List<RaycastResult> _results = new List<RaycastResult>();

    private bool _isDead = false;

    private void Awake()
    {
        _input = new PlayerInputActions();
        _eventData = new PointerEventData(EventSystem.current);

        _number1Handler = _ => OnNumberKeyPressed?.Invoke(0);
        _number2Handler = _ => OnNumberKeyPressed?.Invoke(1);
        _number3Handler = _ => OnNumberKeyPressed?.Invoke(2);
        _number4Handler = _ => OnNumberKeyPressed?.Invoke(3);
        _number5Handler = _ => OnNumberKeyPressed?.Invoke(4);
        _number6Handler = _ => OnNumberKeyPressed?.Invoke(5);
        _number7Handler = _ => OnNumberKeyPressed?.Invoke(6);
        _number8Handler = _ => OnNumberKeyPressed?.Invoke(7);
        _number9Handler = _ => OnNumberKeyPressed?.Invoke(8);
        _number0Handler = _ => OnNumberKeyPressed?.Invoke(9);
    }

    private void OnEnable()
    {
        _input.Enable();

        _input.Combat.Attack.started += OnAttackStarted;

        _input.Player.Dash.performed += OnDashPerformed;
        _input.Player.Dash.canceled += OnDashCanceledHandler;
        _input.Player.Jerk.started += PlayerJerkStarted;

        _input.Interaction.Touch.performed += OnTouchPerformedHandler;
        _input.Interaction.Inventory.started += OnInventoryStartedHandler;
        _input.Interaction.Menu.started += OnMenuStartedHandler;
        _input.Interaction.ControlF1.started += OnControlF1Handler;

        _input.Interaction._1.started += _number1Handler;
        _input.Interaction._2.started += _number2Handler;
        _input.Interaction._3.started += _number3Handler;
        _input.Interaction._4.started += _number4Handler;
        _input.Interaction._5.started += _number5Handler;
        _input.Interaction._6.started += _number6Handler;
        _input.Interaction._7.started += _number7Handler;
        _input.Interaction._8.started += _number8Handler;
        _input.Interaction._9.started += _number9Handler;
        _input.Interaction._0.started += _number0Handler;
    }

    private void OnDisable()
    {
        _input.Combat.Attack.started -= OnAttackStarted;
        _input.Player.Dash.performed -= OnDashPerformed;
        _input.Player.Dash.canceled -= OnDashCanceledHandler;
        _input.Player.Jerk.started -= PlayerJerkStarted;
        _input.Interaction.Touch.performed -= OnTouchPerformedHandler;
        _input.Interaction.Inventory.started -= OnInventoryStartedHandler;
        _input.Interaction.Menu.started -= OnMenuStartedHandler;
        _input.Interaction.ControlF1.started -= OnControlF1Handler;

        _input.Interaction._1.started -= _number1Handler;
        _input.Interaction._2.started -= _number2Handler;
        _input.Interaction._3.started -= _number3Handler;
        _input.Interaction._4.started -= _number4Handler;
        _input.Interaction._5.started -= _number5Handler;
        _input.Interaction._6.started -= _number6Handler;
        _input.Interaction._7.started -= _number7Handler;
        _input.Interaction._8.started -= _number8Handler;
        _input.Interaction._9.started -= _number9Handler;
        _input.Interaction._0.started -= _number0Handler;

        _input.Disable();
    }

    public bool IsPaused
    {
        get => _isPaused;
        set
        {
            _isPaused = value;
            Time.timeScale = _isPaused ? 0f : 1f;

            if (_isPaused)
            {
                _input.Player.Disable();
                _input.Combat.Disable();
            }
            else
            if(!_isDead)
            {
                _input.Player.Enable();
                _input.Combat.Enable();
            }
        }
    }

    public Vector2 GetMovementVector() => _input.Player.Move.ReadValue<Vector2>();

    public void DisableMovement() => _input.Disable();
    public void EnableCombat() => _input.Combat.Enable();
    public void DisableCombat() => _input.Combat.Disable();

    public void EnablePlayer() => _input.Player.Enable();
    public void DisablePlayer() => _input.Player.Disable();

    public void StateDeath(bool current)
    {
        _isDead = current;
        if (current)
        {
            _input.Player.Disable();
            _input.Interaction.Menu.Disable();
            _input.Combat.Disable();
        }
        else {
            _input.Player.Enable();
            _input.Interaction.Menu.Enable();
            _input.Combat.Enable();
        }
    }

    private bool IsPointerOverUI()
    {
        _eventData.position = Mouse.current.position.ReadValue();

        _results.Clear();

        EventSystem.current.RaycastAll(_eventData, _results);

        for (int i = 0; i < _results.Count; i++)
        {
            if (_results[i].gameObject.layer == LayerMask.NameToLayer("UI_BlockInput"))
                return true;
        }

        return false;
    }

    private void OnMenuStartedHandler(InputAction.CallbackContext obj)
    {
        if (!_inventory.IsInventory)
        {
            IsPaused = !IsPaused;
        }

        else
        { _inventory.InventoryOpenClose(); }

        OnMenuStarted?.Invoke();
    }

    private void OnAttackStarted(InputAction.CallbackContext obj)
    {
        if (IsPointerOverUI()) return;

        OnAttack?.Invoke();
    }

    private void OnControlF1Handler(InputAction.CallbackContext obj) => OnF1Started?.Invoke();

    private void OnInventoryStartedHandler(InputAction.CallbackContext obj) => OnInventoryStarted?.Invoke();

    private void OnTouchPerformedHandler(InputAction.CallbackContext obj) => OnTouchPerfomed?.Invoke();

    private void OnDashPerformed(InputAction.CallbackContext obj) => OnDash?.Invoke();

    private void OnDashCanceledHandler(InputAction.CallbackContext obj) => OnDashCanceled?.Invoke();

    private void PlayerJerkStarted(InputAction.CallbackContext obj) => OnJerk?.Invoke();
}