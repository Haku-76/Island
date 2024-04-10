using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
#region Singleton
    private static GameRoot _instance;

    public static GameRoot Instance{
        get => _instance;
    }
    void OnDestroy()
    {
        if(_instance == this)
            _instance = null;
    }
    #endregion

    /// <summary>
    /// 进入调酒游戏的事件
    /// </summary>
    public event Action EnterGameEvent;
    public void CallEnterGameEvent()
    {
        EnterGameEvent?.Invoke();
    }



    /// <summary>
    /// 结束游戏的事件
    /// </summary>
    /// <param name="data">游戏结果数据 ： 酒精度， 口感</param>
    public event Action<MixedWine_Data> FinishGameEvent;

    public void CallFinishGameEvent(MixedWine_Data data)
    {
        FinishGameEvent?.Invoke(data);
    }
    public event Action CloseGameEvent;
    

    public GameObject bar_Game;
    private GameObject barGame_instance;

    void OnEnable()
    {
        EnterGameEvent += OnEnterGameEvent;
        CloseGameEvent += OnCloseGameEvent;
    }

    void OnDisable()
    {
        EnterGameEvent -= OnEnterGameEvent;
        CloseGameEvent -= OnCloseGameEvent;
    }

    void Awake()
    {
        if(_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }

    void OnEnterGameEvent()
    {
        barGame_instance = Instantiate(bar_Game, transform);
    }

    void OnCloseGameEvent()
    {
        if(barGame_instance != null)
        {
            Destroy(barGame_instance);
        }
    }

    public void CloseGame()
    {
        CloseGameEvent?.Invoke();
    }


}
