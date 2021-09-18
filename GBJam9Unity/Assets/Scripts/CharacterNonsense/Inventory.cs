using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public EquiptmentLayout EquippedItems;
    public List<EquiptmentSlot> PocketItems;

    public List<EquiptmentSlot> GetAllItems()
    {
        List<EquiptmentSlot> outList = EquippedItems.GetAllItems();
        outList.AddRange(PocketItems);
        return outList;
    }
}
