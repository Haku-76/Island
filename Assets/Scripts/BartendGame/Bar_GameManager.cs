using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bar_GameManager : MonoBehaviour
{
    
    private bool isGameFinished = false;
    
#region Singleton
    private static Bar_GameManager _instance;
    
    public static Bar_GameManager Instance{
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


    public DrinksUI drinks_DragUI;

    public ProgressUI progressUI;

    public WineGlass wineGlass;

    
    public bool isFinished => isAddWine & isAddWater;

    public bool isAddWine;
    public bool isAddWater; 

    void Start()
    {
        isAddWater = false;
        isAddWine = false;
    }

    void Update()
    {
        if(isFinished && !isGameFinished)
        {
            isGameFinished = true;
            StartCoroutine(FinishGame());
        }
    }
    
    IEnumerator FinishGame()
    {
        GameRoot.Instance.CallFinishGameEvent(wineGlass.GetResult());
        yield return new WaitForSeconds(3f);

        GameRoot.Instance.CloseGame();
    }
}
