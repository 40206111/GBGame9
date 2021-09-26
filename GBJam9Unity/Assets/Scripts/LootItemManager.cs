using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItemManager : PopulatingMenuManager
{
    protected static LootItemManager _instance;
    public static LootItemManager Instance { get { return _instance; } }
    private void Awake()
    {
        _instance = this;
        gameObject.SetActive(false);
    }

    protected override void Update()
    {
        if (!GameManager.Instance.IsActiveInputTarget(gameObject.GetInstanceID()))
        {
            return;
        }
        base.Update();
        if (Input.GetButtonDown("BButton"))
        {
            Display(null);
        }
    }

    public void Display(Lootable lootable = null)
    {
        CleanUp();
        gameObject.SetActive(lootable != null);
        if (lootable != null)
        {
            GameManager.Instance.AddInputTarget(gameObject.GetInstanceID());
            PopulateMenu(lootable.GetItems());
        }
        else
        {
            GameManager.Instance.RemoveInputTarget(gameObject.GetInstanceID());
        }
    }
}
