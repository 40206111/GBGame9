using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventRelay : MonoBehaviour
{
    PlayerActions Actions;
    private void Start()
    {
        Actions = GetComponentInParent<PlayerActions>();
    }
    public void AttackEnded()
    {
        Actions.AttackAnimationEnded();
    }
}
