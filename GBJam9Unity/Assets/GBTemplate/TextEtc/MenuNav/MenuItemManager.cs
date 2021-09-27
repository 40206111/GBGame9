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
    [SerializeField]
    protected GameObject InputKey = null;
    [SerializeField]
    protected string InputAxis = "Vertical";

    // Start is called before the first frame update
    protected virtual void Start()
    {
        ArrowValues = Arrow.anchoredPosition;
        if (MenuItems.Count > 0)
        {
            JustHighlight(0);
        }
    }

    protected virtual void JustHighlight(int index)
    {
        if (index < 0 || index >= MenuItems.Count)
        {
            return;
        }
        MenuItems[index].IsHighlighted = true;
        Arrow.SetParent(MenuItems[index].transform);
        Arrow.localPosition = Vector2.zero;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!IsInputTarget())
        {
            return;
        }
        if (MenuItems.Count == 0)
        {
            return;
        }
        int change = GetChangeFromInput();
        ChangeSelectedMenuItem(change);
        CheckForAButton();
    }

    protected virtual bool IsInputTarget()
    {
        int id;
        if (InputKey != null)
        {
            id = InputKey.GetInstanceID();
        }
        else
        {
            id = gameObject.GetInstanceID();
        }

        return GameManager.Instance.IsActiveInputTarget(id);
    }

    protected virtual void OnEnable()
    {
        GameManager.Instance.AddInputTarget(gameObject.GetInstanceID());
        JustHighlight(CurrentIndex);
    }

    protected virtual void OnDisable()
    {
        GameManager.Instance.RemoveInputTarget(gameObject.GetInstanceID());
    }

    public virtual void CleanUp()
    {
        _currentIndex = 0;
        Arrow.SetParent(transform);
        Arrow.localPosition = ArrowValues;
    }

    protected virtual int GetChangeFromInput()
    {
        float vert = Input.GetAxisRaw(InputAxis) * (Input.GetButtonDown(InputAxis) ? 1.0f : 0.0f);
        int change = 0;
        if (vert > 0)
        {
            change = -1;
        }
        else if (vert < 0)
        {
            change = 1;
        }

        if (InputAxis == "Horizontal")
        {
            change *= -1;
        }
        return change;
    }

    protected virtual void ChangeSelectedMenuItem(int change, bool force = false)
    {
        if (!force && change == 0)
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
            UseActionOutcome(MenuItems[CurrentIndex].PerformAction());
        }
    }

    protected virtual void UseActionOutcome(bool success) { }

    public virtual void FeedbackResponse(eLittleFeedback feedback) { }

    public int MenuItemCount
    {
        get { return MenuItems.Count; }
    }

    protected int CurrentIndex
    {
        get
        {
            return _currentIndex;
        }
        set
        {
            if (MenuItems.Count == 0)
            {
                _currentIndex = -1;
                return;
            }
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
