using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WorldCanvasCurrentHp : MonoBehaviour
{
    [SerializeField] private TMP_Text _damageAmountText;
    private float _timeText = 1;
    [SerializeField] private float height = 1.5f;


    private void Awake()
    {


        gameObject.SetActive(false);
    }
    public void ShowDamageUI( int damage, Transform target, Color color)
    {
        gameObject.SetActive(true);
        Vector3 vector3 = new Vector3(0, height, 0);

        gameObject.transform.position = target.position + vector3;
       
        _damageAmountText.text = "-" + damage;
        _damageAmountText.color = color;

        StartCoroutine(UiText());
    }
    private IEnumerator UiText()
    { 
    yield return new WaitForSeconds(_timeText);
        gameObject.SetActive(false);
    }

}
