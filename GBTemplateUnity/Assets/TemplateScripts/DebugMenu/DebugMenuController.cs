#if DEVELOPMENT_BUILD || UNITY_EDITOR
using UnityEngine;

public class DebugMenuController : MonoBehaviour
{
    public static DebugMenuController Instance;

    [SerializeField]
    DebugConsole Console;
    
    public float GameSpeed = 1;

    [HideInInspector]
    public float LastGameSpeed = 1;

    public void SetGameSpeed(float speed)
    {
        GameSpeed = speed;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Console.gameObject.SetActive(false);
    }

    void ToggleConsole()
    {
        bool active = Console.gameObject.activeSelf;
        Console.gameObject.SetActive(!active);
        float tempSpeed = GameSpeed;
        GameSpeed = active ? LastGameSpeed : 0;
        LastGameSpeed = tempSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            ToggleConsole();
        }

        if (Time.timeScale != GameSpeed)
        {
            Time.timeScale = GameSpeed;
        }
    }

}
#endif