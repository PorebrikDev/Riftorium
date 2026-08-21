using UnityEngine;
using Zenject;

public class Player_Interact : MonoBehaviour
{
    [SerializeField] private float interactRadius = 1f;
    [SerializeField] private LayerMask interactLayer;

    [Inject] private readonly Player _player;
    private float interactDistance = 1f;


    public void TryInteract()
    {
        Vector2 interactPosition = (Vector2)transform.position + _player.LastMoveDirection.normalized * interactDistance;
        Collider2D collider = Physics2D.OverlapCircle(interactPosition, interactRadius, interactLayer);

        if (collider == null)
        {
            Debug.Log("Радом нет объекта");
            return;
        }
        if (collider.TryGetComponent<IInteractable>(out var interactable))
        { 
        interactable.Interact();
        }
            
    }

    private void OnDrawGizmosSelected()
    {
        if (_player == null) return;

        Vector2 interactPosition =
            (Vector2)transform.position +
            _player.LastMoveDirection.normalized * interactDistance;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactPosition, interactRadius);
    }

}
