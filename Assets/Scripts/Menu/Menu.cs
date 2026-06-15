using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingButton;

    [SerializeField] private GameObject _settings;

    [Inject] private readonly InputService _input;

    private void Awake()
    {
        _playButton.onClick.AddListener(TapPlay);
        _closeButton.onClick.AddListener(CloseGame);
        _settingButton.onClick.AddListener(ChangeSetting);

        _input.OnMenuStarted += Activate;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(TapPlay);
        _closeButton.onClick.RemoveListener(CloseGame);
        _settingButton.onClick.RemoveListener(ChangeSetting);


        _input.OnMenuStarted -= Activate;
    }

    public void CloseGame() => Application.Quit();

    private void TapPlay()
    {
        gameObject.SetActive(false);
        _input.IsPaused = false;
        CloseSetting();
    }

    private void Activate()
    {
        if (_input.IsPaused)
        {
            gameObject.SetActive(true);
        }

        else
        {
            gameObject.SetActive(false);
            CloseSetting();
        }
    }

    private void ChangeSetting()
    {
        _settings.SetActive(!_settings.activeSelf);
    }

    public void CloseSetting()
    { 
    _settings?.SetActive(false);
    }
}
