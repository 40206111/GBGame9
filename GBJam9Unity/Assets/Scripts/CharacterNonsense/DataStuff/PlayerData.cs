using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eChickenClass { none = -1, melee, mage, ranger }

public enum eKeyEvents
{
    None = 1 << 0,
    WokenUp = 1 << 1,
    MetMage = 1 << 2,
    MetRanger = 1 << 3
}

public static class PlayerData
{
    public static bool DataLoaded = false;

    public static eKeyEvents KeyEvents;

    public static Inventory Inventory;
    public static ChickenData MeleeChicken;
    public static ChickenData MageChicken;
    public static ChickenData RangerChicken;

    public static eChickenClass ActiveChicken = eChickenClass.melee;
    private static Dictionary<eChickenClass, bool> _availableChickens = new Dictionary<eChickenClass, bool>()
    {
        {eChickenClass.melee, true},
        {eChickenClass.mage, false},
        {eChickenClass.ranger, false}
    };

    public static int Seeds = 0;

    public static void ConstructClasses()
    {
        Inventory = new Inventory();
        MeleeChicken = new ChickenData(eChickenClass.melee);
        MageChicken = new ChickenData(eChickenClass.mage);
        RangerChicken = new ChickenData(eChickenClass.ranger);
    }

    public static ChickenData GetChickenData(eChickenClass chickClass, bool forceDataReturn = false)
    {
        if (!forceDataReturn && _availableChickens.ContainsKey(chickClass) && !_availableChickens[chickClass])
        {
            return null;
        }
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

    public static void SetChickenAvailable(eChickenClass chickClass, bool available)
    {
        if (_availableChickens.ContainsKey(chickClass))
        {
            _availableChickens[chickClass] = available;
        }
    }

    public static bool IsChickenAvailable(eChickenClass chickClass)
    {
        if(_availableChickens.ContainsKey(chickClass))
        {
            return _availableChickens[chickClass];
        }
        return false;
    }


#if DEVELOPMENT_BUILD || UNITY_EDITOR
    public static void OnEnable()
    {
        DebugConsole.ConsoleEvent += OnDebugConsole;
    }

    public static void OnDisable()
    {
        DebugConsole.ConsoleEvent -= OnDebugConsole;
    }

    static void OnDebugConsole(string message)
    {
        var parts = message.Split(' ');


        if (parts[0].ToLower().Equals("player"))
        {
            if (parts[1].ToLower() == "seeds")
            {
                AddSeeds(parts[2]);
            }
        }
    }

    static void AddSeeds(string seeds)
    {
        if (int.TryParse(seeds, out int seedNumber))
        {
            Seeds += seedNumber;
        }
    }
#endif
}
