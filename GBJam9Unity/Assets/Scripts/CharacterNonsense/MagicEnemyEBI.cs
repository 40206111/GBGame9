using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MagicEnemyEBI", fileName = "MagicEnemyEBI")]
public class MagicEnemyEBI : EntityBaseInfo
{
    protected override void Reset()
    {
        Name = "Magician";
        MaxHealth = 5;
        PhysicalDamage = 0;
        MagicDamage = 5;
        PhysicalResistance = 0;
        MagicResistance = 10;
        Energy = 20;
    }
}
