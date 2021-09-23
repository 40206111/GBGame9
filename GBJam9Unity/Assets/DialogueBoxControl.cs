using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBoxControl : MonoBehaviour
{
    private static DialogueBoxControl _Instance;
    public static DialogueBoxControl Instance { get { return _Instance; } }
    private void Awake()
    {
        if(_Instance != null && _Instance.gameObject != null)
        {
            Destroy(_Instance.gameObject);
        }
        _Instance = this;
    }

    public bool RequiresInput = true;
    TextBoxFiller Filler;
    bool IsShowing = false;

    // Start is called before the first frame update
    void Start()
    {
        Filler = GetComponentInChildren<TextBoxFiller>();
        Filler.gameObject.SetActive(false);
        IsShowing = Filler.gameObject.activeInHierarchy;
    }


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

    public void PrintText(string text, bool closeAfterText = false)
    {
        StartCoroutine(PrintTextAndWait(text, closeAfterText));
    }

    public IEnumerator PrintTextAndWait(string text, bool closeAfterText = false)
    {
        if (!IsShowing)
        {
            Display(true);
        }
        GameManager.Instance.AddInputTarget(Filler.GetInstanceID());
        yield return Filler.PrintTextAndWait(text);
        GameManager.Instance.RemoveInputTarget(Filler.GetInstanceID());
        if (closeAfterText)
        {
            Display(false);
        }
    }
}
