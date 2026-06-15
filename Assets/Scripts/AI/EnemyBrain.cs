using System.Collections;
using UnityEngine;
using Zenject;

public class EnemyBrain : MonoBehaviour
{
    public float NextAttackTime { get; set; }

    [Header("AI States")]
    [SerializeField] private EnemyDateSO[] _attackSO;

    [Header("Run Places")]
    [SerializeField] private Transform[] _runPoints;
    [SerializeField] private float _runDuration = 3f;

    [Header("Check Zone")]
    [SerializeField] private CheckPlayerPlace _downZone;
    [SerializeField] private CheckPlayerPlace _upZone;
    [SerializeField] private CheckPlayerPlace _leftZone;
    [SerializeField] private CheckPlayerPlace _rightZone;

    public bool IsPlayerDown => _downZone.IsPlayerThere;
    public bool IsPlayerUp => _upZone.IsPlayerThere;
    public bool IsPlayerLeft => _leftZone.IsPlayerThere;
    public bool IsPlayerRight => _rightZone.IsPlayerThere;
    public bool IsPlayerNear =>
    _downZone.IsPlayerThere ||
    _upZone.IsPlayerThere ||
    _leftZone.IsPlayerThere ||
    _rightZone.IsPlayerThere;

    [Header("Components")]

    [Inject] private Player _player;

    public Animator Animator { get; private set; }
    public EnemyCombat Combat { get; private set; }
    public EnemyLife EnemyLife { get; private set; }

    public bool IsBusy { get; private set; } = false;

    public Vector3 VectorToPlayer => _player.transform.position - transform.position;

    private EnemyMovement _movement;
    private Coroutine _coroutine;
    private WaitForSeconds _wait;
    private WaitForSeconds _waitThink;
    private Vector3 _spawnPos;
    private int _lastPoint = -1;
    private bool _isRunningAway =false;


    private void Awake()
    {
        Animator = GetComponent<Animator>();
        Combat = GetComponent<EnemyCombat>();
        _movement = GetComponent<EnemyMovement>();
        EnemyLife = GetComponent<EnemyLife>();
        _wait = new WaitForSeconds(_runDuration);
        _waitThink = new WaitForSeconds(0.2f);
        _spawnPos = transform.position;
    }

    private void OnEnable()
    {
        transform.position = _spawnPos;
        _coroutine = StartCoroutine(ThinkRoutine());
    }

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private void OnDestroy()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    public void ChangeSO()
    {
        if (!EnemyLife.IsDead)
        {
            Debug.Log(EnemyLife.IsDead + "1" +IsBusy + "bysi");
            if (IsBusy)
                return;
        }

        foreach (EnemyDateSO state in _attackSO)
        {
            if (state.CanEnter(this))
            {
                state.Execute(this);
                break;
            }
        }
    }

    public void RunAway()
    {
        if (_isRunningAway) return;

        StartCoroutine(RunAwayRoutine());
    }

    public void AnimStart() => IsBusy = true;
    public void AnimEnd() => IsBusy = false;

    public void DeadRealization()
    {
        Destroy(gameObject);
    }

    private IEnumerator ThinkRoutine()
    {
        WaitForSeconds wait = _waitThink;

        while (true)
        {
            ChangeSO();
            yield return wait;
        }
    }

    private IEnumerator RunAwayRoutine()
    {
        _isRunningAway = true;

        int _randomIndex;
        do
        { _randomIndex = Random.Range(0, _runPoints.Length); }

        while
        (_randomIndex > 1 && _randomIndex == _lastPoint);

        _lastPoint = _randomIndex;

        Transform point = _runPoints[_lastPoint];

        _movement.MoveTo(point.position);

        yield return _wait;

        _movement.Stop();
        _isRunningAway = false;
    }

}
