using System.Collections.Generic;
using UnityEngine;

public class PoolBrash : MonoBehaviour

{    public List<GameObject> pool = new List<GameObject>();

    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform BushPool;
    [SerializeField] private Transform BushLokalPlase;


    private int _poolSize = 2;

    private void Awake()
    {

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity, BushPool);
            obj.GetComponent<Bush>().Init(this);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetBrash(Transform target)
    {
        foreach (GameObject brash in pool)
        {
            if (!brash.activeInHierarchy)
            {
                brash.transform.SetParent(BushLokalPlase);
                brash.transform.position = target.transform.position;
                brash.SetActive(true);
                return brash;
            }
        }

        GameObject more = Instantiate(prefab, transform.position, Quaternion.identity, BushLokalPlase);
        return more;
    }
    public void ReturnToPool(GameObject enemy)
    {
        enemy.transform.SetParent(BushPool);
        enemy.SetActive(false);
    }
}