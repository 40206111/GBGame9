using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MenuItemBase
{
    public Transform ArrowHolder;

    [SerializeField]
    Text PriceText;
    public ItemDetails Details;

    SpriteRenderer SRenderer;

    protected override void Awake()
    {
        SRenderer = GetComponent<SpriteRenderer>();
    }

    public override bool PerformAction()
    {
        Debug.Log($"Purchasing {Details.name} for {Details.Price} Sunflowers");

        if (PlayerData.Seeds >= Details.Price)
        {
            PlayerData.Inventory.AddItem(Details);
            PlayerData.Seeds -= Details.Price;
            return true;
        }
        else
        {
            return false;
        }

    }

    public void SetUp(ItemDetails details)
    {
        Details = details;
        SRenderer.sprite = Details.ItemImage;
        PriceText.text = Details.Price.ToString();
    }

    public override bool IsHighlighted { 
        get => base.IsHighlighted;
        set
        {
            isHighlighted = value;
            if (isHighlighted && Details != null)
            {
                DialogueBoxControl.Instance.PrintText(Details.FlavourText, closeAfterText: false, timePerChar: 0.005f);
            }
        }
    }

}
