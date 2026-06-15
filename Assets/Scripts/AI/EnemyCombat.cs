using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private EnemyHitbox[] _hitBox;
    [SerializeField] private EnemyDateSO _currentDate;

    private CapsuleCollider2D _capsuleCollider;
    private void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    public void OpenCapsule() => _capsuleCollider.enabled = false;

    public void CloseCapsule() => _capsuleCollider.enabled = true;

    public void SetupAttack(EnemyDateSO date)
    {
        _currentDate = date;
    }
    public void EnableHitbox()
    {
        if (!_currentDate.HasHitbox)
            return;

        _hitBox[_currentDate.HitboxIndex]
            .EnableHitBox(
                _currentDate.Damage,
                _currentDate.TimeBoxEnabled);
    }
}