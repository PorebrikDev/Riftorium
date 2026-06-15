using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    public static PlayerEffects instance;
    private Animator animator;

    private void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }

    public void HillEfect()
    {
        animator.SetTrigger("hill");
    }
}
