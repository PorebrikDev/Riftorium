using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    [SerializeField] private Collider2D _collider2D;

    private int _damage;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _collider2D.enabled = false;
    }

    public void EnableHitBox(int dmg, float time)
    {
        _damage = dmg;
        _collider2D.enabled = true;

        Invoke(nameof(DisableHitBox), time);
    }

    public void DisableHitBox()
    {
        _collider2D.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("увидел");
        if (other.TryGetComponent(out Player player))
        {
            DamageContext ctx = new DamageContext(
                _damage, this.transform);

            player.TakeDamage(ctx);
            Debug.Log("ударил");

        }
    }
}

