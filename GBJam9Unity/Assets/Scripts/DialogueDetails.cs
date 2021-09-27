using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDetails
{
    public eSpeakingCharacter Portrait;
    public string Text;
    public bool LeftSide;
    public float TimePerChar;
    public bool CloseAferText;

    public DialogueDetails(string text, eSpeakingCharacter character, bool leftSide, float timePerChar = -1.0f, bool closeAfterText = false)
    {
        Text = text;
        Portrait = character;
        LeftSide = leftSide;
        TimePerChar = timePerChar;
        CloseAferText = closeAfterText;
    }

}
