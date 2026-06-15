using System;
using UnityEngine;
using Zenject;

public class PlayerVisual : MonoBehaviour
{
    public event Action OnDeath;

    [SerializeField] private Animator animator;
    [SerializeField] private ActiveWeapon activeWeapon;

    [Inject] private readonly Player _player;

    private void Start()
    {
        _player.OnPlayerDeath += Player_OnPlayerDeath;
    }

    private void OnDestroy()
    {
        _player.OnPlayerDeath -= Player_OnPlayerDeath;
    }

    public void UpdateWeaponAnimator(Vector2 move)
    {
        animator.SetFloat("Speed", move.sqrMagnitude);

        if (move.sqrMagnitude > 0.01f)
        {
            animator.SetFloat("MoveX", move.x);
            animator.SetFloat("MoveY", move.y);
        }
    }

    public Animator GetAnimator() => animator;

    public void UIDeathMenu()
    {
        OnDeath?.Invoke();
       _player.gameObject.SetActive(false);
    }

    private void Player_OnPlayerDeath()
    {
        animator.SetBool("IsDie", true);
    }
}