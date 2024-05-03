using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private float alphaSpeed = 20;

    private bool isShow;

    private UnityAction hideCallBack;

    protected virtual void Awake()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
        }
    }

    public abstract void Init();

    protected virtual void Start()
    {
        Init();
    }


    protected virtual void Update()
    {
        changePanel();
    }

    private void changePanel()
    {
        //淡入
        if (isShow && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
            }
        }
        //淡出
        else if (!isShow)
        {
            canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                //回调
                hideCallBack?.Invoke();
            }
        }

    }

    public void Show()
    {
        isShow = true;
        gameObject.SetActive(true);
        canvasGroup.alpha = 0;
    }

    public void Hide(UnityAction callBack)
    {
        isShow = false;
        canvasGroup.alpha = 1;
        hideCallBack = callBack;
    }

}
