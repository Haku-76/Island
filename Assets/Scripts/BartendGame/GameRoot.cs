using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class GameRoot : Singleton<GameRoot>
{
    public GameObject bar_Game;
    // private GameObject barGame_instance;

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
        bar_Game.SetActive(true);
        Debug.Log($"EnterGame");
        TransitionManager.Instance.SetCameraWithOutTransition(gameCameraStand, false);
    }

    public void StartGame()
    {
        bar_Game.SetActive(true);
    }

    void OnFinishGameEvent(MixedWine_Data data)
    {
        TransitionManager.Instance.ReturnPreCamera();
    }

    public void CloseGame()
    {
        bar_Game.SetActive(false);
    }


}
