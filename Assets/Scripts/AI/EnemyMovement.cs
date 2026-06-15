using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    public void MoveTo(Vector3 position)
    {
        _agent.SetDestination(position);
    }

    public void Stop()
    {
        _agent.ResetPath();
    }
}