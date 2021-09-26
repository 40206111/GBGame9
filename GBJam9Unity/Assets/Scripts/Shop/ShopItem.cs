using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MenuItemBase
{
    public Transform ArrowHolder;

    [SerializeField]
    Text PriceText;
    ItemDetails Details;

    SpriteRenderer SRenderer;

    protected override void Awake()
    {
        SRenderer = GetComponent<SpriteRenderer>();
    }

    public override void PerformAction()
    {
        Debug.Log($"Purchasing {Details.name} for {Details.Price} Sunflowers");
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
            if (Details != null)
            {
                DialogueBoxControl.Instance.PrintText(Details.FlavourText, closeAfterText: false);
            }
        }
    }

}
