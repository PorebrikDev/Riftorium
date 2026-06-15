using System;
using System.Collections;
using UnityEngine;
using Zenject;

[SelectionBase]
public class Player : MonoBehaviour, IDamageableInt
{
    public event Action OnPlayerDeath;
    public event Action OnFlashBlink;
    public event Action<float> OnChangeHp;
    public event Action<float> OnChangeMp;


    public Vector2 CurrentTransPos;
    public float MaxHealth => _heroSO.maxHealth;
    public float MaxMana => _heroSO.MaxMana;
    public float CurrentHealth => _currentHealth;
    public Vector2 LastMoveDirection => _lastMoveDirection;
    public Vector2 StartTransPos => _stratTransPos;
    public bool IsAlive() => _isAlive;

    [SerializeField] private HeroSO _heroSO;
    [SerializeField] private KnockBack _knockBack;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private DamagePoolUi damagePoolUi;
    [SerializeField] private UI_Death _uiDeath;
    [SerializeField] private Player_Interact player_Interact;
    [SerializeField] private AnimatorWaponEvent player_WaponEvent;
    [SerializeField] private DelayedAbility _delayedAbility;

    [SerializeField] private float _currentHealth;

    [Space(height: 20)]
    [SerializeField] private float jerkCoolDownTime = 5f;

    [Inject] private readonly InputService _inputService;
    [Inject] private readonly Inventory _inventory;

    private Vector2 _lastMoveDirection = Vector2.down;
    private Vector2 inputVector;
    private float _heroCurrentSpeed;
    private float _currentMana;

    private readonly float _jerkManaCost = 5f;

    private Rigidbody2D _rb;
    private Vector2 _stratTransPos;

    private WaitForSeconds _waitDamage;
    private WaitForSeconds _waitJerkCooldown;
    private WaitForSeconds _waitJerk;

    private bool _canTakeDamage = true;
    private bool _isAlive = true;
    private bool _isDashing;
    private bool _isJerking;
    private bool _isJerkingControl;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _knockBack = GetComponent<KnockBack>();
        player_Interact = GetComponent<Player_Interact>();

        _waitDamage = new WaitForSeconds(_heroSO.damageRecoveryTime);
        _waitJerk = new WaitForSeconds(_heroSO.herpJerkTimer);
        _waitJerkCooldown = new WaitForSeconds(jerkCoolDownTime);
    }

    private void OnEnable()
    {
        _heroCurrentSpeed = _heroSO.heroBaseSpeed;
        _currentHealth = _heroSO.maxHealth;
        _currentMana = _heroSO.MaxMana;
        transform.position = CurrentTransPos;

        _canTakeDamage = true;
        _isAlive = true;
        _isDashing = false;
        _isJerking = false;
        _isJerkingControl = false;

        CheñkHealth();
    }

    private void Start()
    {
        _inputService.OnAttack += GameInput_OnPlayerAttack;
        _inputService.OnDash += GameInput_OnPlayerDash;
        _inputService.OnDashCanceled += GameInput_OnPlayerDashCanceled;
        _inputService.OnJerk += GameInput_OnPlayerJerkStarted;
        _inputService.OnTouchPerfomed += GameInput_OnInteractionTouchPerfomed;
        _inputService.OnInventoryStarted += GameInput_OnInteractionInventoryStarted;

        _uiDeath.OnRespStart += Respawn;

        _stratTransPos = transform.position;
    }

    private void Update()
    {
        inputVector = _inputService.GetMovementVector();

        if (inputVector.sqrMagnitude > 0.01f)
        {
            inputVector = inputVector.normalized;
            _lastMoveDirection = inputVector;
        }
    }

    private void FixedUpdate()
    {
        if (_knockBack.IsGettingKnockedBack || _isJerkingControl)
            return;

        HandleMovement();
    }

    private void OnDestroy()
    {
        _inputService.OnAttack -= GameInput_OnPlayerAttack;
        _inputService.OnDash -= GameInput_OnPlayerDash;
        _inputService.OnDashCanceled -= GameInput_OnPlayerDashCanceled;
        _inputService.OnJerk -= GameInput_OnPlayerJerkStarted;
        _inputService.OnTouchPerfomed -= GameInput_OnInteractionTouchPerfomed;
        _inputService.OnInventoryStarted -= GameInput_OnInteractionInventoryStarted;

        _uiDeath.OnRespStart -= Respawn;
    }

    public void TakeDamage(DamageContext ctx)
    {
        if (!_canTakeDamage || !_isAlive) return;

        _canTakeDamage = false;
        _currentHealth = Mathf.Max(0f, _currentHealth - ctx.Damage);

        Debug.Log($"ïîëó÷åíî óðîíà = {ctx.Damage}; òåêóùåå çäîðîâüå = {_currentHealth}.");

        _knockBack.GetKnockedBack(ctx.Source);
        OnFlashBlink?.Invoke();
        damagePoolUi.ShowDamage(ctx.Damage, transform, Color.red);

        StartCoroutine(DamageRecoveryRoutine());
        CheñkHealth();
        DetectedDeath();
    }

    public void Respawn()
    {
        transform.position = CurrentTransPos;
        _inputService.StateDeath(false);
    }

    public void HpRecovery(float hp)
    {
        _currentHealth += hp;
        CheñkHealth();
        DetectedDeath();
    }

    public void ChangeMana(float mana)
    {
        _currentMana += mana;
        CheckMana();
    }

    private void GameInput_OnPlayerDash()
    {
        if (_isDashing) return;
        _isDashing = true;
        _heroCurrentSpeed *= _heroSO.heroDashMultiplier;
    }

    private void GameInput_OnPlayerDashCanceled()
    {
        _isDashing = false;
        _heroCurrentSpeed = _heroSO.heroBaseSpeed;
    }

    private void GameInput_OnPlayerAttack()
    {
        Tool active = ActiveWeapon.Instance.GetActiveWeapon();
        if (active != null & player_WaponEvent.CanAttack)
        {
            ActiveWeapon.Instance.GetActiveWeapon().Attack();
            ActiveWeapon.Instance.VisualAttack();
        }
    }

    private void CheñkHealth()
    {
        if (_currentHealth > _heroSO.maxHealth) { _currentHealth = _heroSO.maxHealth; }
        OnChangeHp?.Invoke(_currentHealth);
    }

    private void CheckMana()
    {
        if (_currentMana > _heroSO.MaxMana) { _currentMana = _heroSO.MaxMana; }
        if (_currentMana <= 0) { _currentMana = 0; }

        OnChangeMp?.Invoke(_currentMana);
    }

    private void DetectedDeath()
    {
        if (_currentHealth > 0 || !_isAlive) return;

        _isAlive = false;
        inputVector = Vector2.zero;
        _rb.velocity = Vector2.zero;

        _inputService.StateDeath(true);

        OnPlayerDeath?.Invoke();
    }

    private void GameInput_OnPlayerJerkStarted()
    {
        if (_currentMana < _jerkManaCost || _isJerking) return;

        StartCoroutine(CoruytineJerkStart());
    }

    private IEnumerator CoruytineJerkStart()
    {
        _isJerking = true;
        _isJerkingControl = true;

        ChangeMana(-_jerkManaCost);

        float totalCooldown = _heroSO.herpJerkTimer + jerkCoolDownTime;
        _delayedAbility.StartFilled(totalCooldown);

        Vector2 dir = _lastMoveDirection.normalized;

        _rb.velocity = Vector2.zero;
        _rb.AddForce(dir * _heroSO.herpJerkStength, ForceMode2D.Impulse);

        trailRenderer.emitting = true;

        yield return _waitJerk;
        _isJerkingControl = false;
        trailRenderer.emitting = false;
        _heroCurrentSpeed = _heroSO.heroBaseSpeed;
        _delayedAbility.StartFilled(jerkCoolDownTime);

        yield return _waitJerkCooldown;
        _isJerking = false;
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return _waitDamage;
        _canTakeDamage = true;
    }

    private void HandleMovement()
    {
        _rb.MovePosition(_rb.position + inputVector * (_heroCurrentSpeed * Time.fixedDeltaTime));
    }

    private void GameInput_OnInteractionInventoryStarted() => _inventory.InventoryOpenClose();
    private void GameInput_OnInteractionTouchPerfomed() => player_Interact.TryInteract();
}