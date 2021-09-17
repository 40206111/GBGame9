#if DEVELOPMENT_BUILD || UNITY_EDITOR
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDebugScene : MonoBehaviour
{
    void Awake()
    {
        if (!SceneManager.GetSceneByName("DebugScene").isLoaded)
        {
            SceneManager.LoadSceneAsync("DebugScene", LoadSceneMode.Additive);
        }
    }
}
#endif