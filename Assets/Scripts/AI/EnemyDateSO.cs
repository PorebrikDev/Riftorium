using UnityEngine;

public abstract class EnemyDateSO : ScriptableObject
{
    public string AttackName;
    public string AnimatorTrigger;
    public bool HasHitbox = true;
    public int HitboxIndex;

    public int Damage;
    public float TimeBoxEnabled = 0.2f;
    public float Cooldown;

    public float CriticalHit;
    public float CritChance;

    public abstract bool CanEnter(EnemyBrain brain);
    public abstract void Execute(EnemyBrain brain);
}
