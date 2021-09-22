#if DEVELOPMENT_BUILD || UNITY_EDITOR
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenFromBoot : MonoBehaviour
{
    void Awake()
    {
        if (GameManager.Instance == null && !SceneManager.GetSceneByName("Boot").isLoaded)
        {
            SceneManager.LoadScene("Boot");
        }
    }
}
#endif