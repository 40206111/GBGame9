using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField]
    Text PriceText;
    ItemDetails Details;

    SpriteRenderer SRenderer;

    private void Awake()
    {
        SRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetUp(ItemDetails details)
    {
        Details = details;
        SRenderer.sprite = Details.ItemImage;
        PriceText.text = Details.Price.ToString();
    }

    public void Selected()
    {
        DialogueBoxControl.Instance.PrintText(Details.FlavourText, closeAfterText: false);
    }

}
