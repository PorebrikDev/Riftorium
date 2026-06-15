using UnityEngine;

public class UiAnimatorScript : MonoBehaviour
{
    [SerializeField] private UIAnimatorEnum _animatorEnum;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAnimation(UIAnimatorEnum uIAnimator)
    {
        Debug.Log(uIAnimator.ToString());

        _animator.ResetTrigger(uIAnimator.ToString());
        _animator.SetTrigger(uIAnimator.ToString());
        //_animator.SetTrigger(uIAnimator.ToString());
    }
}
