using System.Collections;
using UnityEngine;
using Zenject;

public class FlashBlink : MonoBehaviour
{
    [Inject] private readonly Player _player;

    [SerializeField] private Material _blinkMaterial;

    private Material _defultMaterial;
    private SpriteRenderer _spriteRenderer;
    private WaitForSeconds _wait;
    private readonly float _timeBlick = 0.2f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defultMaterial = _spriteRenderer.material;
        _wait = new WaitForSeconds(_timeBlick);
    }

    private void OnEnable()
    {
        _player.OnFlashBlink += OnFlashBlink;
    }

    private void OnDisable()
    {
        _player.OnFlashBlink -= OnFlashBlink;
        StopAllCoroutines();
    }

    private void OnFlashBlink()
    {
        StartCoroutine(SetBlinkingMaterial());
    }

    private IEnumerator SetBlinkingMaterial()
    {
        _spriteRenderer.material = _blinkMaterial;
        yield return _wait;
        _spriteRenderer.material = _defultMaterial;
        yield return _wait;
        _spriteRenderer.material = _blinkMaterial;
        yield return _wait;
        _spriteRenderer.material = _defultMaterial;
    }
}
