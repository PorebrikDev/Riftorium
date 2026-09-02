using System;
using UnityEngine;

public class EnemyLife : MonoBehaviour, IDamageableInt
{
    public event Action<int, int> OnNPCTakeDamage;

    [SerializeField] private int _hpEnemy = 20;

    [SerializeField] private int _currentHp;

    private Droped _droped;


    public bool IsDead { get; private set; }

    private void Awake()
    {
        _droped = GetComponent<Droped>();
        _currentHp = _hpEnemy;
        IsDead = false;
    }

    private void DetectedDeath()
    {
        if (_currentHp <= 0)
            IsDead = true;
    }

    public void TakeDamage(DamageContext ctx)
    {
        _currentHp -= ctx.Damage;

        if (_currentHp < 0)
            _currentHp = 0;

        OnNPCTakeDamage?.Invoke(_currentHp, _hpEnemy);

        DetectedDeath();
    }

    private void OnDestroy()
    {
        _droped.DropThis();
    }
}
