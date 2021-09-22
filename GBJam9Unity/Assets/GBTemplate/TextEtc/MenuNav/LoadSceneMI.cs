using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneMI : MenuItemBase
{
    [SerializeField]
    string SceneName;
    [SerializeField]
    eTransitionEnums Transition;
    [SerializeField]
    float FadeTime = 1;

    public override void PerformAction()
    {
        GameManager.Instance.LoadSceneByName(SceneName, Transition, FadeTime);
    }
}
