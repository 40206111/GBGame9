using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenuItem : MenuItemBase
{
    public ItemDetails Item;
    protected int itemCount = 0;
    public int ItemCount
    {
        get { return itemCount; }
        protected set
        {
            itemCount = value;
            ItemCountText.text = $"x{itemCount}" + (itemCount < 10 ? "  " : itemCount < 100 ? " " : "");
        }
    }
    protected Text ItemNameText;
    [SerializeField]
    protected Text ItemCountText;
    protected override void Awake()
    {
        base.Awake();
        ItemNameText = GetComponent<Text>();
    }
    public override bool PerformAction()
    {
        // ~~~ remove from list, add to inventory
        return true;
    }

    public void SetItem(ItemDetails item)
    {
        Item = item;
        ItemNameText.text = item.name;
        ItemCount = 1;
    }

    public void AddToCount(int toAdd = 1)
    {
        ItemCount += toAdd;
    }

    public void SubFromCount(int toSub = 1)
    {
        ItemCount -= toSub;
    }
}
