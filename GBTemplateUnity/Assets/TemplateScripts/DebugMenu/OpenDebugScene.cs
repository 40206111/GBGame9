#if DEVELOPMENT_BUILD || UNITY_EDITOR
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDebugScene : MonoBehaviour
{
    void Awake()
    {
        SceneManager.LoadSceneAsync("DebugScene", LoadSceneMode.Additive);
    }
}
#endif