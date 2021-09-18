using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "ScriptableObjects/EntityBaseStats", fileName = "EntityBaseStats")]
public class BaseStatBlock : ScriptableObject
{
    public float MaxHealth = 0;
    public float PhysicalDamage = 0;
    public float MagicDamage = 0;
    public float PhysicalResistance = 0;
    public float MagicResistance = 0;
    public float Energy = 0;

    protected virtual void Reset()
    {
        MaxHealth = 0;
        PhysicalDamage = 0;
        MagicDamage = 0;
        PhysicalResistance = 0;
        MagicResistance = 0;
        Energy = 0;
    }

    protected virtual void Copy(BaseStatBlock other)
    {
        MaxHealth = other.MaxHealth;
        PhysicalDamage = other.PhysicalDamage;
        MagicDamage = other.MagicDamage;
        PhysicalResistance = other.PhysicalResistance;
        MagicResistance = other.MagicResistance;
        Energy = other.Energy;
    }

    public static BaseStatBlock operator +(BaseStatBlock a, BaseStatBlock b)
    {
        return new BaseStatBlock
        {
            MaxHealth = a.MaxHealth + b.MaxHealth,
            PhysicalDamage = a.PhysicalDamage + b.PhysicalDamage,
            MagicDamage = a.MagicDamage + b.MagicDamage,
            PhysicalResistance = a.PhysicalResistance + b.PhysicalResistance,
            MagicResistance = a.MagicResistance + b.MagicResistance,
            Energy = a.Energy + b.Energy
        };
    }

    public static BaseStatBlock operator *(BaseStatBlock a, float val)
    {
        return val * a;
    }
    public static BaseStatBlock operator *(float val, BaseStatBlock a)
    {
        return new BaseStatBlock
        {
            MaxHealth = val * a.MaxHealth,
            PhysicalDamage = val * a.PhysicalDamage,
            MagicDamage = val * a.MagicDamage,
            PhysicalResistance = val * a.PhysicalResistance,
            MagicResistance = val * a.MagicResistance,
            Energy = val * a.Energy
        };
    }
}
