using DG.Tweening;
using UnityEngine;

public class TreeStump : MonoBehaviour, IDamageableInt
{
    [SerializeField] private float _moveShadow = 3.6f;

    private Tween _shadowTween;
    private GameObject _gameobj;
    private TreeResource _treeScript;

    private void Awake()
    {
        _gameobj = transform.GetChild(0).gameObject;
        _treeScript = GetComponentInChildren <TreeResource>();
    }

    private void OnEnable()
    {
        _treeScript.OnTreeFall += MoveShadow;
    }

    private void OnDisable()
    {
        _treeScript.OnTreeFall -= MoveShadow;
    }

    public void TakeDamage(DamageContext context)
    {
        // ничего 
    }

    public void MoveShadow()
    {
        _shadowTween = _gameobj.transform
            .DOMoveX(transform.position.x - _moveShadow, 1.9f)
            .SetEase(Ease.InCubic)
                .OnComplete(() =>
                { _gameobj.SetActive(false); });
    }
}