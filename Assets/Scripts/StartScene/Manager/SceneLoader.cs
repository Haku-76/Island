using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SceneName]
    public string newSceneName;

    public void LoadNewScene()
    {
        ScenesManager.Instance.GoToMainScene(new Vector3(0,-1.25f,0));
    }
}
