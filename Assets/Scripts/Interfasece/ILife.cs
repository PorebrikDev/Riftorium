
using System;

public interface ILife 
{
    int CurrentLife { get; }
    int MaxLife { get; }

    event Action<int> OnTakeDamage;
}
