using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eChickenClass { none = -1, melee, mage, ranger }

public static class PlayerData
{
    public static bool DataLoaded = false;

    public static Inventory Inventory;
    public static ChickenData MeleeChicken;
    public static ChickenData MageChicken;
    public static ChickenData RangerChicken;

    public static void ConstructClasses()
    {
        Inventory = new Inventory();
        MeleeChicken = new ChickenData(eChickenClass.melee);
        MageChicken = new ChickenData(eChickenClass.mage);
        RangerChicken = new ChickenData(eChickenClass.ranger);
    }

    public static ChickenData GetChickenData(eChickenClass chickClass)
    {
        switch (chickClass)
        {
            case eChickenClass.melee:
                return MeleeChicken;
            case eChickenClass.mage:
                return MageChicken;
            case eChickenClass.ranger:
                return RangerChicken;
            default:
                return MeleeChicken;
        }
    }
    //public static 
}
