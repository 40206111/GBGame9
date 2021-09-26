using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiBanner : MonoBehaviour
{
    [SerializeField]
    Text Health;
    [SerializeField]
    Text Energy;
    [SerializeField]
    Text Seeds;

    int CurrentHealth;
    int CurrentEnergy;
    int CurrentSeeds;

    void Update()
    {
        if (PlayerData.Seeds != CurrentSeeds)
        {
            CurrentSeeds = PlayerData.Seeds;
            Seeds.text = CurrentSeeds.ToString();
        }


        if (PlayerData.GetChickenData(PlayerData.ActiveChicken).CurrentHealth != CurrentHealth)
        {
            CurrentHealth = PlayerData.GetChickenData(PlayerData.ActiveChicken).CurrentHealth;
            Health.text = $"{CurrentHealth}/{PlayerData.GetChickenData(PlayerData.ActiveChicken).GetMaxHealth()}";
        }


        if (PlayerData.GetChickenData(PlayerData.ActiveChicken).CurrentStamina != CurrentEnergy)
        {
            CurrentEnergy = PlayerData.GetChickenData(PlayerData.ActiveChicken).CurrentStamina;
            Energy.text = $"{CurrentEnergy}/{PlayerData.GetChickenData(PlayerData.ActiveChicken).GetMaxStamina()}";
        }
    }
}
