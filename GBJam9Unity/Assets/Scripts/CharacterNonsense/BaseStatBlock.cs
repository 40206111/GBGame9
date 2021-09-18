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

    public virtual void Reset()
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
        BaseStatBlock outBlock = CreateInstance<BaseStatBlock>();
        outBlock.MaxHealth = a.MaxHealth + b.MaxHealth;
        outBlock.PhysicalDamage = a.PhysicalDamage + b.PhysicalDamage;
        outBlock.MagicDamage = a.MagicDamage + b.MagicDamage;
        outBlock.PhysicalResistance = a.PhysicalResistance + b.PhysicalResistance;
        outBlock.MagicResistance = a.MagicResistance + b.MagicResistance;
        outBlock.Energy = a.Energy + b.Energy;
        return outBlock;
    }

    public static BaseStatBlock operator *(BaseStatBlock a, float val)
    {
        return val * a;
    }
    public static BaseStatBlock operator *(float val, BaseStatBlock a)
    {
        BaseStatBlock outBlock = CreateInstance<BaseStatBlock>();
        outBlock.MaxHealth = val * a.MaxHealth;
        outBlock.PhysicalDamage = val * a.PhysicalDamage;
        outBlock.MagicDamage = val * a.MagicDamage;
        outBlock.PhysicalResistance = val * a.PhysicalResistance;
        outBlock.MagicResistance = val * a.MagicResistance;
        outBlock.Energy = val * a.Energy;
        return outBlock;
    }
}
