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
    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int val = (int)(Time.time % 6.0f);
        Text.material = (val >= 3 ? Highlighted : Regular);
    }
}
