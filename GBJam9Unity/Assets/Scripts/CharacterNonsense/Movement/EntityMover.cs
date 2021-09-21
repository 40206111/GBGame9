using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMover : MonoBehaviour
{
    [SerializeField]
    Animator Animator;
    Rigidbody2D Rigidbody;
    Vector2 TravelDirection;
    Vector2Int FacingDirection = Vector2Int.down;
    float MaxSpeed = 2.0f;
    float Acceleration = 4.0f;
    float Deceleration = 4.0f;

    int MovementLocks = 0;

    readonly Dictionary<Vector2Int, int> FacingValues = new Dictionary<Vector2Int, int>()
        { { Vector2Int.up, 0 }, { Vector2Int.right, 1 }, { Vector2Int.down, 2 }, { Vector2Int.left, 3 } };


    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Acceleration = 5.0f * MaxSpeed;
        Deceleration = 2.0f * Acceleration;
    }

    public void AddMovementLock()
    {
        MovementLocks++;
    }

    public void RemoveMovementLock()
    {
        MovementLocks--;
        if (MovementLocks < 0)
        {
            MovementLocks = 0;
        }
    }

    public bool IsMovementLocked
    {
        get
        {
            return MovementLocks > 0;
        }
    }

    private Vector2Int CalculateSimpleFacing(Vector2 dir)
    {
        Vector2Int outVec = Vector2Int.zero;

        if (dir != Vector2.zero)
        {
            if (Mathf.Abs(dir.x) >= Mathf.Abs(dir.y))
            {
                if (dir.x > 0)
                {
                    outVec = Vector2Int.right;
                }
                else
                {
                    outVec = Vector2Int.left;
                }
            }
            else
            {
                if (dir.y > 0)
                {
                    outVec = Vector2Int.up;
                }
                else
                {
                    outVec = Vector2Int.down;
                }
            }
        }

        return outVec;
    }

    public void SetTravelDirection(Vector2 dir)
    {
        if (dir == TravelDirection)
        {
            return;
        }

        if (dir.sqrMagnitude > 1.0f)
        {
            dir = dir.normalized;
        }
        TravelDirection = dir;

        SetFacingDirection(CalculateSimpleFacing(TravelDirection));
    }

    private void SetFacingDirection(Vector2Int dir)
    {
        if(dir == Vector2Int.zero)
        {
            return;
        }
        if (!FacingValues.ContainsKey(dir))
        {
            dir = Vector2Int.down;
        }
        FacingDirection = dir;
        Animator.SetInteger("Facing", FacingValues[dir]);
        Animator.SetFloat("FacingX", dir.x);
        Animator.SetFloat("FacingY", dir.y);
    }

    private void Update()
    {
        Animator.SetBool("Moving", !IsMovementLocked && Rigidbody.velocity.sqrMagnitude > 0.01f);
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = MovementLocks > 0 ? Vector2.zero : TravelDirection * MaxSpeed;
        Vector2 currentVelocity = Rigidbody.velocity;
        if (targetVelocity != currentVelocity)
        {
            float timeDeceleration = Time.fixedDeltaTime * Deceleration;
            Vector2 newAccel = Vector2.zero;
            Vector2 perpTV = Vector2.Perpendicular(targetVelocity).normalized;
            Vector2 diffVel = targetVelocity - currentVelocity;

            Vector2 decelVec = Vector2.zero;
            if (Vector2.Dot(currentVelocity, diffVel) < 0)
            {
                decelVec = -currentVelocity;
            }
            else
            {
                float perpDecel = Vector2.Dot(perpTV, currentVelocity);
                float sign = Mathf.Sign(perpDecel);
                perpDecel = Mathf.Min(Mathf.Abs(perpDecel), timeDeceleration) * sign;
                decelVec = -1.0f * perpDecel * perpTV;
            }

            float spareAccel = 0.0f;
            if (decelVec.sqrMagnitude > timeDeceleration * timeDeceleration)
            {
                decelVec = decelVec.normalized * timeDeceleration;
            }
            else
            {
                spareAccel = 1.0f - (decelVec.magnitude / timeDeceleration);
            }

            newAccel = decelVec + Acceleration * spareAccel * Time.fixedDeltaTime * TravelDirection;

            currentVelocity += newAccel;
            if (currentVelocity.sqrMagnitude > MaxSpeed * MaxSpeed)
            {
                currentVelocity = MaxSpeed * currentVelocity.normalized;
            }

            Rigidbody.velocity = currentVelocity;
        }
    }
}
