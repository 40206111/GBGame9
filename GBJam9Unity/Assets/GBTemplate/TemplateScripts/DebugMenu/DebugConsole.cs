#if DEVELOPMENT_BUILD || UNITY_EDITOR
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DebugConsole : MonoBehaviour
{
    [SerializeField]
    InputField ConsoleInput;
    EventSystem myEventSystem;

    public static event Action<string> ConsoleEvent;

    string LastCommand = "";

    private void Awake()
    {
        myEventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    private void OnEnable()
    {
        StartCoroutine(SelectConsole());
        ConsoleInput.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log($"reading {ConsoleInput.text} from console");
            LastCommand = ConsoleInput.text;
            ParseInput(ConsoleInput.text);
            SendEvent(ConsoleInput.text);
            ConsoleInput.text = "";
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StartCoroutine(SelectConsole(toggle: true));
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && myEventSystem.currentSelectedGameObject == ConsoleInput.gameObject)
        {
            ConsoleInput.text = LastCommand;
        }
    }

    void SendEvent(string message)
    {
        if (ConsoleEvent != null)
        {
            ConsoleEvent.Invoke(message);
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
            case "fade":
                if (parts.Length == 2)
                {
                    ParseFadeInstructions(parts[1], "1");
                }
                else if (parts.Length == 3)
                {
                    ParseFadeInstructions(parts[1], parts[2]);
                }
                else
                {
                    Debug.LogError("Fade GradEnumName/GradIndex GradTime(Optional)");
                }
                break;
        }

        StartCoroutine(SelectConsole());
    }

    void ParseFadeInstructions(string type, string time)
    {
        eTransitionEnums transition;

        if (!Enum.TryParse(type, out transition))
        {
            if (int.TryParse(type, out int enumInt))
            {
                transition = (eTransitionEnums)enumInt;
            }
            else
            {
                Debug.LogError($"Fade {type} does not exist");
            }
        }

        float.TryParse(time, out float transTime);

        Debug.Log($"running {transition.ToString()} for {transTime} seconds");
        GameManager.Instance.QueueTransition(transition, transTime);
        GameManager.Instance.TransController.RunTransition(true, asToggle: true);

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