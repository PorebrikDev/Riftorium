using UnityEngine;
using Zenject;

public class Respawn : MonoBehaviour
{

    [SerializeField] private UI_Death _uiDeath;

    [Inject] private readonly UiAnimatorScript _uiAnimator;
    [Inject] private readonly Player _player;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _uiDeath.OnRespStart += RespawnPlay;
    }

    private void OnDestroy()
    {
        _uiDeath.OnRespStart -= RespawnPlay;
    }

    public void RespawnPlay()
    {
        LoadScreen();

        _player.gameObject.SetActive(true);
        _player.Respawn();
    }

    public void LoadScreen()
    {
        _uiAnimator.PlayAnimation(UIAnimatorEnum.GoBlackScreen);
    }
}
