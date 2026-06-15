using System;
using UnityEngine;
using Zenject;

public class Bush : MonoBehaviour, IDamageableInt
{
    public event EventHandler OnTakeDamage;

    [SerializeField] private PoolBrash _pooBrash;
    [SerializeField] private int currentHealth = 3;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int damage = 1;

    [SerializeField] private AudioClip _clip;

    [Inject] private readonly AudioManager _audioManager;

    private Transform _targer;
    private EnemyPool _enemyPool;

    private void Awake()
    {
        _targer = transform;
        _enemyPool = FindAnyObjectByType<EnemyPool>();
    }

    public void Init(PoolBrash pool) { _pooBrash = pool; }
    public void TakeDamage(DamageContext ctx)
    {
        if (ctx.ToolType == ToolType.Sword || ctx.ToolType == ToolType.Axe)
        {
            if (currentHealth == maxHealth)
            {
                GameObject enemy = _enemyPool.GetEnemy(transform);
            }

            currentHealth -= damage;

            _audioManager.PlaySFXRandomPitch(_clip, 1f, 0.9f, 1.2f);

            OnTakeDamage?.Invoke(this, EventArgs.Empty);

            if (currentHealth <= 0)
            {
                DestroyObject();
            }
        }
    }

    private void DestroyObject()
    {
        _pooBrash.ReturnToPool(gameObject);
        NavMeshSerfaceManager.Instance.RebakeNavPartOf();
    }
}