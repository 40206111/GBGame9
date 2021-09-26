using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedMenuManager : PopulatingMenuManager
{
    [SerializeField]
    protected RectTransform ItemIconsHolder;


    public override void PopulateMenu(List<ItemDetails> inDetails)
    {
        CleanUp();
        List<ItemMenuItem> items = new List<ItemMenuItem>();
        int itemCount = 0;
        foreach (ItemDetails id in inDetails)
        {

            ItemMenuItem imi = Instantiate(LootMenuItemPrefab, ItemHolder).GetComponent<ItemMenuItem>();
            if (id == null)
            {
                imi.GetComponent<Text>().text = $"No {GoodEnumName(GetTypeFromInt(itemCount))}";
            }
            else
            {
                imi.SetItem(id);
            }
            imi.transform.localPosition += (Vector3.down * (InitialGap + EntryHeight * items.Count));
            items.Add(imi);
            itemCount++;
        }
        MenuItems.AddRange(items);

        CurrentIndex = 0;
        JustHighlight(CurrentIndex);
    }

    protected eItemType GetTypeFromInt(int i)
    {
        if (i < (int)eItemType.trinket)
        {
            return (eItemType)i;
        }
        else if (i == 4)
        {
            return eItemType.weapon;
        }
        else
        {
            return eItemType.trinket;
        }
    }

    protected string GoodEnumName(eItemType type)
    {
        switch (type)
        {
            case eItemType.headPiece:
                return "Head Piece";
            case eItemType.chestPiece:
                return "Chest Piece";
            case eItemType.legWear:
                return "Legwear";
            case eItemType.footWear:
                return "Footwear";
            case eItemType.weapon:
                return "Weapon";
            case eItemType.trinket:
                return "Trinket";
            default:
                return "Nothing";
        }
    }

    protected override void ChangeSelectedMenuItem(int change)
    {
        base.ChangeSelectedMenuItem(change);
        if (change != 0)
        {
            ItemMenuItem itemMenuItem = (ItemMenuItem)MenuItems[CurrentIndex];
            if (itemMenuItem.Item != null)
            {
                DialogueBoxControl.Instance.PrintText(itemMenuItem.Item.ToString());
            }
        }
    }

    public override void CleanUp()
    {
        base.CleanUp();
        MoveTextHolder(0);
    }

    protected override void MoveTextHolder(int amount)
    {
        base.MoveTextHolder(amount);
        float newY = ItemHolder.position.y;
        Vector3 pos = ItemIconsHolder.position;
        pos.y = newY;
        ItemIconsHolder.position = pos;
    }
}
