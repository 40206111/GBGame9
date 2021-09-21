using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventRelay : MonoBehaviour
{
    PlayerAttack Attacker;
    private void Start()
    {
        Attacker = GetComponentInParent<PlayerAttack>();
    }
    public void AttackEnded()
    {
        Attacker.AttackAnimationEnded();
    }
}
