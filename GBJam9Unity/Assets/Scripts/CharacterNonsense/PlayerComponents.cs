using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponents : MonoBehaviour
{
    [SerializeField]
    EntityMover _Mover;
    [SerializeField]
    Animator _Animator;
    [SerializeField]
    PlayerAttack _Attacker;
    [SerializeField]
    PlayerActions _Actions;

    public EntityMover Mover { get { return _Mover; } }
    public Animator Animator { get { return _Animator; } }
    public PlayerAttack Attacker { get { return _Attacker; } }
    public PlayerActions Actions { get { return _Actions; } }

    private void Reset()
    {
        _Mover = GetComponent<EntityMover>();
        _Animator = GetComponentInChildren<Animator>();
        _Attacker = GetComponent<PlayerAttack>();
        _Actions = GetComponent<PlayerActions>();
    }
}
