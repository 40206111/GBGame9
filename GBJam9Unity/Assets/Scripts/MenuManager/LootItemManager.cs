using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItemManager : PopulatingMenuManager
{
    protected static LootItemManager _instance;
    public static LootItemManager Instance { get { return _instance; } }
    protected void Awake()
    {
        _instance = this;
        gameObject.SetActive(false);
    }

    protected Lootable CurrentLootable = null;

    protected override void Update()
    {
        if (!IsInputTarget())
        {
            return;
        }
        base.Update();
        if (Input.GetButtonDown("AButton"))
        {
            TransferItemToInventory();
        }
        else if (Input.GetButtonDown("BButton"))
        {
            Display(null);
        }
    }

    private void TransferItemToInventory()
    {
        MenuItemBase mi = MenuItems[CurrentIndex];
        PlayerData.Inventory.AddItem(CurrentLootable.TakeItem(mi.CurrentText));
        RemoveMenuItem(mi);
        Destroy(mi.gameObject);
    }

    public void Display(Lootable lootable = null)
    {
        CleanUp();
        if (lootable?.GetItems().Count == 0)
        {
            DialogueBoxControl.Instance.PrintText("Nothing left here.", closeAfterText: true);
            return;
        }
        gameObject.SetActive(lootable != null);
        if (lootable != null)
        {
            if (InputKey == null)
            {
                GameManager.Instance.AddInputTarget(gameObject.GetInstanceID());
            }
            PopulateMenu(lootable.GetItems());
        }
        else
        {
            if (InputKey == null)
            {
                GameManager.Instance.RemoveInputTarget(gameObject.GetInstanceID());
            }
        }
        CurrentLootable = lootable;
    }

    public override void RemoveMenuItem(MenuItemBase menuItem)
    {
        base.RemoveMenuItem(menuItem);
        if (MenuItems.Count == 0)
        {
            Display(null);
        }
    }
}
