using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System;
using Game.Core.Math;


public class EnemyAi : MonoBehaviour
{
    [SerializeField] private State _startingState;
    [SerializeField] private float _roamingDistanceMax = 7f;
    [SerializeField] private float _roamingDistanceMin = 3f;
    [SerializeField] private float _roamingTimerMax = 2f;

    [SerializeField] private bool _isChasingEnemy = false;
    [SerializeField] private float _chasingDistance = 4f;
    [SerializeField] private float _chasingSpeedMultiplier = 2f;

    [SerializeField] private bool _isAttackingEnemy = false;
    [SerializeField] private float _attackingDistance = 2f;
    [SerializeField] private float _attackRate = 2f;
    [SerializeField] private EnemyEntity _enemyEntity;

    private float _nextAttackTime = 0;
    private float _roamingTimer;
    private Player _player;
    private NavMeshAgent _navMeshAgent;
    private State _currentState;

    private Vector3 _roamPosition;
    private Vector3 _startingPosition;

    private float _roamingSpeed;
    private float _chasingSpeed;
    private float _defaltAcceleration;

    private float _nextCheckDirectionTime = 0f;
    private float _checkDirectionDuration = 0.1f;
    private Vector3 _lastPosition;

    public event EventHandler OnEnemyAttack;

    private enum State
    {
        Idle,
        Roaming,
        Chasing,
        Attacking,
        Death
    }

    private void Awake()
    {
        _enemyEntity = GetComponent<EnemyEntity>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _roamingSpeed = _navMeshAgent.speed;
        _chasingSpeed = _navMeshAgent.speed * _chasingSpeedMultiplier;
        _defaltAcceleration = _navMeshAgent.acceleration;
    }

    private void OnEnable()
    {
        _enemyEntity.OnEnemyDeath += CheckEvent_OnEnemyDeath;
        _currentState = _startingState;
        _roamingTimer = 0f;
        Roaming();
    }

    private void Update()
    {
        StateHandler();
        MovementDirectionHandler();
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    private void StateHandler()
    {
        switch (_currentState)
        {
            case State.Roaming:
                _roamingTimer -= Time.deltaTime;

                if (_roamingTimer < 0)
                {
                    Roaming();
                    _roamingTimer = _roamingTimerMax;
                }
                CheckCurrentState();
                break;

            case State.Chasing:
                ChasingTarget();
                CheckCurrentState();
                break;

            case State.Attacking:
                AttackingTarget();
                CheckCurrentState();
                break;

            case State.Death:
                break;

            default:
            case State.Idle:
                break;
        }
    }

    public float GetRoamingAnimationSpeed()
    {
        return _navMeshAgent.speed / _roamingSpeed;
    }

    private void ChasingTarget()
    {
        if (_player != null)
            _navMeshAgent.SetDestination(_player.transform.position);
    }

    private void CheckCurrentState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
        State newState = State.Roaming;
        if (_isChasingEnemy)
        {
            if (distanceToPlayer <= _chasingDistance)
            {
                newState = State.Chasing;
            }
        }

        if (_isAttackingEnemy)
        {
            if (distanceToPlayer <= _attackingDistance)
            {
                newState = State.Attacking;
                if (_player.IsAlive())
                { newState = State.Attacking; }
                else { newState = State.Roaming; }
            }
        }

        if (newState != _currentState)
        {
            if (newState == State.Chasing)
            {
                _navMeshAgent.ResetPath();
                _navMeshAgent.speed = _chasingSpeed;
            }
            else if (newState == State.Roaming)
            {
                _roamingTimer = 0f;
                _navMeshAgent.speed = _roamingSpeed;
            }
            else if (newState == State.Attacking)
            {
                _navMeshAgent.ResetPath();
            }

            _currentState = newState;
        }
    }

    private void AttackingTarget()
    {
        if (Time.time > _nextAttackTime)
        {
            OnEnemyAttack?.Invoke(this, EventArgs.Empty);
            _nextAttackTime = Time.time + _attackRate;
        }
    }

    public bool IsRunning
    {
        get
        {
            return _navMeshAgent.velocity.magnitude > 0.1;
        }
    }

    private void MovementDirectionHandler()
    {
        if (_currentState == State.Death) { return; }
        if (Time.time > _nextCheckDirectionTime)
        {
            if (IsRunning)
            {
                ChangeFacingDirection(_lastPosition, transform.position);
            }

            else if (_currentState == State.Attacking)
            {
                ChangeFacingDirection(transform.position, _player.transform.position);
            }
            _lastPosition = transform.position;
            _nextCheckDirectionTime = Time.time + _checkDirectionDuration;
        }
    }

    private void Roaming()
    {
        _startingPosition = transform.position;
        _roamPosition = GetRoamingPosition();
        _navMeshAgent.SetDestination(_roamPosition);
    }

    private Vector3 GetRoamingPosition()
    {
        float distance = UnityEngine.Random.Range(_roamingDistanceMin, _roamingDistanceMax);

        Vector2 dir = RandomUtils.RandomDirection2D();

        return _startingPosition + new Vector3(dir.x, dir.y, 0f) * distance;
    }

    private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition)
    {
        if (sourcePosition.x > targetPosition.x)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }
    }

    private void CheckEvent_OnEnemyDeath(object sender, System.EventArgs e)
    {
        _currentState = State.Death;
    }

    private void OnDisable()
    {
        _enemyEntity.OnEnemyDeath -= CheckEvent_OnEnemyDeath;
    }

    public void StopAgentForSeconds(float duration)
    {
        StartCoroutine(StopAgentCoroutine(duration));
    }

    private IEnumerator StopAgentCoroutine(float duration)
    {
        _navMeshAgent.ResetPath();
        _navMeshAgent.velocity = Vector3.zero;
        _navMeshAgent.speed = 0f;
        _navMeshAgent.acceleration = 0f;
        yield return new WaitForSeconds(duration);
        _navMeshAgent.speed = _roamingSpeed;
        _navMeshAgent.acceleration = _defaltAcceleration;
    }
}