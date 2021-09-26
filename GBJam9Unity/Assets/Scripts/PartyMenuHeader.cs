using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyMenuHeader : MonoBehaviour
{
    [System.Serializable]
    private struct ChickenToSprite
    {
        public eChickenClass ChickenClass;
        public Sprite Sprite;
    }
    [SerializeField]
    Text Title;
    [SerializeField]
    List<Image> Chickens = new List<Image>();
    [SerializeField]
    List<ChickenToSprite> Sprites = new List<ChickenToSprite>();

    public void ChangeChicken(eChickenClass chickClass)
    {
        Title.text = TitleWriter(chickClass);
        Sprite sprite = Sprites.Find(x => x.ChickenClass == chickClass).Sprite;
        if(sprite == null)
        {
            Debug.Log($"No chicken sprite for: {chickClass}");
        }
        foreach(Image im in Chickens)
        {
            im.sprite = sprite;
        }
    }

    private string TitleWriter(eChickenClass chickClass)
    {
        string outString = "";
        switch (chickClass)
        {
            case eChickenClass.melee:
                outString += "Melee";
                break;
            case eChickenClass.mage:
                outString += "Mage";
                break;
            case eChickenClass.ranger:
                outString += "Ranger";
                break;
            default:
                outString += "Problem";
                break;
        }
        outString += " Chicken";
        return outString;
    }
}
