using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;
using Zenject;

public class TreeResource : MonoBehaviour, IDamageableInt
{
    public event Action OnTreeFall;

    [SerializeField] private int _currentHealth = 3;
    [SerializeField] private int _amountHit = 1;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private ItemSO _fireWoodSO;

    [Header("Audio")]
    [SerializeField] private AudioClip _axeSound;
    [SerializeField] private AudioClip _fallSound;


    [Inject] LocationManager _locationManager;
    [Inject] AudioManager _audioManager;

    private WaitForSeconds _sleep;
    private Coroutine _coroutine;
    private Tween _tweenHit;
    private Tween _tweenDestroy;

    private BoxCollider2D _boxCollider;
    private Transform _parent;


    private bool _isTakeDamage = true;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _sleep = new WaitForSeconds(0.4f);
        _parent = transform.parent;
    }

    private void OnDestroy()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = null;
        _isTakeDamage = true;
        DOTween.KillAll();
    }

    public void TakeDamage(DamageContext ctx)
    {
        if (ctx.ToolType != ToolType.Axe || !_isTakeDamage) return;

        _currentHealth -= _amountHit;

        _audioManager.PlaySFXRandomPitch(_axeSound, 1f , 0.8f, 1.1f);

        _tweenHit = DOTween.Sequence()
            .Append(_parent.DORotate(new Vector3(0f, 0f, 3f), 0.3f))
            .Append(_parent.DORotate(Vector3.zero, 0.3f));


        DropFireWoods();

        _coroutine = StartCoroutine(enumerator());

        if (_currentHealth <= 0)
        {
            DestroyObject();
        }
    }

    private void AnimationFalling()
    {
        _tweenDestroy.Kill();
        _audioManager.PlaySFX(_fallSound);

        _tweenDestroy = DOTween.Sequence()
            .Append(_parent.DORotate(new Vector3(0f, 0f, 90f), 2f))
            .SetEase(Ease.InExpo);
    }

    private void DestroyObject()
    {

        _boxCollider.enabled = false;

        OnTreeFall?.Invoke();

        AnimationFalling();
        NavMeshSerfaceManager.Instance.RebakeNavPartOf();
    }

    private void DropFireWoods()
    {
        GameObject obj = Instantiate(_prefab, _parent.position , Quaternion.identity);

        Vector3 randomDir = UnityEngine.Random.insideUnitCircle.normalized;
        float distance = UnityEngine.Random.Range(1.5f, 3f);

        obj.GetComponent<PickUpObject>().Init(_fireWoodSO);
        obj.transform.SetParent(_locationManager.CurrentLocation);
        obj.transform.DOJump( obj.transform.position + (randomDir * distance), 1f, 1, 0.5f);
    }

    private IEnumerator enumerator()
    {
        _isTakeDamage = false;
        yield return _sleep;
        _isTakeDamage = true;

        _coroutine = null;
    }
}