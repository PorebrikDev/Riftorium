using UnityEngine;

public class TriggerCave : MonoBehaviour
{
    [SerializeField] private GameObject main;
    [SerializeField] private GameObject firstChild;

    private void Awake()
    {
        firstChild = gameObject.transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision )
        {
            Debug.Log("Player обнаружен");
            firstChild.SetActive(true);
            main.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision && main != null)
        {
            firstChild.SetActive(false);
            main.SetActive(true);
        }
    }
}