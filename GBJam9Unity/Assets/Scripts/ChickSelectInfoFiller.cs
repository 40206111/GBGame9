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
        ChickenSprite.sprite = ChickenSprites.Find(x => x.ChickenClass == ChickenClass).ChickenSprite;
        ChickenData data = PlayerData.GetChickenData(ChickenClass);
        HealthText.text = $"{data.CurrentHealth}/\n{data.GetMaxHealth()}";
        StaminaText.text = $"{data.CurrentStamina}/\n{data.GetMaxStamina()}";
    }
}
