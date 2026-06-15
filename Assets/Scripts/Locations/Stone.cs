using UnityEngine;
using Zenject;

public class Stone : MonoBehaviour, IDamageableInt
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private int hp = 3;

    [SerializeField] private AudioClip _clip;

    [Inject] private AudioManager _audioManager;

    private readonly int _damageToStone = 1;

    public void TakeDamage(DamageContext ctx)
    {

        if (ctx.ToolType != ToolType.Pickaxe)
        {
            Debug.Log("Инструмент не тот");
            return;
        }

        Debug.Log("взаимодействие произлшло ");
        hp -= _damageToStone;

        _audioManager.PlaySFXRandomPitch(_clip, 1f, 0.5f, 1.5f);

        ChecHp();

        Instantiate(_particleSystem, gameObject.transform.position, Quaternion.identity);
    }

    private void ChecHp()
    {
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}