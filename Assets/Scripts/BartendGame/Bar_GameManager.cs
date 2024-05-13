using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bar_GameManager : Singleton<Bar_GameManager>
{

    public DrinksUI drinks_DragUI;

    public ProgressUI progressUI;

    public WineGlass wineGlass;

    
    public bool isFinished => isAddWine & isAddWater;

    public bool isAddWine;
    public bool isAddWater; 

    private bool isOver = false;

    void Start()
    {
        isAddWater = false;
        isAddWine = false;
    }

    void Update()
    {
        if(isFinished && !isOver)
        {
            isOver = true;
            StartCoroutine(FinishGame());
        }
    }
    
    IEnumerator FinishGame()
    {
        EventHandler.CallFinishGameEvent(wineGlass.GetResult());
        // yield return new WaitForSeconds(3f);
        yield return null;
        GameRoot.Instance.CloseGame();
    }
}
