using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bar_GameManager : Singleton<Bar_GameManager>
{


    public ProgressUI progressUI;

    public WineGlass wineGlass;
    public Slider qte_slider;

    public Canvas gameCanvas;

    
    public bool isFinished => isAddWine & isAddWater;

    public bool isAddWine;
    public bool isAddWater; 

    private bool isOver = false;

    void Start()
    {
        gameCanvas.worldCamera = Camera.main;
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

    public void StartQTE(float bestScale, Bottle bottle)
    {
        StartCoroutine(AddWineQTE(bestScale, bottle));
    }

    IEnumerator AddWineQTE(float bestScale, Bottle bottle)
    {
        qte_slider.gameObject.SetActive(true);
        float timer = 0;
        while(timer < Const_Value.qte_time_max)
        {
            float qte_percent = timer / Const_Value.qte_time_max;
            qte_slider.value = qte_percent;
            if(Input.GetMouseButtonDown(0))
            {
                break;
            }
            timer += Time.deltaTime;
            yield return null;
        }

        bottle.addWineOver = true;
        qte_slider.gameObject.SetActive(false);
    }
}
