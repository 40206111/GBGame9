using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryControl : MonoBehaviour
{
    private static InventoryControl _instance;
    public static InventoryControl Instance { get { return _instance; } }
    private void Awake()
    {
        _instance = this;
        Holder.SetActive(false);
    }

    [SerializeField]
    InventoryMenuManager Manager;
    [SerializeField]
    GameObject Holder;


    private void Update()
    {
        if (!GameManager.Instance.IsActiveInputTarget(gameObject.GetInstanceID()))
        {
            return;
        }
        if (Input.GetButtonDown("BButton"))
        {
            HideInventory();
        }
    }

    public void ShowInventory()
    {
        GameManager.Instance.AddInputTarget(gameObject.GetInstanceID());
        Manager.PopulateMenu(PlayerData.Inventory.GetPocketItems());
        Holder.SetActive(true);
    }

    public void HideInventory()
    {
        GameManager.Instance.RemoveInputTarget(gameObject.GetInstanceID());
        Holder.SetActive(false);
        DialogueBoxControl.Instance.Display(false);
    }
}
