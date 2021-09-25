#if DEVELOPMENT_BUILD || UNITY_EDITOR
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenFromBoot : MonoBehaviour
{
    [SerializeField]
    string SceneToLoadAfter;

    void Awake()
    {
        if (GameManager.Instance == null && !SceneManager.GetSceneByName("Boot").isLoaded)
        {
            if (SceneToLoadAfter != "")
            {
                Cheats.ForceLoadSceneFromBoot = true;
                Cheats.SceneToLoadFromBoot = SceneToLoadAfter;
            }
            SceneManager.LoadScene("Boot");
        }
    }
}
#endif