using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePoolUi : MonoBehaviour
{
    public static DamagePoolUi instance;
    [SerializeField] private WorldCanvasCurrentHp prefabDamage;

    private int _poolSize = 5;
    
    private List<WorldCanvasCurrentHp> pool = new List<WorldCanvasCurrentHp>();

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < _poolSize; i++)
        { 
        var obj = Instantiate(prefabDamage, transform);
            obj.gameObject.SetActive(false);
            pool.Add(obj);
        }
    }

    public void ShowDamage(int damage, Transform target, Color color)
    {
        var obj = pool.Find(x => !x.gameObject.activeInHierarchy);
        if (obj != null) 
        {
            obj.gameObject.SetActive(true);
            obj.ShowDamageUI(damage, target, color);
        }
    }


}
