using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public EquiptmentLayout EquippedItems = new EquiptmentLayout();
    public List<EquiptmentSlot> PocketItems = new List<EquiptmentSlot>();

    public List<EquiptmentSlot> GetAllItems()
    {
        List<EquiptmentSlot> outList = EquippedItems.GetAllEquipped();
        outList.AddRange(PocketItems);
        return outList;
    }

    public void AddItem(ItemDetails item)
    {
        if (item.ItemType == eItemType.chestPiece)
        {
            PlayerData.MeleeChicken.EquippedItems.EquipItem(item, item.ItemType);
        }

        EquiptmentSlot es = PocketItems.Find(x => x.Equiptment == null);
        if (es != null)
        {
            es.Equiptment = item;
        }
        else
        {
            PocketItems.Add(new EquiptmentSlot());
            PocketItems[PocketItems.Count - 1].AddItem(item);
        }
    }

    public void RemoveItem(ItemDetails item)
    {
        EquiptmentSlot es = PocketItems.Find(x => x.Equiptment == item);
        if (es != null)
        {
            es.Equiptment = null;
        }
    }
}
