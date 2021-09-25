using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenData
{
    public EntityBaseInfo EntityInfo;
    public StatGrowth StatGrowth;

    public BaseStatBlock BaseStats = ScriptableObject.CreateInstance<BaseStatBlock>();
    public BaseStatBlock EquiptmentStats = ScriptableObject.CreateInstance<BaseStatBlock>();
    public int Level = 1;

    public EquiptmentLayout EquippedItems = new EquiptmentLayout();

    protected int _currentHealth = 0;
    protected int _currentStamina = 0;
    public eChickenClass Class = eChickenClass.none;

    public ChickenData(eChickenClass chickClass)
    {
        Class = chickClass;
    }
    //public ChickenData(EntityBaseInfo baseInfo, StatGrowth statGrowth, eChickenClass chickClass)
    //{
    //    EntityInfo = baseInfo;
    //    StatGrowth = statGrowth;
    //    Class = chickClass;
    //    HealToFull();
    //}

    public void HealToFull()
    {
        CurrentHealth = GetMaxHealth();
    }

    public void StaminaToFull()
    {
        _currentStamina = GetMaxStamina();
    }

    public int CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

    public int CurrentStamina
    {
        get { return _currentStamina; }
        set { _currentStamina = value; }
    }

    public int GetMaxHealth()
    {
        return (int)(BaseStats.MaxHealth + EquiptmentStats.MaxHealth);
    }

    public int GetMaxStamina()
    {
        return (int)(BaseStats.Energy + EquiptmentStats.Energy);
    }

    protected virtual void CalculatedStatsForLevel()
    {
        int levelMult = Level - 1;
        BaseStats = EntityInfo + StatGrowth * levelMult;
    }

    protected virtual void CalculateArmourStats()
    {
        EquiptmentStats.Reset();
        foreach (EquiptmentSlot es in EquippedItems.GetAllEquipped())
        {
            EquiptmentStats += es.Equiptment;
        }
    }
}
