using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItemManager : MenuItemManager
{
    protected static LootItemManager _instance;
    public static LootItemManager Instance { get { return _instance; } }
    private void Awake()
    {
        _instance = this;
        gameObject.SetActive(false);
    }

    protected int ItemsScrolled = 0;
    [SerializeField]
    protected RectTransform ItemHolder;
    [SerializeField]
    protected RectTransform LootMenuItemPrefab;
    protected int VisibleEntries = 5;
    protected int EntryHeight = 14;
    protected int InitialGap = 10;


    public void Display(Lootable lootable = null)
    {
        CleanUp();
        gameObject.SetActive(lootable != null);
        if (lootable != null)
        {
            GameManager.Instance.AddInputTarget(GetInstanceID());
            PopulateMenu(lootable);
        }
        else
        {
            GameManager.Instance.RemoveInputTarget(GetInstanceID());
        }
    }

    protected void PopulateMenu(Lootable lootable)
    {
        List<ItemMenuItem> items = new List<ItemMenuItem>();
        foreach (ItemDetails id in lootable.GetItems())
        {
            if (CheckForItem(items, id, out int index))
            {
                items[index].AddToCount(1);
            }
            else
            {
                ItemMenuItem imi = Instantiate(LootMenuItemPrefab, ItemHolder).GetComponent<ItemMenuItem>();
                imi.SetItem(id);
                imi.transform.localPosition += (Vector3.down * (InitialGap + EntryHeight * items.Count));
                items.Add(imi);
            }
        }
        MenuItems.AddRange(items);
    }

    protected bool CheckForItem(List<ItemMenuItem> items, ItemDetails item, out int index)
    {
        index = items.FindIndex(x => x.Item == item);
        return index >= 0;
    }

    protected override void CleanUp()
    {
        base.CleanUp();
        ItemsScrolled = 0;
        ItemHolder.anchoredPosition = Vector3.zero;
        foreach (MenuItemBase mib in MenuItems)
        {
            Destroy(mib.gameObject);
        }
        MenuItems.Clear();
    }

    protected override void ChangeSelectedMenuItem(int change)
    {
        if (change == 0)
        {
            return;
        }
        base.ChangeSelectedMenuItem(change);

        int difference = 0;
        if (change < 0 && CurrentIndex < ItemsScrolled)
        {
            difference = CurrentIndex - ItemsScrolled;
        }
        else if (change > 0 && CurrentIndex >= ItemsScrolled + VisibleEntries - 1)
        {
            difference = CurrentIndex - (ItemsScrolled + VisibleEntries - 2);
        }
        CurrentIndex += difference;
        MoveTextHolder(EntryHeight * difference);
    }

    protected void MoveTextHolder(int amount)
    {
        ItemHolder.Translate(Vector3.up * amount);
    }

    public void RemoveMenuItem(MenuItemBase menuItem)
    {
        int index = MenuItems.IndexOf(menuItem);
        if (MenuItems.Count != 0)
        {
            if (index != MenuItems.Count - 1)
            {
                ChangeSelectedMenuItem(1);
            }
            else
            {
                ChangeSelectedMenuItem(-1);
            }
        }
        MenuItems.RemoveAt(index);
        for (int i = index; i < MenuItems.Count; ++i)
        {
            MenuItems[i].transform.Translate(Vector3.up * EntryHeight);
        }

    }
}
