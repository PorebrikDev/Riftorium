using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevel : MonoBehaviour
{


    [SerializeField] private GameObject curentLayer;
    [SerializeField] private GameObject mainLayer;
    private void Awake()
    {
        curentLayer = GetComponent<GameObject>();

    }
    private void OnLeavel (Collider2D collision)
    {
      
            curentLayer.SetActive (true);
            mainLayer.SetActive (false);
       
    }
    private void OffLeavel(Collider2D collision)
    {
       
            curentLayer.SetActive(false);
            mainLayer.SetActive (true);
      
    }
    

}
