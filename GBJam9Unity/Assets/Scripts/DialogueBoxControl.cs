using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBoxControl : MonoBehaviour
{
    private static DialogueBoxControl _Instance;
    public static DialogueBoxControl Instance { get { return _Instance; } }
    private void Awake()
    {
        if (_Instance != null && _Instance.gameObject != null)
        {
            Destroy(_Instance.gameObject);
        }
        _Instance = this;

        Filler = GetComponentInChildren<TextBoxFiller>();
        Filler.gameObject.SetActive(false);
        IsShowing = Filler.gameObject.activeInHierarchy;
    }

    public bool RequiresInput = true;
    TextBoxFiller Filler;
    bool IsShowing = false;


    public void ResetSpeed()
    {
        Filler.ResetSpeed();
    }

    public void SetTimePerChar(float time)
    {
        Filler.SetTimePerChar(time);
    }

    public void Display(bool active)
    {
        Filler.ClearText();
        Filler.gameObject.SetActive(active);
        IsShowing = Filler.gameObject.activeInHierarchy;
    }

    public void PrintText(string text, float timePerChar = -1.0f, bool closeAfterText = false)
    {
        StartCoroutine(PrintTextAndWait(text,timePerChar, closeAfterText));
    }

    public IEnumerator PrintTextAndWait(string text, float timePerChar = -1.0f, bool closeAfterText = false)
    {
        if (!IsShowing)
        {
            Display(true);
        }
        GameManager.Instance.AddInputTarget(Filler.GetInstanceID());
        if(timePerChar!= -1.0f)
        {
            SetTimePerChar(timePerChar);
        }
        yield return Filler.PrintTextAndWait(text);
        if (closeAfterText)
        {
            yield return StartCoroutine(Filler.WaitForUser());
            Display(false);
        }
        if (timePerChar != -1.0f)
        {
            ResetSpeed();
        }
        GameManager.Instance.RemoveInputTarget(Filler.GetInstanceID());
    }
}
