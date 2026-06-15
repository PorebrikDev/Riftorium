using DG.Tweening;
using UnityEngine;

public class VisiualFadeTrees : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Tween _tween;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnDestroy()
    {
        _tween?.Kill();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( !collision.CompareTag("Player")) return;

        _tween?.Kill();
        _tween = _spriteRenderer.DOFade(0.3f, 1f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")) return;

        _tween?.Kill();
        _tween = _spriteRenderer.DOFade(1f, 1f);
    }
}