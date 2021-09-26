using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopControl : MenuItemManager
{
    ShopItem SpecialItem;
    ShopItem DefenseItem;
    ShopItem AttackItem;

    [SerializeField]
    ItemPool SpecialItemPool;
    [SerializeField]
    ItemPool DefenseItemPool;
    [SerializeField]
    ItemPool AttackItemPool;

    [SerializeField]
    string ShopText;


    protected void Start()
    {
        base.Start();
        SpecialItem = MenuItems[0] as ShopItem;
        DefenseItem = MenuItems[1] as ShopItem;
        AttackItem = MenuItems[2] as ShopItem;
        PopulateShop();
        StartCoroutine(WaitForShopKeeperToFinish());
    }

    IEnumerator WaitForShopKeeperToFinish()
    {
        yield return StartCoroutine(DialogueBoxControl.Instance.PrintTextAndWait(ShopText, closeAfterText: true));

        (MenuItems[CurrentIndex] as ShopItem).IsHighlighted = true;
    }

    protected override void JustHighlight(int index)
    {
       (MenuItems[index] as ShopItem).IsHighlighted = true;
        Arrow.SetParent((MenuItems[index] as ShopItem).ArrowHolder);
        Arrow.localPosition = Vector2.zero;
    }

    private void PopulateShop()
    {
        int random = Random.Range(0, SpecialItemPool.Items.Count);

        SpecialItem.SetUp(SpecialItemPool.Items[random]);

        random = Random.Range(0, DefenseItemPool.Items.Count);

        DefenseItem.SetUp(DefenseItemPool.Items[random]);

        random = Random.Range(0, AttackItemPool.Items.Count);

        AttackItem.SetUp(AttackItemPool.Items[random]);

    }


#if DEVELOPMENT_BUILD || UNITY_EDITOR
    protected void OnEnable()
    {
        base.OnEnable();
        DebugConsole.ConsoleEvent += OnDebugConsole;
    }

    protected void OnDisable()
    {
        base.OnDisable();
        DebugConsole.ConsoleEvent -= OnDebugConsole;
    }

    private void OnDebugConsole(string message)
    {
        var parts = message.Split(' ');

        
        if (parts[0].ToLower().Equals("shop"))
        {
            if (parts[1].ToLower() == "reset")
            {
                PopulateShop();
            }
        }
    }
#endif
}
