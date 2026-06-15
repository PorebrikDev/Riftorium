using UnityEngine;

public class CheckPlayerPlace : MonoBehaviour
{
    [Header("Check Colider")]
    public bool IsPlayerThere { get; private set; }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter: " + collision.name);

        if (collision.TryGetComponent<Player>(out _))
            IsPlayerThere = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
            IsPlayerThere = false;
    }
}
