using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopControl : MenuItemManager
{
    ShopItem SpecialItem;
    ShopItem DefenseItem;
    ShopItem AttackItem;

    [SerializeField]
    Text ItemNameText;
    [SerializeField]
    GameObject ItemNameGameObject;

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
        StartCoroutine(WaitForShopKeeperToFinish(ShopText));
    }

    IEnumerator WaitForShopKeeperToFinish(string text)
    {
        ItemNameGameObject.gameObject.SetActive(false);
        yield return StartCoroutine(DialogueBoxControl.Instance.PrintTextAndWait(text, closeAfterText: true, timePerChar: 0.03f));

        var menuItem = (MenuItems[CurrentIndex] as ShopItem);
        ItemNameGameObject.gameObject.SetActive(true);
        menuItem.IsHighlighted = true;
        ItemNameText.text = menuItem.Details.name;
    }

    protected override void JustHighlight(int index)
    {
        var menuItem = (MenuItems[index] as ShopItem);
        menuItem.IsHighlighted = true;
        Arrow.SetParent(menuItem.ArrowHolder);
        if (menuItem.Details != null)
        {
            ItemNameText.text = menuItem.Details.name;
        }
        Arrow.localPosition = Vector2.zero;
    }

    protected override void UseActionOutcome(bool success)
    {
        if (success)
        {
            var menuItem = (MenuItems[CurrentIndex] as ShopItem);
            StartCoroutine(WaitForShopKeeperToFinish($"Purchasing {menuItem.Details.name} for {menuItem.Details.Price} Sunflowers..."));
            PopulateItem(CurrentIndex);
        }
        else
        {
            StartCoroutine(WaitForShopKeeperToFinish("That is not nearly enough seeds for this fine item!"));
        }
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

    private void PopulateItem(int itemIndex)
    {
        if (itemIndex == 0)
        {
            int random = Random.Range(0, SpecialItemPool.Items.Count);
            var newItem = SpecialItemPool.Items[random];

            while (newItem.name == SpecialItem.Details.name &&
                SpecialItemPool.Items.Count > 1)
            {
                random = Random.Range(0, SpecialItemPool.Items.Count);
                newItem = SpecialItemPool.Items[random];
            }

            SpecialItem.SetUp(newItem);
        }
        else if (itemIndex == 1)
        {
            int random = Random.Range(0, DefenseItemPool.Items.Count);
            var newItem = DefenseItemPool.Items[random];

            while (newItem.name == DefenseItem.Details.name &&
                DefenseItemPool.Items.Count > 1)
            {
                random = Random.Range(0, DefenseItemPool.Items.Count);
                newItem = DefenseItemPool.Items[random];
            }

            DefenseItem.SetUp(newItem);
        }
        else
        {
            int random = Random.Range(0, AttackItemPool.Items.Count);
            var newItem = AttackItemPool.Items[random];

            while (newItem.name == AttackItem.Details.name &&
                AttackItemPool.Items.Count > 1)
            {
                random = Random.Range(0, AttackItemPool.Items.Count);
                newItem = AttackItemPool.Items[random];
            }

            AttackItem.SetUp(newItem);
        }
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
