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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
}
