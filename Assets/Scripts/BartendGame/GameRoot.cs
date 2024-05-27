using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class GameRoot : Singleton<GameRoot>
{
    public GameObject bar_Game;
    private GameObject barGame_instance;

    public CinemachineVirtualCamera gameCameraStand; 

    void OnEnable()
    {
        EventHandler.FinishGameEvent += OnFinishGameEvent;
    }

    void OnDisable()
    {
        EventHandler.FinishGameEvent -= OnFinishGameEvent;
    }


    public void EnterGame()
    {
        barGame_instance = Instantiate(bar_Game, transform);
        Debug.Log($"EnterGame");
        TransitionManager.Instance.SetCameraWithOutTransition(gameCameraStand, false);
    }

    public void StartGame()
    {
        barGame_instance = Instantiate(bar_Game, transform);
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
