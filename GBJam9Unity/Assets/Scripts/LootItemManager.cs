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
        if (!IsInputTarget())
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
    }
}
