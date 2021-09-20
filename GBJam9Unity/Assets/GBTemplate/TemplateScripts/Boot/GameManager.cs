using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager Instance;
    
    string ActiveScene;
    int ActiveSceneIndex;
    float PowerOffTime = 0;
    [SerializeField]
    float PowerOffWaitTime = 1.0f;

    [SerializeField]
    bool PlaySplash;

    bool LoadSceneNow = false;

    public TransitionController TransController;

    public enum eGameState
    {
        Playing,
        Paused
    }

    public eGameState GameState { get; private set; }


    public void ChangeGameState(eGameState gameState)
    {
        GameState = gameState;
    }

    public void QueueTransition(eTransitionEnums transitionEnum, float fadeTime = 1)
    {
        if (TransController != null)
        {
            TransController.AddTransition(transitionEnum, fadeTime);
        }
        else
        {
            Debug.LogWarning("Transition attempted to be added, but no transition controller to add it to");
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new System.Exception("GameManager Created when GameManager already exists");
        }
        DontDestroyOnLoad(gameObject);

        var renderTrans = GetComponent<RenderTransition>();
        if (renderTrans != null)
        {
            TransController = new TransitionController(renderTrans);
        }

        ActiveScene = "Boot";
        ActiveSceneIndex = SceneManager.GetSceneByName(ActiveScene).buildIndex;

        GameState = eGameState.Playing;
    }

    private void Start()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        if (!PlaySplash)
        {
            LoadSceneByName("StartScene", eTransitionEnums.None);
        }
        else
        {
#endif
            LoadNextScene(eTransitionEnums.None);
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        }
#endif
    }

    public void LoadNextScene(eTransitionEnums transition, float fadeTime = 1.0f)
    {
        if (SceneManager.GetSceneByBuildIndex(ActiveSceneIndex + 1).isLoaded)
        {
            return;
        }
        QueueTransition(transition, fadeTime);
        StartCoroutine(LoadScene(ActiveSceneIndex + 1));
    }

    public void LoadSceneByName(string sceneName, eTransitionEnums transition, float fadeTime = 1.0f)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            return;
        }
        QueueTransition(transition, fadeTime);
        var index = SceneManager.GetSceneByName(sceneName).buildIndex;
        StartCoroutine(LoadScene(index));
    }


    void WaitForTransistion()
    {
        RenderTransition.FinishedTransitionDel -= WaitForTransistion;
        LoadSceneNow = true;
    }

    IEnumerator LoadScene(int nextScene, bool UnloadActiveScene = true)
    {
        TransController.RunTransition(fadeIn: false, method: WaitForTransistion);

        while (!LoadSceneNow)
        {
            yield return null;
        }
        LoadSceneNow = false;

        var loadingScene = SceneManager.LoadSceneAsync(ActiveSceneIndex + 1, LoadSceneMode.Additive);

        while (!loadingScene.isDone)
        {
            yield return null;
        }

        var newScene = SceneManager.GetSceneByBuildIndex(nextScene);
        Debug.Log($"Loaded Scene {newScene.name}");
        string sceneToUnload = "";
        if (UnloadActiveScene)
        {
            sceneToUnload = ActiveScene;
            SceneManager.UnloadSceneAsync(ActiveScene);
            Debug.Log($"Unloaded Scene {ActiveScene}");
            SceneManager.SetActiveScene(newScene);
            ActiveSceneIndex = nextScene;
            ActiveScene = newScene.name;
        }

        while (sceneToUnload != "" && SceneManager.GetSceneByName(sceneToUnload).isLoaded)
        {
            yield return null;
        }

        TransController.RunTransition(fadeIn: true);

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
            QueueTransition(eTransitionEnums.Aperture, 0.5f);
            TransController.RunTransition(fadeIn: false, PowerOff);
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
