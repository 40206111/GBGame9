using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadThingsForGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (GameManager.Instance.Shop == null)
        {
            SceneManager.LoadSceneAsync("Shop", LoadSceneMode.Additive);
        }
    }

}
