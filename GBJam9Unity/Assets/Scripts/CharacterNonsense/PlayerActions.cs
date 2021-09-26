using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    PlayerComponents Pcs;
    bool Attacking = false;

    private void Awake()
    {
        Pcs = GetComponent<PlayerComponents>();
    }

    public void AButtonPushed()
    {
        Vector2Int facing = Pcs.Mover.GetFacingDirection;
        LayerMask mask = LayerMask.GetMask("Interactable");
        RaycastHit2D raycast = Physics2D.Raycast(((Vector2)transform.position) + new Vector2(0.0f, 0.25f), facing, 1.0f, mask);

        if (raycast.collider != null)
        {
            ExecuteAInteraction(raycast);
        }
        else
        {
            ExecuteAAttack();
        }

    }

    public void BButtonPushed()
    {
        ExecuteBAttack();
    }

    private void ExecuteAInteraction(RaycastHit2D rayHit)
    {
        Debug.Log(rayHit.collider.name);
        rayHit.collider.GetComponent<IInteractable>().RunInteraction();
    }

    private void ExecuteAAttack()
    {
        if (DoAttack())
        {
            Pcs.Animator.SetTrigger("AAttack");
        }
    }

    private void ExecuteBAttack()
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

    public void SelectPushed()
    {
        if (!Attacking)
        {
            ChickenSelectControl.Instance.RunChickenSelect(SwapActiveChicken);
        }
    }

    public void SwapActiveChicken(eChickenClass chickClass)
    {
        Debug.Log($"Swapping chickens is definitely coded. Go to chicken: {chickClass}.");
        if (chickClass == eChickenClass.none)
        {
            return;
        }
        Pcs.Data.ChangeActiveChicken(chickClass);
        Pcs.Mover.RefreshFacingForAnimator();
        PlayerData.ActiveChicken = chickClass;
    }
}
