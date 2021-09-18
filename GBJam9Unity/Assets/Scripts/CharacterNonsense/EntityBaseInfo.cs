using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/EntityBaseInfo", fileName = "EntityBaseInfo")]
public class EntityBaseInfo : BaseStatBlock
{
    public string Name = "";

    public override void Reset()
    {
        MaxHealth = 10;
        PhysicalDamage = 1;
        MagicDamage = 1;
        PhysicalResistance = 0;
        MagicResistance = 0;
        Energy = 10;
    }
}
