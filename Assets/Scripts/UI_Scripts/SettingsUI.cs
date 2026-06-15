using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Button _muzSwitch;
    [SerializeField] private Button _changeScreen;

    [SerializeField] private TMP_Dropdown _dropdownResolution;
    [SerializeField] private TMP_Dropdown _dropdownFPS;

    [SerializeField] private Sprite _spriteT;
    [SerializeField] private Sprite _spriteF;

    [Inject] private AudioManager _audioManager;
    [Inject] private FPSCenter _fcenter;

    private bool _isMusicMuted;
    private bool _isFullscreen;

    private void Start()
    {
        musicSlider.value = _audioManager.GetMusicVolume();
        sfxSlider.value = _audioManager.GetSFXVolume();

        _muzSwitch.image.sprite = _spriteT;

        musicSlider.onValueChanged.AddListener(SetMusic);
        sfxSlider.onValueChanged.AddListener(SetSFX);
        _muzSwitch.onClick.AddListener(SwitchMusic);
        _changeScreen.onClick.AddListener(ChangeScreen);

        _isFullscreen = Screen.fullScreen;
        _changeScreen.image.sprite = _isFullscreen ? _spriteT : _spriteF;

        SetupDropdown();
        SetupFPS();
        SyncFPSDropdown();

        gameObject.SetActive(false);

    }

    private void OnDestroy()
    {
        musicSlider.onValueChanged.RemoveListener(SetMusic);
        sfxSlider.onValueChanged.RemoveListener(SetSFX);

        _muzSwitch.onClick.RemoveListener(SwitchMusic);
        _changeScreen.onClick.RemoveListener(ChangeScreen);

        _dropdownResolution.onValueChanged.RemoveListener(SetResolution);
        _dropdownFPS.onValueChanged.RemoveListener(OnFPSChanged);
    }

    private void SetMusic(float value)
    {
        _audioManager.SetMusicVolume(value);
    }

    private void SetSFX(float value)
    {
        _audioManager.SetSFXVolume(value);
    }

    private void SwitchMusic()
    {
        Debug.Log("клик");
        _isMusicMuted = (!_isMusicMuted);
        _muzSwitch.image.sprite = (_isMusicMuted ? _spriteF : _spriteT);
        _audioManager.SetMuted(_isMusicMuted);
    }

    private void ChangeScreen()
    {
        _isFullscreen = !_isFullscreen;
        Screen.fullScreen = _isFullscreen;
        _changeScreen.image.sprite = (_isFullscreen ? _spriteT : _spriteF);
    }


    private void SetupFPS()
    {
        _dropdownFPS.ClearOptions();
        _dropdownFPS.AddOptions(new List<string> { "30", "60", "120", "Unlimited" });
        _dropdownFPS.onValueChanged.AddListener(OnFPSChanged);
    }

    private void OnFPSChanged(int index)
    {
        int fps = index switch
        {
            0 => 30,
            1 => 60,
            2 => 120,
            3 => -1,
            _ => 60
        };
        _fcenter.SetFPS(fps);
    }

    private void SyncFPSDropdown()
    {
        int fps = PlayerPrefs.GetInt("FPS", 60);

        _dropdownFPS.value = fps switch
        {
            30 => 0,
            60 => 1,
            120 => 2,
            -1 => 3,
            _ => 1
        };
    }

    private void SetupDropdown()
    {
        _dropdownResolution.ClearOptions();
        _dropdownResolution.AddOptions(new List<string> { "1920 x 1080", "1600 x 900", "1280 x 720" });
        _dropdownResolution.onValueChanged.AddListener(SetResolution);
    }

    private void SetResolution(int index)
    {
        switch (index)
        {
            case 0:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;

            case 1:
                Screen.SetResolution(1600, 900, Screen.fullScreen);
                break;

            case 2:
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                break;
        }
    }

    //private void Set1080p() => Screen.SetResolution(1920, 1080, Screen.fullScreen);
    //private void Set720p() => Screen.SetResolution(1280, 720, Screen.fullScreen);
    //private void Set900p() => Screen.SetResolution(1600, 900, Screen.fullScreen);
}
