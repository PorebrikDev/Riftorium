using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(KnockBack))]
[RequireComponent(typeof(PolygonCollider2D))]

public class EnemyEntity : MonoBehaviour, IDamageableInt
{

    public event EventHandler OnTakeHit;
    public event EventHandler OnEnemyDeath;

    [SerializeField] private EnemySO _enemySO;
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private AudioClip _enemyClip;

    [Inject] private AudioManager _audioManager;

    private PolygonCollider2D _collider;
    private KnockBack _knockBack;

    private readonly float _attackCooldown = 1.0f;
    private float _currentHealth;
    private float _lastDamageTime;

    private bool _canTakeDamage;
    private bool _isAlive;

    private WaitForSeconds _wait;


    private void Awake()
    {
        _collider = GetComponent<PolygonCollider2D>();
        _knockBack = GetComponent<KnockBack>();
        _wait = new WaitForSeconds(_enemySO.damageRecoveryTime);
    }

    private void OnEnable()
    {
        _canTakeDamage = true;
        _isAlive = true;
        _currentHealth = _enemySO.enemyHealth;
    }

    public void TakeDamage(DamageContext ctx)
    {
        if (!_canTakeDamage || !_isAlive) return;

        _canTakeDamage = false;

        _currentHealth -= ctx.Damage;

        _audioManager.PlaySFXRandomPitch(_enemyClip, 1f, 0.6f, 1.3f);

        _knockBack.GetKnockedBack(ctx.Source);

        OnTakeHit?.Invoke(this, EventArgs.Empty);

        Debug.Log("текущее хп = " + _currentHealth + "| Нанесенный урон = " + ctx.Damage);

        DamagePoolUi.instance.ShowDamage(ctx.Damage, transform, Color.blue);

        DetectDeath();

        StartCoroutine(DamageRecoveryTimeSlime());
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if(Time.time - _lastDamageTime < _attackCooldown) return;

            _lastDamageTime = Time.time;

            DamageContext ctx = new DamageContext(
                _enemySO.enemyDamageAmount,
                transform);

            player.TakeDamage(ctx);
        }
    }

    public void Init(EnemyPool pool) => _enemyPool = pool;

    public void ReturnToPoolAnimation() => _enemyPool.ReturnToPool(gameObject);

    public void PoligonColliderTurnOn() => _collider.enabled = true;

    public void PoligonColliderTurnOff() => _collider.enabled = false;

    private void DetectDeath()
    {
        if (_currentHealth <= 0 && _isAlive)
        {
            _isAlive = false;
            OnEnemyDeath?.Invoke(this, EventArgs.Empty);
        }
    }

    private IEnumerator DamageRecoveryTimeSlime()
    {
        yield return _wait;
        _canTakeDamage = true;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
