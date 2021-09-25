using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eItemType { none = -1, headPiece, chestPiece, legWear, footWear, trinket, weapon, consumable}

[CreateAssetMenu(menuName = "ScriptableObjects/ItemDetails", fileName = "ItemDetails")]
public class ItemDetails : BaseStatBlock
{
    public string Name = "";
    public eItemType ItemType = eItemType.none;
    public string FlavourText = "Spicy";
    public Sprite ItemImage;
    public int Price;
}
