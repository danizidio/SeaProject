using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public void SelectingNextScene(string sceneName)
    {
        StartCoroutine(LoadingNextScene(sceneName));
    }

    public IEnumerator LoadingNextScene(string sceneName)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName);

        while (!loading.isDone)
        {

            yield return null;
        }
    }
}
