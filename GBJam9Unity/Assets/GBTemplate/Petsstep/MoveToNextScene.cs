using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextScene : MonoBehaviour
{
    public void LoadNextScene()
    {
        StartCoroutine(WaitWhileLoadNextScene());
    }

    IEnumerator WaitWhileLoadNextScene()
    {
        int thisScene = SceneManager.GetActiveScene().buildIndex;
        var loadingScene = SceneManager.LoadSceneAsync(thisScene + 1, LoadSceneMode.Additive);

        while (!loadingScene.isDone)
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync(thisScene);
    }
}
