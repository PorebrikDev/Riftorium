using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyPool : MonoBehaviour
{
    private List<GameObject> _pool = new List<GameObject>();

    [SerializeField] private GameObject prefab;
    [SerializeField] private int _poolSize = 6;
    [SerializeField] private Transform poolRoot;

    [Inject] private readonly LocationManager _locationManager;    
    [Inject] private readonly Player _player;
    [Inject] private readonly DiContainer _container;

    private void Awake()
    {

        for (int i = 0; i < _poolSize; i++)
        {
            var obj = _container.InstantiatePrefab(prefab, poolRoot);
            obj.SetActive(false);
            obj.GetComponent<EnemyEntity>().Init(this);
            _pool.Add(obj);
        }
    }

    public GameObject GetEnemy(Transform spawnPosition)
    {
        foreach (var enemy in _pool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.transform.SetParent(_locationManager.CurrentLocation);
                enemy.transform.position = spawnPosition.position;
                enemy.SetActive(true);
                SetupEnemyAi(enemy);

                return enemy;
            }
        }

        var obj = _container.InstantiatePrefab(prefab, _locationManager.CurrentLocation);
        obj.transform.SetParent(_locationManager.CurrentLocation);
        obj.transform.position = spawnPosition.position;
        obj.GetComponent<EnemyEntity>().Init(this);
        SetupEnemyAi(obj);
        obj.SetActive(true);
        _pool.Add(obj);
        return obj;
    }

    public void ReturnToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        enemy.transform.SetParent(poolRoot);
    }

    private void SetupEnemyAi(GameObject enemyObj)
    {
        EnemyAi ai = enemyObj.GetComponent<EnemyAi>();
        if (ai != null)
        {
            ai.SetPlayer(_player);
        }
    }
}