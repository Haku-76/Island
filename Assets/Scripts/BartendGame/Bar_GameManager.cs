using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bar_GameManager : MonoBehaviour
{
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

    public SettlementPanel settlementUI;

    public WineGlass wineGlass;

    
    public bool isFinished => isAddWine & isAddWater;

    public bool isAddWine;
    public bool isAddWater; 

    void Start()
    {
        settlementUI.gameObject.SetActive(false);
        isAddWater = false;
        isAddWine = false;
    }

    void Update()
    {
        if(isFinished)
        {
            StartCoroutine(FinishGame());
        }
    }
    
    IEnumerator FinishGame()
    {
        settlementUI.gameObject.SetActive(true);
        settlementUI.Settle(wineGlass.GetAlcohol());
        yield return new WaitForSeconds(3f);

        GameRoot.Instance.CloseGame();
    }
}
