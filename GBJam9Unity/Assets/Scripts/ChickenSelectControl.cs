using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChickenSelectControl : MonoBehaviour
{
    private static ChickenSelectControl _instance;
    public static ChickenSelectControl Instance { get { return _instance; } }
    private void Awake()
    {
        _instance = this;
    }

    private int SelectedIndex = 0;
    [SerializeField]
    private RectTransform Arrow;
    [SerializeField]
    private GameObject MenuContainer;
    [SerializeField]
    private List<ChickSelectInfoFiller> Chickens = new List<ChickSelectInfoFiller>();

    private void Start()
    {
        MenuContainer.SetActive(false);
    }

    public void RunChickenSelect(Action<eChickenClass> feedback)
    {
        StartCoroutine(RunChickenSelectAndWait(feedback));
    }

    public IEnumerator RunChickenSelectAndWait(Action<eChickenClass> feedback)
    {
        GameManager.Instance.AddInputTarget(GetInstanceID());
        MenuContainer.SetActive(true);
        foreach(ChickSelectInfoFiller chicken in Chickens)
        {
            chicken.FillInfo();
        }

        int finalIndex = -1;
        while(finalIndex == -1)
        {
            if (!GameManager.Instance.IsActiveInputTarget(GetInstanceID()))
            {
                yield return null;
                continue;
            }
            SelectionUpdate();
            if (Input.GetButtonDown("AButton"))
            {
                finalIndex = SelectedIndex;
                break;
            }
            if (Input.GetButtonDown("BButton"))
            {
                break;
            }
            yield return null;
        }

        if (finalIndex == -1)
        {
            feedback.Invoke(eChickenClass.none);
        }
        else
        {
            feedback.Invoke(Chickens[finalIndex].ChickenClass);
        }

        GameManager.Instance.RemoveInputTarget(GetInstanceID());
        MenuContainer.SetActive(false);
    }

    private void SelectionUpdate()
    {
        float hor = Input.GetAxisRaw("Horizontal") * (Input.GetButtonDown("Horizontal") ? 1.0f : 0.0f);
        int change = 0;
        if (hor < 0)
        {
            change = -1;
        }
        else if (hor > 0)
        {
            change = 1;
        }

        int before = SelectedIndex;

        SelectedIndex = Mathf.Clamp(SelectedIndex + change, 0, Chickens.Count - 1);

        if(SelectedIndex == before)
        {
            return;
        }

        Vector2 arrowPos = Arrow.anchoredPosition;
        Arrow.SetParent(Chickens[SelectedIndex].transform);
        Arrow.anchoredPosition = arrowPos;
    }
}
