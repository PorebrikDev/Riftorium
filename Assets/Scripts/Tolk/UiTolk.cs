using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;

public class UiTolk : MonoBehaviour
{
    [SerializeField] private PrefabTolk _prefab;
    [SerializeField] private Transform _parent;

    private readonly int _poolSize = 5;
    private Vector3 _plus = new Vector3(0f, 3f, 0f);

    private List<PrefabTolk> _pool = new();

    private void Awake()
    {
        CreatePool();
    }

    public void ShowText(Vector3 pos, string text, float duration, Action onComplete)
    {
        var obj = _pool.Find(x => !x.gameObject.activeInHierarchy);

        if (obj == null)
        {
            CreatePool();
            obj = _pool.Find(x => !x.gameObject.activeInHierarchy);
        }

        obj.transform.position = pos + _plus;
        obj.SetText(text);
        obj.gameObject.SetActive(true);

        Observable.Timer(TimeSpan.FromSeconds(4)).Subscribe(_ =>
        {
            BackToPool(obj);
            onComplete.Invoke();
        });
    }

    public void BackToPool(PrefabTolk obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void CreatePool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            var element = Instantiate(_prefab, _parent);
            _pool.Add(element);
            element.gameObject.SetActive(false);
        }
    }
}
