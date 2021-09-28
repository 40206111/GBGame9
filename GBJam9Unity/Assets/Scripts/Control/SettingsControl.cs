using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsControl : MonoBehaviour
{
    private static SettingsControl _instance;
    public static SettingsControl Instance { get { return _instance; } }
    private void Awake()
    {
        _instance = this;
        Holder.SetActive(false);
    }

    public static Action<bool> PlayMusicSetEvent;


    public static bool PlayMusic = true;

    [SerializeField]
    private Sprite Tick;
    [SerializeField]
    private Sprite Cross;

    [SerializeField]
    private Image TickBox;

    [SerializeField]
    private GameObject Holder;

    

    private void Start()
    {
        UpdateDisplay();
    }

    public void ShowSettings()
    {
        Holder.SetActive(true);
        GameManager.Instance.AddInputTarget(gameObject.GetInstanceID());
    }
    public void HideSettings()
    {
        Holder.SetActive(false);
        GameManager.Instance.RemoveInputTarget(gameObject.GetInstanceID());
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsActiveInputTarget(gameObject.GetInstanceID()))
        {
            return;
        }
        if (Input.GetButtonDown("AButton"))
        {
            PlayMusic = !PlayMusic;
            PlayMusicSetEvent?.Invoke(PlayMusic);
            UpdateDisplay();
        }
        else if (Input.GetButtonDown("BButton"))
        {
            HideSettings();
        }
    }

    private void UpdateDisplay()
    {
        TickBox.sprite = PlayMusic ? Tick : Cross;
    }
}
