using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEntityBase : MonoBehaviour
{
    protected EntityBaseInfo EntityInfo;
    protected StatGrowth StatGrowth;

    protected BaseStatBlock BaseStats = ScriptableObject.CreateInstance<BaseStatBlock>();
    protected BaseStatBlock EquiptmentStats = ScriptableObject.CreateInstance<BaseStatBlock>();
    protected int Level = 3;

    protected Inventory Inventory;

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    protected virtual void CalculatedStatsForLevel()
    {
        int levelMult = Level - 1;
        BaseStats = EntityInfo + StatGrowth * levelMult;
    }

    protected virtual void CalculateArmourStats()
    {
        EquiptmentStats.Reset();
        foreach (EquiptmentSlot es in Inventory.EquippedItems.GetAllEquipped())
        {
            EquiptmentStats += es.Equiptment;
        }
    }
}
