using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private float _knockBackForse = 3f;

    private Rigidbody2D _rb;
    private EnemyAi _enemyAi;

    public bool IsGettingKnockedBack { get; private set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _enemyAi = GetComponent<EnemyAi>();
    }

    public void GetKnockedBack(Transform damageSource)
    {
        IsGettingKnockedBack = true;
        Vector2 difference = (transform.position - damageSource.position).normalized * _knockBackForse / _rb.mass;
        _rb.AddForce(difference, ForceMode2D.Impulse);

        if (_enemyAi != null)
        { _enemyAi.StopAgentForSeconds(1f); }

        StartCoroutine(StopNnumerator());
    }

    private IEnumerator StopNnumerator()
    {
        yield return new WaitForSeconds(0.5f);
        _rb.velocity = Vector3.zero;
        IsGettingKnockedBack = false;
    }
}