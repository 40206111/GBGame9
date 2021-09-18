using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/StatGrowth", fileName = "StatGrowth")]
public class StatGrowth : BaseStatBlock
{
    protected override void Reset()
    {
        MaxHealth = 3;
        PhysicalDamage = 1;
        MagicDamage = 0;
        PhysicalResistance = 1;
        MagicResistance = 0;
        Energy = 2;
    }
}
