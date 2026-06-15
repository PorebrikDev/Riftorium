using UnityEngine;

public class EnemyLife : MonoBehaviour, IDamageableInt
{
    [SerializeField] private int _hpEnemy = 20;

    [SerializeField] private int _currentHp;

    public bool IsDead { get; private set; }

    private void Awake()
    {
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
        DetectedDeath();
    }
}
