using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameManager Instance;

    string ActiveScene;
    int ActiveSceneIndex;
    float PowerOffTime = 0;
    [SerializeField]
    float PowerOffWaitTime = 1.0f;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new System.Exception("GameManager Created when GameManager already exists");
        }
        DontDestroyOnLoad(gameObject);
        ActiveScene = "Boot";
        ActiveSceneIndex = SceneManager.GetSceneByName(ActiveScene).buildIndex;
    }

    private void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        int thisScene = ActiveSceneIndex;
        int nextScene = thisScene + 1;
        var loadingScene = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);

        while (!loadingScene.isDone)
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync(thisScene);
        ActiveScene = SceneManager.GetSceneByBuildIndex(nextScene).name;
        ActiveSceneIndex = nextScene;
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.Escape))
        {
            PowerOffTime += Time.deltaTime;
        }
        else
        {
            PowerOffTime = 0;
        }

        if (PowerOffTime >= PowerOffWaitTime)
        {
            PowerOff();
        }
    }


    void PowerOff()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL("https://elfqueen.itch.io");
#else
        Application.Quit();
#endif
    }
}
