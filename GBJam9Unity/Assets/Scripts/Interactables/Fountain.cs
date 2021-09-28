using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour, IInteractable
{
    public void RunInteraction()
    {
        int chickens = 0;
        chickens += RestoreChicken(eChickenClass.melee);
        chickens += RestoreChicken(eChickenClass.mage);
        chickens += RestoreChicken(eChickenClass.ranger);

        string dialogue = "";
        if (chickens == 1)
        {
            dialogue = "You feel refreshed.";
        }
        else
        {
            dialogue = "You all feel refreshed.";
        }
        DialogueBoxControl.Instance.PrintText(dialogue, closeAfterText: true);
    }

    private int RestoreChicken(eChickenClass chickClass)
    {
        ChickenData data = PlayerData.GetChickenData(chickClass);
        if (data != null)
        {
            data.HealToFull();
            data.StaminaToFull();
            return 1;
        }
        return 0;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
