using System.Collections;
using UnityEngine;
using Zenject;

public class SmithVisiual : MonoBehaviour
{
    [Inject] private readonly AudioManager _audioManager;
    [SerializeField] private AudioClip _soundSneeze;

    [SerializeField] private Transform _player;
    private const float SneezeDistance = 15f;

    private Smith _smith;
    private Animator _animator;
    private readonly WaitForSeconds _sneezWait = new(10);
    private Coroutine _sneezeCoroutine;


    private void Awake()
    {
        _smith = GetComponent<Smith>();
        _animator = GetComponent<Animator>();
        LifeStile();
    }

    private void LifeStile()
    {
        _sneezeCoroutine = StartCoroutine(TimeSneeze());
    }

    public void StopSneeze()
    {
        if (_sneezeCoroutine != null)
        {
            StopCoroutine(TimeSneeze());
            _sneezeCoroutine = null;
        }
    }

    private IEnumerator TimeSneeze()
    {
        while (true)
        {
            yield return _sneezWait;

            if (!IsPlayerNearby() || _smith.IsTolking)
                continue;

            _animator.SetTrigger("sneeze");
            _audioManager.PlaySFXRandomPitch(_soundSneeze, 0.5f);
        }
    }

    private bool IsPlayerNearby()
    {
        Vector2 offset = _player.transform.position - transform.position;
        return offset.sqrMagnitude < SneezeDistance;
    }
}