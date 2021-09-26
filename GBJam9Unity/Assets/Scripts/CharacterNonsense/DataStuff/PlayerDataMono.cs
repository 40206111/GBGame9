using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataMono : MonoBehaviour
{
    [System.Serializable]
    private struct AnimatorFromChicken
    {
        public RuntimeAnimatorController Controller;
        public eChickenClass ChickenClass;
    }
    [SerializeField]
    Animator Animator;
    [SerializeField]
    List<AnimatorFromChicken> Controllers = new List<AnimatorFromChicken>();

    private eChickenClass _activeClass;
    public eChickenClass ActiveClass { get { return _activeClass; } }

    public void ChangeActiveChicken(eChickenClass chickClass)
    {
        Animator.runtimeAnimatorController = Controllers.Find(x => x.ChickenClass == chickClass).Controller;
        // ~~~ surely there is more
        _activeClass = chickClass;
    }
}
