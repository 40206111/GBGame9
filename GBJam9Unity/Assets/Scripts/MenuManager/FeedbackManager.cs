using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackManager : MenuItemManager
{
    protected Action<eLittleFeedback> CurrentCallback = null;
    public bool HideOnExit = true;

    bool FirstFrame = true;

    protected override void Start()
    {
        base.Start();
        HideHighlight();
    }

    protected override void Update()
    {
        if (!GameManager.Instance.IsActiveInputTarget(gameObject.GetInstanceID()))
        {
            return;
        }
        if (FirstFrame)
        {
            FirstFrame = false;
            return;
        }
        base.Update();
        if (Input.GetButtonDown("AButton"))
        {
            ExitLittleMenu(((FeedbackMI)MenuItems[CurrentIndex]).Feedback);
        }
        else if (Input.GetButtonDown("BButton"))
        {
            ExitLittleMenu();
        }
    }

    public void ActivateLittleMenu(Action<eLittleFeedback> callback)
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
        CurrentIndex = 0;
        CurrentCallback = callback;
        GameManager.Instance.AddInputTarget(gameObject.GetInstanceID());
        JustHighlight(CurrentIndex);
        FirstFrame = true;
    }

    public void ExitLittleMenu(eLittleFeedback feedback = eLittleFeedback.none)
    {
        CurrentCallback?.Invoke(feedback);
        HideHighlight();
        GameManager.Instance.RemoveInputTarget(gameObject.GetInstanceID());
        if (HideOnExit)
        {
            gameObject.SetActive(false);
        }
    }

    protected void HideHighlight()
    {
        MenuItems[CurrentIndex].IsHighlighted = false;
        Arrow.SetParent(transform);
        Arrow.anchoredPosition = new Vector2(-10, 0);
    }

    protected override void OnEnable() { }
    protected override void OnDisable() { }
}
