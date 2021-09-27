using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootable : MonoBehaviour,IInteractable
{
    [SerializeField]
    List<EquiptmentSlot> Items = new List<EquiptmentSlot>();
    [SerializeField]
    Sprite LootedSprite;

    public void RunInteraction()
    {
        // ~~~ open loot window
        // ~~~ print loot names
        LootItemManager.Instance.Display(this);
    }

    public EquiptmentSlot TakeItem(string itemName)
    {
        int index = Items.FindIndex(x => x.Equiptment.Name.Equals(itemName));
        if (index >= 0)
        {
            EquiptmentSlot outSlot = Items[index];
            Items.RemoveAt(index);
            DoEmptyCheck();
            return outSlot;
        }
        return null;
    }

    public EquiptmentSlot TakeItem(EquiptmentSlot item)
    {
        if (Items.Contains(item))
        {
            EquiptmentSlot outItem = Items[Items.FindIndex(x => x.Equiptment == item.Equiptment)];
            Items.Remove(outItem);
            DoEmptyCheck();
            return outItem;
        }
        return null;
    }

    private void DoEmptyCheck()
    {
        if (Items.Count == 0 && LootedSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = LootedSprite;
        }
    }

    public List<EquiptmentSlot> GetItems()
    {
        return Items;
    }
}
