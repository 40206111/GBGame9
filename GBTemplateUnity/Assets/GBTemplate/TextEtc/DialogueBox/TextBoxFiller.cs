using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxFiller : MonoBehaviour
{
    [SerializeField]
    private Text Text;
    [SerializeField]
    private Animator IconAnimator;

    [SerializeField]
    private float DefaultTimePerChar = 0.05f;
    [HideInInspector]
    public float TimePerChar = 0.0f;

    private bool Filling = false;

    private void Awake()
    {
        ResetSpeed();
    }

    public void ResetSpeed()
    {
        TimePerChar = DefaultTimePerChar;
    }

    public void SetTimePerChar(float time)
    {
        TimePerChar = time;
    }

    public void PrintText(string text)
    {
        StartCoroutine(TextScroll(text));
    }

    public IEnumerator PrintTextAndWait(string text)
    {
        yield return StartCoroutine(TextScroll(text));
    }

    private IEnumerator TextScroll(string text)
    {
        // If already filling cancel and do this new text
        if (Filling)
        {
            Filling = false;
            yield return null;
        }

        // Filling commence
        Filling = true;

        CharacterInfo info;
        char[] chars = text.ToCharArray();
        int rowWidth = (int)Text.rectTransform.rect.width;
        List<int> rowStartIndexes = new List<int> { 0 };
        int lastWhitespace = -1;
        int currentWidth = 0;

        for (int i = 0; i < chars.Length; ++i)
        {
            char testChar = chars[i];
            Text.font.GetCharacterInfo(testChar, out info);

            /*
             * Get char width
             * if(' ')
             *      whitespace = i
             * if += char > text width 
             *      rowStartI = whitespace +1
             *      i = whitespace+1
             * else
             *      width += char
             */

            int charWidth = info.advance;
            if (testChar.Equals(' '))
            {
                lastWhitespace = i;
            }
            if (currentWidth + charWidth > rowWidth)
            {
                int newRowIndex = lastWhitespace + 1;
                if (newRowIndex == rowStartIndexes[rowStartIndexes.Count - 1])
                {
                    newRowIndex = i + 1;
                }
                if (newRowIndex < chars.Length)
                {
                    rowStartIndexes.Add(newRowIndex);
                    i = newRowIndex - 1; // minus one because for loop ++i
                }
                currentWidth = 0;
            }
            else
            {
                currentWidth += charWidth;
            }
        }

        // While string to print
        int textProgress = 0;
        string[] lines = new string[] { "", "", "" };
        int currentFillLine = 0;
        int nextRowIndex = 1;
        while (textProgress < chars.Length)
        {
            while (currentFillLine < lines.Length)
            {
                yield return new WaitForSeconds(TimePerChar);
                lines[currentFillLine] += chars[textProgress];
                textProgress++;
                if (textProgress < chars.Length)
                {
                    if (nextRowIndex < rowStartIndexes.Count && textProgress == rowStartIndexes[nextRowIndex])
                    {
                        lines[currentFillLine] += '\n';
                        lines[currentFillLine].TrimEnd(' ');
                        currentFillLine++;
                        nextRowIndex++;
                    }
                }

                string outString = "";
                for (int i = 0; i < lines.Length; ++i)
                {
                    outString += lines[i];
                }
                Text.text = outString;

                if (textProgress >= chars.Length)
                {
                    break;
                }
                if (currentFillLine >= lines.Length)
                {
                    break;
                }
            }

            yield return StartCoroutine(WaitForUser());

            if (textProgress < chars.Length)
            {
                for (int i = 0; i < lines.Length - 1; ++i)
                {
                    lines[i] = lines[i + 1];
                }
                lines[lines.Length - 1] = "";
                currentFillLine--;
            }
            else
            {
                for (int i = 0; i < lines.Length; ++i)
                {
                    lines[i] = "";
                }
            }
        }

        // Clean up
        Filling = false;
        yield return null;
    }

    public void ClearText()
    {
        Text.text = "";
    }

    public IEnumerator WaitForUser()
    {
        ActivateButtonPromptIcon();
        while (!ContinueInputPressed())
        {
            yield return null;
        }
        HidePromptIcon();
    }

    public void ActivateButtonPromptIcon()
    {
        IconAnimator.SetBool("Button", true);
    }
    public void HidePromptIcon()
    {
        IconAnimator.SetBool("Button", false);
    }

    private bool ContinueInputPressed()
    {
        return Input.GetButtonDown("AButton");
    }
}
