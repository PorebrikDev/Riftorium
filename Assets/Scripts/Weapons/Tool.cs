using System;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public event Action OnToolUse;

    public int DamageAmount => _damageAmount;
    public ToolType Type => _toolType;

    [SerializeField] private ToolType _toolType;
    [SerializeField] private int _damageAmount = 1;

    private HitBoxNew _hitBox;

    private void Awake()
    {
        _hitBox = GetComponentInChildren<HitBoxNew>();
    }

    public void BoxOn() => _hitBox.HitBoxOn();

    public void BoxOff() =>  _hitBox.HitBoxOff();

    public void Attack() => OnToolUse?.Invoke();
}
