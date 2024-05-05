using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class GameRoot : MonoBehaviour
{
#region Singleton
    private static GameRoot _instance;

    public static GameRoot Instance{
        get => _instance;
    }
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
    #endregion
    /// <summary>
    /// 进入调酒游戏的事件
    /// </summary>
    public static event Action EnterGameEvent;
    public void CallEnterGameEvent()
    {
        EnterGameEvent?.Invoke();
    }

    /// <summary>
    /// 结束游戏的事件
    /// </summary>
    /// <param name="data">游戏结果数据 ： 酒精度， 口感</param>
    public static event Action<MixedWine_Data> FinishGameEvent;

    public void CallFinishGameEvent(MixedWine_Data data)
    {
        FinishGameEvent?.Invoke(data);
    }
    

    public GameObject bar_Game;
    private GameObject barGame_instance;

    public CinemachineVirtualCamera gameCameraStand; 

    void OnEnable()
    {
        EnterGameEvent += OnEnterGameEvent;
        FinishGameEvent += OnFinishGameEvent;
    }

    void OnDisable()
    {
        EnterGameEvent -= OnEnterGameEvent;
        FinishGameEvent -= OnFinishGameEvent;
    }


    void OnEnterGameEvent()
    {
        barGame_instance = Instantiate(bar_Game, transform);
        Debug.Log($"EnterGame");
        TransitionManager.Instance.SetCameraWithOutTransition(gameCameraStand, false);
    }

    void OnFinishGameEvent(MixedWine_Data data)
    {
        TransitionManager.Instance.ReturnPreCamera();
    }

    public void CloseGame()
    {
        if(barGame_instance != null)
        {
            Destroy(barGame_instance);
        }
    }


}
