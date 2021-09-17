using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickTest : MonoBehaviour
{
    [SerializeField]
    Material Regular;
    [SerializeField]
    Material Highlighted;

    Text Text;
    TextBoxFiller TBF;
    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<Text>();
        TBF = GetComponent<TextBoxFiller>();
    }

    // Update is called once per frame
    void Update()
    {
        /*int val = (int)(Time.time % 6.0f);
        Text.material = (val >= 3 ? Highlighted : Regular);*/
        if (Input.GetKeyDown(KeyCode.N))
        {
            StartCoroutine(TextTestCoroutine());
        }
    }

    IEnumerator TextTestCoroutine()
    {
        yield return TBF.PrintTextAndWait("Here is a sizable piece of text so that the progress through the dialogue may better be tracked. Aaekghaeiugwefilaaeiugaeuwhfsfaeaiefua. It's unlikely that much more than this will be needed, but we'll add a few more words just in case.");
    }
}
