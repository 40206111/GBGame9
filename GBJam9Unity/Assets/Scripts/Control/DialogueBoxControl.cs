using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eSpeakingCharacter
{
    None,
    Melee,
    Range,
    Mage
}

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

    [SerializeField]
    Image LeftPortrait;
    [SerializeField]
    Image RightPortrait;

    [SerializeField]
    Sprite EmptyPortrait;
    [SerializeField]
    List<Sprite> Portraits = new List<Sprite>();

    public bool RequiresInput = true;
    TextBoxFiller Filler;
    bool IsShowing = false;

    Queue<DialogueDetails> TheQueue = new Queue<DialogueDetails>();

    public int QueueCount => TheQueue.Count;

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
        if (CameraFollow.ActiveCamera != null)
        {
            GetComponent<Canvas>().worldCamera = CameraFollow.ActiveCamera;
        }
    }

    public void QueueDialogue(string text, eSpeakingCharacter character, bool leftSide, float timePerChar = -1.0f, bool closeAfterText = false)
    {
        var dialogue = new DialogueDetails(text, character, leftSide, timePerChar, closeAfterText);
        TheQueue.Enqueue(dialogue);

        if (!IsShowing)
        {
            var next = TheQueue.Dequeue();
            PrintTextWithCharacter(next.Text, next.Portrait, next.LeftSide, next.TimePerChar, next.CloseAferText);
        }
    }


    public void PrintTextWithCharacter(string text, eSpeakingCharacter character, bool leftSide, float timePerChar = -1.0f, bool closeAfterText = false)
    {
        if (character == eSpeakingCharacter.None)
        {
            LeftPortrait.sprite = EmptyPortrait;
            RightPortrait.sprite = EmptyPortrait;
        }
        else
        {
            LeftPortrait.sprite = leftSide ? Portraits[(int)character - 1] : EmptyPortrait;
            RightPortrait.sprite = !leftSide ? Portraits[(int)character - 1] : EmptyPortrait;
        }
        StartCoroutine(PrintTextAndWait(text, timePerChar, closeAfterText));
    }

    public void PrintText(string text, float timePerChar = -1.0f, bool closeAfterText = false, bool waitInputStayOpen = false)
    {
        StartCoroutine(PrintTextAndWait(text, timePerChar, closeAfterText, waitInputStayOpen));
    }

    public IEnumerator PrintTextAndWait(string text, float timePerChar = -1.0f, bool closeAfterText = false, bool waitInputStayOpen = false)
    {
        if (!IsShowing)
        {
            Display(true);
        }
        if (!GameManager.Instance.IsActiveInputTarget(Filler.GetInstanceID()))
        {
            //Debug.LogWarning("Too many dialogue calls!");
            //yield break;
            GameManager.Instance.AddInputTarget(Filler.GetInstanceID());
        }
        if (timePerChar != -1.0f)
        {
            SetTimePerChar(timePerChar);
        }
        yield return StartCoroutine(Filler.PrintTextAndWait(text));
        if (closeAfterText)
        {
            yield return StartCoroutine(Filler.WaitForUser());
            Display(false);
        }
        else if (waitInputStayOpen)
        {
            yield return StartCoroutine(Filler.WaitForUser());
        }
        if (timePerChar != -1.0f)
        {
            ResetSpeed();
        }

        if (TheQueue.Count != 0)
        {
            var next = TheQueue.Dequeue();
            PrintTextWithCharacter(next.Text, next.Portrait, next.LeftSide, next.TimePerChar, next.CloseAferText);
        }
        else
        {
            LeftPortrait.sprite = EmptyPortrait;
            RightPortrait.sprite = EmptyPortrait;
            GameManager.Instance.RemoveInputTarget(Filler.GetInstanceID());
        }

    }
}
