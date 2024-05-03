using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string newSceneName;

    public void LoadNewScene()
    {
        SceneManager.LoadScene(newSceneName);
    }
}
