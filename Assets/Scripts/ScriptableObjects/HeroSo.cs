using UnityEngine;
[CreateAssetMenu()]

public class HeroSO : ScriptableObject
{
    public string heroName; // Имя
    public float maxHealth; //Максимальное здоровье
    public float MaxMana;
    public int heroDamageAmount; //Урон
    public float heroBaseSpeed; //Скорость передвижения
    public float heroDashMultiplier; // ускорение
    public float heroJerkMultiplier; // ускорение кувырка
    public float herpJerkTimer; // Время кувырка
    public float herpJerkStength; // Сила кувырка
    public float defense; // Броня
    public float damageRecoveryTime; //Задержка перед получением урона
    public float critChance; // Шанс крита
    public float critDamage; //Урон крита
}

