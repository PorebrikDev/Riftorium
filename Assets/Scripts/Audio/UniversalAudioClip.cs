using UnityEngine;
using Zenject;

public class UniversalAudioClip : MonoBehaviour
{
    [Header("Saund Settings")]
    [SerializeField] private AudioClip _clip;
    [SerializeField] private bool _isFastStart = false;

    [Inject] private readonly AudioManager _manager;

    [Range(0f, 1f)]
    [SerializeField] private float _volume = 1f;

    [Range(0.5f, 1.5f)]
    [SerializeField] private float _pitch = 1f;

    [Header("Saund Type")]
    [SerializeField] private bool _isMusic = false;
    [SerializeField] private bool _isUseRandomPitch = false;
    [SerializeField] private Vector2 _randomPitchRange = new Vector2(0.95f, 1.05f);

    private void Start()
    {
        if (_isFastStart) Play();
    }

    public void Play()
    {
        if (_clip == null) return;

        float finalPitch = _pitch;

        if (_isUseRandomPitch)
            finalPitch = Random.Range(_randomPitchRange.x, _randomPitchRange.y);

        if (_isMusic)
            _manager.PlayMusic(_clip, _volume);

        else
            _manager.PlaySFX(_clip, _volume, finalPitch);

    }

    public void Stop()
    { 

    if(_clip == null) return;

        if (_isMusic)
            _manager.StopMusic();

        else _manager.StopSfx();
    }
}
