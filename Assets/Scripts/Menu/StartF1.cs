using System.Collections;
using UnityEngine;

public class StartF1 : MonoBehaviour
{
    [SerializeField] private int timeDelay = 4;

    private WaitForSeconds _wait;

    private void Awake()
    {
        _wait = new WaitForSeconds(timeDelay);
    }

    private void Start()
    {
        StartCoroutine(PopUpWindow());
    }

    private IEnumerator PopUpWindow()
    {
        yield return _wait;
        gameObject.SetActive(false);
    }
}