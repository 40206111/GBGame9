using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTransition : MonoBehaviour
{
    Vector2[] ColourCoords = new Vector2[] { new Vector2(0.2f, 0.0f), new Vector2(0.4f, 0.0f), new Vector2(0.6f, 0.0f), new Vector2(0.8f, 0.0f) };
    string OutColourStringBase = "_OutCol";
    string TransProgString = "_TransProgress";

    [SerializeField]
    float FadeTime = 1.0f;
    [SerializeField]
    float ShaderFadeTime = 1.0f;

    [SerializeField]
    Material PaletteMat;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            StartCoroutine(FadeTransition());
        }
        if (Input.GetKeyDown(KeyCode.F10))
        {
            ResetColourCoords();
        }

        PaletteMat.SetFloat(TransProgString, Mathf.Abs(((Time.time * (1.0f / ShaderFadeTime)) % 4.0f) - 2.0f) - 1.0f); // Mathf.Sin(Time.time * (1.0f / ShaderFadeTime) * Mathf.PI * 0.5f));
    }

    void ResetColourCoords()
    {
        for (int i = 0; i < ColourCoords.Length; ++i)
        {
            SetOutColours(i + 1, ColourCoords[i]);
        }
    }

    IEnumerator FadeTransition()
    {
        float elapsed = 0.0f;
        int colours = 4;
        float divTime = FadeTime / colours;
        int currentColour = 0;

        while (elapsed < FadeTime || currentColour < ColourCoords.Length)
        {

            int colCheck = (int)(elapsed / divTime);
            if (colCheck > currentColour)
            {
                currentColour = colCheck;
                for (int i = 0; /*i <= currentColour &&*/ i < ColourCoords.Length; ++i)
                {
                    int fColour = Mathf.Clamp(i + currentColour, 0, 3);
                    SetOutColours(i + 1, ColourCoords[fColour]);
                }
            }


            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    void SetOutColours(int colIndex, Vector2 to)
    {
        PaletteMat.SetVector(OutColourStringBase + colIndex, to);
    }
}
