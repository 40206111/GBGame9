using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityMover))]
public class PlayerMoveInput : MonoBehaviour
{
    EntityMover Mover;

    private void Awake()
    {
        Mover = GetComponent<EntityMover>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 travelDir = Vector2.zero;
        travelDir += new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(travelDir.sqrMagnitude > 1.0f)
        {
            travelDir = travelDir.normalized;
        }
        Mover.SetTravelDirection(travelDir);
    }
}
