using UnityEngine;
using Zenject;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private SpawnEnumID id;

    [Inject] private LocationManager _manager;

    private SpriteRenderer _spriteRenderer;
    private Color _color = new Color(1f, 1f, 1f, 0f);

    private void Start()
    {
        _spriteRenderer.color = _color;
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _manager.Register(id, transform);
    }

    private void OnDestroy()
    {
        _manager.Unregister(id);
    }
}