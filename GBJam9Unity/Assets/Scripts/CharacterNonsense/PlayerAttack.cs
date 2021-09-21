using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerComponents Pcs;
    bool Attacking = false;

    private void Awake()
    {
        Pcs = GetComponent<PlayerComponents>();
    }

    public void DoAAttack()
    {
        if (DoAttack())
        {
            Pcs.Animator.SetTrigger("AAttack");
        }

    }

    public void DoBAttack()
    {
        if (DoAttack())
        {
            Pcs.Animator.SetTrigger("BAttack");
        }
    }

    private bool DoAttack()
    {
        if (Attacking)
        {
            return false;
        }

        Attacking = true;
        if (Pcs.Mover != null)
        {
            Pcs.Mover.AddMovementLock();
        }

        return true;
    }

    public void AttackAnimationEnded()
    {
        if (Pcs.Mover != null)
        {
            Pcs.Mover.RemoveMovementLock();
        }
        Attacking = false;
    }
}
