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

    [SerializeField]
    Camera ShopCamera;

    Camera OldCamera;

    [SerializeField]
    eTransitionEnums TransitionIn;
    [SerializeField]
    eTransitionEnums TransitionOut;
    [SerializeField]
    float FadeTime = 1;

    [SerializeField]
    Animator Merchant;

    bool ShopOpen = false;
    bool FirstFrame = true;

    protected override void Start()
    {
        base.Start();
        SpecialItem = MenuItems[0] as ShopItem;
        DefenseItem = MenuItems[1] as ShopItem;
        AttackItem = MenuItems[2] as ShopItem;
        GameManager.Instance.Shop = this;
        ShopCamera.enabled = false;
    }

    protected override void Update()
    {
        if (FirstFrame)
        {
            FirstFrame = false;
            return;
        }
        if (!IsInputTarget())
        {
            return;
        }
        base.Update();
        CheckForBButton();
    }

    protected virtual void CheckForBButton()
    {
        if (Input.GetButtonDown("BButton"))
        {
            LeaveShop();
        }
    }

    public void LeaveShop()
    {
        if (!ShopOpen) return;
        ShopOpen = false;
        GameManager.Instance.QueueTransition(TransitionOut, FadeTime);
        GameManager.Instance.TransController.RunTransition(false, method: RemoveShop);
    }

    void RemoveShop()
    {
        RenderTransition.FinishedTransitionDel -= RemoveShop;
        Merchant.SetBool("Go", false);
        GameManager.Instance.RemoveInputTarget(gameObject.GetInstanceID());
        OldCamera.enabled = true;
        ShopCamera.enabled = false;
        CameraFollow.ActiveCamera = OldCamera;
        DialogueBoxControl.Instance.Display(false);
        GameManager.Instance.QueueTransition(TransitionOut, FadeTime);
        GameManager.Instance.TransController.RunTransition(true);
    }

    public void GoToShop()
    {
        if (ShopOpen) return;
        ShopOpen = true;
        FirstFrame = true;
        GameManager.Instance.AddInputTarget(gameObject.GetInstanceID());
        GameManager.Instance.QueueTransition(TransitionOut, FadeTime);
        GameManager.Instance.TransController.RunTransition(false, method: LoadShop);
    }

    void LoadShop()
    {
        RenderTransition.FinishedTransitionDel -= LoadShop;
        OldCamera = CameraFollow.MainCamera;
        OldCamera.enabled = false;
        ShopCamera.enabled = true;
        CameraFollow.ActiveCamera = ShopCamera;
        PopulateShop();
        Merchant.SetBool("Go", true);
        GameManager.Instance.QueueTransition(TransitionIn, FadeTime);
        GameManager.Instance.TransController.RunTransition(true);
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
            StartCoroutine(WaitForShopKeeperToFinish($"Purchasing {menuItem.Details.name} for {menuItem.Details.Price} Sunflower seeds!"));
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


    protected override void OnEnable()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        DebugConsole.ConsoleEvent += OnDebugConsole;
#endif
    }

    protected override void OnDisable()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        DebugConsole.ConsoleEvent -= OnDebugConsole;
#endif
    }

#if DEVELOPMENT_BUILD || UNITY_EDITOR
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
