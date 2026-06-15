using UnityEngine;
using Zenject;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject targetLocation;
    [SerializeField] private SpawnEnumID targetSpawn;

    [Inject] private LocationManager _manager;

    private SpriteRenderer _spriteRenderer;
    private Color _color = new Color(1f, 1f, 1f, 0f);

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _spriteRenderer.color = _color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        _manager.SwitchLocation(targetLocation, targetSpawn);
    }
}