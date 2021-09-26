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
    eChickenClass CurrentChicken;

    private void Start()
    {
        CurrentHealth = PlayerData.GetChickenData(PlayerData.ActiveChicken).CurrentHealth;
        Health.text = $"{CurrentHealth}/{PlayerData.GetChickenData(PlayerData.ActiveChicken).GetMaxHealth()}";

        CurrentEnergy = PlayerData.GetChickenData(PlayerData.ActiveChicken).CurrentStamina;
        Energy.text = $"{CurrentEnergy}/{PlayerData.GetChickenData(PlayerData.ActiveChicken).GetMaxStamina()}";
    }

    void Update()
    {
        if (PlayerData.Seeds != CurrentSeeds)
        {
            CurrentSeeds = PlayerData.Seeds;
            Seeds.text = CurrentSeeds.ToString();
        }


        if (PlayerData.GetChickenData(PlayerData.ActiveChicken).CurrentHealth != CurrentHealth || CurrentChicken != PlayerData.ActiveChicken)
        {
            CurrentHealth = PlayerData.GetChickenData(PlayerData.ActiveChicken).CurrentHealth;
            Health.text = $"{CurrentHealth}/{PlayerData.GetChickenData(PlayerData.ActiveChicken).GetMaxHealth()}";
        }


        if (PlayerData.GetChickenData(PlayerData.ActiveChicken).CurrentStamina != CurrentEnergy || CurrentChicken != PlayerData.ActiveChicken)
        {
            CurrentEnergy = PlayerData.GetChickenData(PlayerData.ActiveChicken).CurrentStamina;
            Energy.text = $"{CurrentEnergy}/{PlayerData.GetChickenData(PlayerData.ActiveChicken).GetMaxStamina()}";
        }

        if (CurrentChicken != PlayerData.ActiveChicken)
        {
            CurrentChicken = PlayerData.ActiveChicken;
        }
    }
}
