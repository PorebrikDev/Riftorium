using UnityEngine;
using Zenject;

public class HitBoxNew : MonoBehaviour
{
    [Inject] private readonly Player _player;

    private Tool _tool;
    private PolygonCollider2D _collider;

    private void Awake()
    {
        _tool = GetComponentInParent<Tool>();
        _collider = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageableInt damageable))
        {
            DamageContext context = new DamageContext(
                _tool.DamageAmount,
                _tool.transform.root,
                _tool.Type);

            damageable.TakeDamage(context);
        }
    }

    public void HitBoxOn()
    {
        Vector2 dir = _player.LastMoveDirection;

        if (dir.y > 0)
            transform.localPosition = new Vector3(0f, 1.0f, 0f);      
        else if (dir.y < 0)
            transform.localPosition = new Vector3(0f, -0.2f, 0f);     
        else if (dir.x > 0)
            transform.localPosition = new Vector3(0.8f, 0.6f, 0f);      
        else if (dir.x < 0)
            transform.localPosition = new Vector3(-0.8f, 0.6f, 0f);    

        _collider.enabled = true;
    }

    public void HitBoxOff()
    {
        _collider.enabled = false;
    }
}