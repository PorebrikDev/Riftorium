using UnityEngine;

public class ClopScript : MonoBehaviour
{
    private Animator _animator;
    private UniversalAudioClip _audioClip;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioClip = GetComponent<UniversalAudioClip>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<Player>(out _)) return;

        _audioClip.Play();
        _animator.SetBool("PlayerEnter", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<Player>(out _)) return;

        _audioClip.Stop();
        _animator.SetBool("PlayerEnter", false);
    }
}