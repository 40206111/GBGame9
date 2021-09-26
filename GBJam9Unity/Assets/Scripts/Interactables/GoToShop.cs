using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToShop : MonoBehaviour, IInteractable
{
    public void RunInteraction()
    {
        if (GameManager.Instance.Shop != null)
        {
            GameManager.Instance.Shop.GoToShop();
        }
    }
}
