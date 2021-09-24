using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItemManager : MonoBehaviour
{
    protected int _currentIndex = 0;
    [SerializeField]
    protected bool MenuLoops = true;
    [SerializeField]
    protected List<MenuItemBase> MenuItems = new List<MenuItemBase>();
    [SerializeField]
    protected RectTransform Arrow;
    [SerializeField]
    protected AudioSource AudioSource;
    protected Vector2 ArrowValues;

    // Start is called before the first frame update
    protected void Start()
    {
        ArrowValues = Arrow.anchoredPosition;
        if (MenuItems.Count > 0)
        {
            JustHighlight(0);
        }
    }

    protected void JustHighlight(int index)
    {
        MenuItems[index].IsHighlighted = true;
        Arrow.SetParent(MenuItems[index].transform);
        Arrow.localPosition = Vector2.zero;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!GameManager.Instance.IsActiveInputTarget(GetInstanceID()))
        {
            return;
        }
        if(MenuItems.Count == 0)
        {
            return;
        }
        int change = GetChangeFromInput();
        ChangeSelectedMenuItem(change);
        CheckForAButton();
    }

    protected virtual void OnEnable()
    {
        GameManager.Instance.AddInputTarget(GetInstanceID());
    }

    private void OnDisable()
    {
        GameManager.Instance.RemoveInputTarget(GetInstanceID());
    }

    protected virtual void CleanUp()
    {
        _currentIndex = 0;
        Arrow.SetParent(transform);
        Arrow.localPosition = ArrowValues;
    }

    protected virtual int GetChangeFromInput()
    {
        float vert = Input.GetAxisRaw("Vertical") * (Input.GetButtonDown("Vertical") ? 1.0f : 0.0f);
        int change = 0;
        if (vert > 0)
        {
            change = -1;
        }
        else if (vert < 0)
        {
            change = 1;
        }
        return change;
    }

    protected virtual void ChangeSelectedMenuItem(int change)
    {
        if (change == 0)
        {
            return;
        }
        MenuItems[CurrentIndex].IsHighlighted = false;
        CurrentIndex += change;
        JustHighlight(CurrentIndex);
    }

    protected virtual void CheckForAButton()
    {
        if (Input.GetButtonDown("AButton"))
        {
            AudioSource.Play();
            MenuItems[CurrentIndex].PerformAction();
        }
    }

    protected int CurrentIndex
    {
        get
        {
            return _currentIndex;
        }
        set
        {
            if (MenuLoops)
            {
                if (value < 0)
                {
                    value = MenuItems.Count - 1;
                }
                _currentIndex = value % MenuItems.Count;
            }
            else
            {
                _currentIndex = Mathf.Clamp(value, 0, MenuItems.Count - 1);
            }
        }
    }
}
