using UnityEngine;

public readonly struct DamageContext
{
    public readonly int Damage;
    public readonly Transform Source;
    public readonly ToolType ToolType;

    public DamageContext(
        int damage,
        Transform source,
        ToolType toolType = ToolType.Zero)
    {
        Damage = damage;
        Source = source;
        ToolType = toolType;
    }
}
