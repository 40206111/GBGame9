using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedMenuManager : PopulatingMenuManager
{
    [SerializeField]
    protected RectTransform ItemIconsHolder;


    public override void PopulateMenu(List<EquiptmentSlot> inDetails)
    {
        CleanUp();
        List<ItemMenuItem> items = new List<ItemMenuItem>();
        int itemCount = 0;
        foreach (EquiptmentSlot es in inDetails)
        {

            ItemMenuItem imi = Instantiate(LootMenuItemPrefab, ItemHolder).GetComponent<ItemMenuItem>();
            if (es.Equiptment == null)
            {
                imi.GetComponent<Text>().text = $"No {GoodEnumName(es.RequiredType)}";
            }
            else
            {
                imi.SetItem(es);
            }
            imi.transform.localPosition += (Vector3.down * (InitialGap + EntryHeight * items.Count));
            items.Add(imi);
            itemCount++;
        }
        MenuItems.AddRange(items);

        CurrentIndex = 0;
        JustHighlight(CurrentIndex);
        PrintCurrentItem();
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

    protected override void ChangeSelectedMenuItem(int change, bool force = false)
    {
        base.ChangeSelectedMenuItem(change);
        if (change != 0)
        {
            PrintCurrentItem();
        }
    }

    protected virtual void PrintCurrentItem()
    {
        ItemMenuItem itemMenuItem = (ItemMenuItem)MenuItems[CurrentIndex];
        if (itemMenuItem.Slot.Equiptment != null)
        {
            PrintToDialogue(itemMenuItem.Slot.Equiptment.ToString());
        }
        else
        {
            PrintToDialogue("No item equipped.");
        }
    }
    protected virtual void PrintToDialogue(string str)
    {
        DialogueBoxControl.Instance.PrintText(str, 0.0f);
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
