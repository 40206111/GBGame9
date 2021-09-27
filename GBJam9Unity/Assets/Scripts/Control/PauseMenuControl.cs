using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuControl : MonoBehaviour
{
    private static PauseMenuControl _instance;
    public static PauseMenuControl Instance { get { return _instance; } }
    private void Awake()
    {
        _instance = this;
    }

    [SerializeField]
    private GameObject MenuContainer;

    private void Start()
    {
        MenuContainer.SetActive(false);
    }

    public void OpenPauseMenu()
    {
        MenuContainer.SetActive(true);
        GameManager.Instance.AddInputTarget(gameObject.GetInstanceID());
    }

    public void ClosePauseMenu()
    {
        MenuContainer.SetActive(false);
        GameManager.Instance.RemoveInputTarget(gameObject.GetInstanceID());
    }

    private void Update()
    {
        if (GameManager.Instance.NoInputTargets)
        {
            if (Input.GetButtonDown("Start"))
            {
                OpenPauseMenu();
            }
        }
        else if (MenuContainer.activeInHierarchy && GameManager.Instance.IsActiveInputTarget(gameObject.GetInstanceID()))
        {
            if (Input.GetButtonDown("BButton") || Input.GetButtonDown("Start"))
            {
                ClosePauseMenu();
            }
        }
    }
}
