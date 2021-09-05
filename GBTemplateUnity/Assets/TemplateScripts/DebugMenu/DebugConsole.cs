#if DEVELOPMENT_BUILD || UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class DebugConsole : MonoBehaviour
{
    [SerializeField]
    InputField ConsoleInput;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ParseInput(ConsoleInput.text);
            ConsoleInput.text = "";
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
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
    }

    void SetGameSpeed(string input)
    {
        DebugMenuController.Instance.LastGameSpeed = float.Parse(input);
    }
}
#endif