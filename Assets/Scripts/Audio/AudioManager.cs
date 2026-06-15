using UnityEngine;

[DefaultExecutionOrder(-80)]

public class AudioManager : MonoBehaviour
{

    [Header("Music")]
    [SerializeField] private AudioSource _musicSource;

    [Header("SFX")]
    [SerializeField] private AudioSource _sfxSource;


    private float _musicVolume = 0.4f;
    private float _sfxVolume = 0.6f;


    private bool _isMuted;

    private void Awake()
    {
        _musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        _sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        _musicSource.volume = _musicVolume;
        _sfxSource.volume = _sfxVolume;
    }

    public void PlayMusic(AudioClip clip, float volume = 1f)
    {
        if (_musicSource == null || clip == null) return;
        if (_musicSource.clip == clip && _musicSource.isPlaying) return;

        _musicSource.clip = clip;
        _musicSource.volume = _musicVolume * volume;
        _musicSource.loop = true;
        _musicSource.Play();
    }

    public void StopMusic() => _musicSource?.Stop();

    public void PlaySFX(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        if (_sfxSource == null || clip == null) return;

        _sfxSource.pitch = pitch;
        _sfxSource.PlayOneShot(clip, volume * _sfxVolume);
    }

    public void StopSfx()
    {
        if (_sfxSource == null) return;

        _sfxSource.Stop();
        _sfxSource.pitch = 1f;
    }

    public void PlaySFXRandomPitch(AudioClip clip, float volume, float minPitch = 0.95f, float maxPitch = 1.05f)
    {
        PlaySFX(clip, volume, Random.Range(minPitch, maxPitch));
    }

    public void SetMusicVolume(float value)
    {
        _musicVolume = value;
        _musicSource.volume = value;

        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float value)
    {
        _sfxVolume = value;

        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();
    }

    public float GetMusicVolume()
    {
        return _musicVolume;
    }

    public float GetSFXVolume()
    {
        return _sfxVolume;
    }

    public void SetMuted(bool state)
    {
        _isMuted = state;

        _musicSource.mute = state;
        _sfxSource.mute = state;
    }
}