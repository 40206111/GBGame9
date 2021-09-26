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
    PlayerActions _Actions;
    [SerializeField]
    PlayerDataMono _Data;

    public EntityMover Mover { get { return _Mover; } }
    public Animator Animator { get { return _Animator; } }
    public PlayerActions Actions { get { return _Actions; } }
    public PlayerDataMono Data{ get { return _Data; } }

    private void Reset()
    {
        _Mover = GetComponent<EntityMover>();
        _Animator = GetComponentInChildren<Animator>();
        _Actions = GetComponent<PlayerActions>();
        _Data = GetComponent<PlayerDataMono>();
    }
}
