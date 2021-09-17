#if DEVELOPMENT_BUILD || UNITY_EDITOR
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DebugConsole : MonoBehaviour
{
    [SerializeField]
    InputField ConsoleInput;
    EventSystem myEventSystem;

    private void Awake()
    {
        myEventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    private void OnEnable()
    {
        StartCoroutine(SelectConsole());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log($"reading {ConsoleInput.text} from console");
            ParseInput(ConsoleInput.text);
            ConsoleInput.text = "";
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StartCoroutine(SelectConsole(toggle: true));
        }
    }

    IEnumerator SelectConsole(bool toggle = false)
    {
        bool dontSelect = !(toggle && myEventSystem.currentSelectedGameObject == ConsoleInput.gameObject);
        myEventSystem.SetSelectedGameObject(null);
        yield return null;
        if (dontSelect)
        {
            ConsoleInput.Select();
        }
    }

    void ParseInput(string input)
    {
        var parts = input.Split(' ');

        string keyword = parts[0].ToLower();

        switch (keyword)
        {
            case "gamespeed":
                if (parts.Length == 2)
                {
                    Debug.Log($"setting game speed to {parts[1]}");
                    SetGameSpeed(parts[1]);
                }
                else
                {
                    Debug.Log($"please enter game speed and nothing else");
                }
                break;
            case "run":
                Debug.Log($"setting game timescale to {DebugMenuController.Instance.LastGameSpeed}");
                DebugMenuController.Instance.SetGameSpeed(DebugMenuController.Instance.LastGameSpeed);
                break;
            case "stop":
                Debug.Log($"pausing game");
                DebugMenuController.Instance.SetGameSpeed(0);
                break;
        }

        StartCoroutine(SelectConsole());
    }

    void SetGameSpeed(string input)
    {
        DebugMenuController.Instance.LastGameSpeed = float.Parse(input);
        if (DebugMenuController.Instance.GameSpeed != 0)
        {
            DebugMenuController.Instance.SetGameSpeed(DebugMenuController.Instance.LastGameSpeed);
        }
    }
}
#endif