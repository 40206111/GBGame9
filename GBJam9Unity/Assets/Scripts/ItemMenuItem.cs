using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenuItem : MenuItemBase
{
    public EquiptmentSlot Slot;
    //protected int itemCount = 0;
    public int ItemCount
    {
        get { return Slot.Count; }
        protected set
        {
            Slot.Count = value;
            RefreshNumberText();
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

    /*public void SetItem(ItemDetails item)
    {
        Slot.Equiptment = item;
        ItemNameText.text = item.name;
        ItemCount = 1;
    }*/

    public void CleanUp()
    {
        Slot = null;
        ItemNameText.text = ItemCountText.text = "";
    }

    public void SetItem(EquiptmentSlot item)
    {
        Slot = item;
        RefreshAllText();
    }

    public void RefreshNameText()
    {
        ItemNameText.text = Slot.Equiptment.name;
    }

    public void RefreshNumberText()
    {
        ItemCountText.text = $"x{ItemCount}" + (ItemCount < 10 ? "  " : ItemCount < 100 ? " " : "");
    }

    public void RefreshAllText()
    {
        RefreshNameText();
        RefreshNumberText();
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
