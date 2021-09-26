using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityMover), typeof(PlayerActions), typeof(PlayerComponents))]
public class PlayerInput : MonoBehaviour
{
    PlayerComponents Pcs;

    private void Awake()
    {
        Pcs = GetComponent<PlayerComponents>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveUpdate();

        if (GameManager.Instance.NoInputTargets)
        {
            AttackUpdate();

            SelectUpdate();
        }
    }

    void MoveUpdate()
    {
        Vector2 travelDir = Vector2.zero;
        if (GameManager.Instance.NoInputTargets)
        {
            travelDir += new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        if (travelDir.sqrMagnitude > 1.0f)
        {
            travelDir = travelDir.normalized;
        }
        Pcs.Mover.SetTravelDirection(travelDir);
    }

    void AttackUpdate()
    {
        if (Input.GetButtonDown("AButton"))
        {
            Pcs.Actions.AButtonPushed();
        }
        else if (Input.GetButtonDown("BButton"))
        {
            Pcs.Actions.BButtonPushed();
        }
    }

    void SelectUpdate()
    {
        if (Input.GetButtonDown("Select"))
        {
            Pcs.Actions.SelectPushed();
        }
    }
}
