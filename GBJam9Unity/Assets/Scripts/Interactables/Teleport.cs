using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour, IInteractable
{
    [SerializeField]
    Transform TeleportationPoint;
    [SerializeField]
    Transform Teleportee;

    [SerializeField]
    eTransitionEnums Transition;
    [SerializeField]
    eTransitionEnums TransitionIn;
    [SerializeField]
    float FadeTime = 1;

    public void RunInteraction()
    {
        GameManager.Instance.QueueTransition(Transition, FadeTime);
        GameManager.Instance.TransController.RunTransition(false, method: AfterFade);
    }

    public void AfterFade()
    {
        RenderTransition.FinishedTransitionDel -= AfterFade;
        Teleportee.position = TeleportationPoint.position;
        GameManager.Instance.QueueTransition(TransitionIn, FadeTime);
        GameManager.Instance.TransController.RunTransition(true);
    }
}
