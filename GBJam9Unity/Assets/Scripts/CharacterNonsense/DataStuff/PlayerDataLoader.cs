using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataLoader : MonoBehaviour
{
    [System.Serializable]
    public struct ClassToStats
    {
        public eChickenClass ChickenClass;
        public BaseStatBlock Stats;
    }

    [SerializeField]
    List<ClassToStats> BaseStats = new List<ClassToStats>();
    [SerializeField]
    List<ClassToStats> GrowthStats = new List<ClassToStats>();

    private void Start()
    {
        LoadData();
    }

    public void LoadData()
    {
        if (PlayerData.DataLoaded)
        {
            return;
        }
        PlayerData.DataLoaded = true;

        PlayerData.ConstructClasses();

        SetStats(PlayerData.GetChickenData(eChickenClass.melee, true));
        SetStats(PlayerData.GetChickenData(eChickenClass.mage, true));
        SetStats(PlayerData.GetChickenData(eChickenClass.ranger, true));
    }

    private void SetStats(ChickenData chicken)
    {
        chicken.BaseStats = BaseStats.Find(x => x.ChickenClass == chicken.Class).Stats;
        chicken.StatGrowth = (StatGrowth)GrowthStats.Find(x => x.ChickenClass == chicken.Class).Stats;

        chicken.HealToFull();
        chicken.StaminaToFull();
    }
}
