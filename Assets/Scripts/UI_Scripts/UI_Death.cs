using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UI_Death : MonoBehaviour
{
    public event Action OnRespStart;

    [SerializeField] private PlayerVisual _playerVisual;
    [SerializeField] private Menu _menu;
    [SerializeField] private Button _buttonResp;
    [SerializeField] private Button _buttonQuit;

    private void Start()
    {
        _playerVisual.OnDeath += OpenDeathScreen;
        _buttonResp.onClick.AddListener(RespawnPlay);
        _buttonQuit.onClick.AddListener(QuitGame);

        gameObject.SetActive(false);
    }

    private void OpenDeathScreen()
    {
        gameObject.SetActive(true);
    }

    private void RespawnPlay()
    {
        OnRespStart?.Invoke();
        gameObject?.SetActive(false);
    }

    private void QuitGame()
    {
        _menu.CloseGame();
    }

    private void OnDestroy()
    {
        _buttonResp?.onClick.RemoveListener(RespawnPlay);
        _playerVisual.OnDeath -= OpenDeathScreen;
        _buttonQuit.onClick.RemoveListener(QuitGame);
    }
}