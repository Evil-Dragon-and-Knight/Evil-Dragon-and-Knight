using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    private AsyncOperation _operation;
    private static string _nextScene = "TitleScene";

    private void Start()
    {
        Time.timeScale = 1f;
        Invoke(nameof(StartLoading), 0.3f);
    }

    public static void LoadScene(string sceneName)
    {
        _nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    private void StartLoading()
    {
        StartCoroutine(LoadSceneProcess());
    }

    private IEnumerator LoadSceneProcess()
    {
        yield return null;
        _operation = SceneManager.LoadSceneAsync(_nextScene);
        _operation.allowSceneActivation = false;

        while (!_operation.isDone)
        {
            yield return null;
            if (_operation.progress >= 0.9f)
            {
                Invoke(nameof(RequestOK), 3f);
            }
        }
    }

    private void RequestOK()
    {
        _operation.allowSceneActivation = true;
    }
}
