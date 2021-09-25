using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSelectControl : MonoBehaviour
{
    private static ChickenSelectControl _instance;
    public static ChickenSelectControl Instance { get { return _instance; } }
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

}
