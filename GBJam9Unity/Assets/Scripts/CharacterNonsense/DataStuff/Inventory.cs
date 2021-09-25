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
        EquiptmentSlot es = PocketItems.Find(x => x.Equiptment == null);
        es.Equiptment = item;
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
