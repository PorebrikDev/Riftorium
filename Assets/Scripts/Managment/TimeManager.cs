using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float time = 0;
    [SerializeField] private TMP_Text textTime;

    private void Update()
    {
        time += Time.deltaTime;
        UpdateTime();

    }
    private void UpdateTime()
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        textTime.text = $"{minutes:00}:{seconds:00}";
    }
    private void TimeClear()
    { 
    time = 0;
    }

}
