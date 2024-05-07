using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : Singleton<ScenesManager>
{
    [SceneName]
    public string startSceneName;
    private CanvasGroup fadeCanvasGroup;
    private bool isFade;
    protected override void Awake()
    {
        base.Awake();
        fadeCanvasGroup = FindObjectOfType<CanvasGroup>();
        SceneManager.LoadScene(startSceneName, LoadSceneMode.Additive);
    }

    
    private IEnumerator Transition(string sceneName, Vector3 targetPosition)
    {
        yield return Fade(1);
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        
        yield return LoadSceneSetActive(sceneName);
        
        yield return Fade(0);
    }
    private IEnumerator LoadSceneSetActive(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        
        SceneManager.SetActiveScene(newScene);
    }

    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;
        fadeCanvasGroup.blocksRaycasts = true;
        float speed = Mathf.Abs(targetAlpha - fadeCanvasGroup.alpha) / 1.0f;
        while(!Mathf.Approximately(targetAlpha, fadeCanvasGroup.alpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }
        fadeCanvasGroup.blocksRaycasts = false;
        isFade = false;
    }
}
