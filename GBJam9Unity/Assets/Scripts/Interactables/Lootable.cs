using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootable : MonoBehaviour,IInteractable
{
    List<ItemDetails> Items = new List<ItemDetails>();

    public void RunInteraction()
    {
        // ~~~ open loot window
        // ~~~ print loot names
    }

    public ItemDetails TakeItem(string itemName)
    {
        int index = Items.FindIndex(x => x.Name.Equals(itemName));
        if (index >= 0)
        {
            ItemDetails outItem = Items[index];
            Items.RemoveAt(index);
            return outItem;
        }
        return null;
    }
}
