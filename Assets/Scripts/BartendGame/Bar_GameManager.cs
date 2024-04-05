using System;
using UnityEngine;
using UnityEngine.UI;

public class Bar_GameManager : MonoBehaviour
{
#region Singleton
    private static Bar_GameManager _instance;

    public static Bar_GameManager Instance{
        get => _instance;
    }
    #endregion

    void Awake()
    {
        if(_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }
    void OnDestroy()
    {
        if(_instance == this)
            _instance = null;
    }

    public event Action EnterGameEvent;

    public event Action FinishGameEvent;

    public DrinksUI drinks_DragUI;

    public void EnterGame()
    {
        EnterGameEvent?.Invoke();
    }

    public void FinishGame()
    {
        FinishGameEvent?.Invoke();
    }
    
}
