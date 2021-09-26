using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickSelectInfoFiller : MonoBehaviour
{
    [System.Serializable]
    public struct ClassToSprite
    {
        public eChickenClass ChickenClass;
        public Sprite ChickenSprite;
    }
    public eChickenClass ChickenClass;
    [HideInInspector]
    public bool ChickenAvailable = true;
    public Image ChickenSprite;
    public Text HealthText;
    public Text StaminaText;
    public List<ClassToSprite> ChickenSprites = new List<ClassToSprite>();

    public void FillInfo()
    {
        if(ChickenClass == eChickenClass.none)
        {
            Debug.Log("No class set!");
            return;
        }
        ChickenData data = PlayerData.GetChickenData(ChickenClass);
        if (data != null)
        {
            ChickenSprite.sprite = ChickenSprites.Find(x => x.ChickenClass == ChickenClass).ChickenSprite;
            HealthText.text = $"{data.CurrentHealth}/\n{data.GetMaxHealth()}";
            StaminaText.text = $"{data.CurrentStamina}/\n{data.GetMaxStamina()}";
            ChickenAvailable = true;
        }
        else
        {
            ChickenSprite.sprite = ChickenSprites.Find(x => x.ChickenClass == eChickenClass.none).ChickenSprite;
            HealthText.text = "???";
            StaminaText.text = "???";
            ChickenAvailable = false;
        }
    }
}
