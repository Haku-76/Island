using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelManager:Singleton<UIPanelManager>
{
    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();


    void Awake()
    {
        base.Awake();

    }

    public T showPanel<T>(bool isdynamic = true) where T : BasePanel
    {
        string panelName = typeof(T).Name;
        string path = "UI/" + panelName;
        if (isdynamic)
        {
            if (!panelDic.ContainsKey(panelName))
            {
                GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>(path));
                panelObj.transform.SetParent(transform, false);

                T panel = panelObj.GetComponent<T>();
                panelDic.Add(panelName, panel);

                panel.Show();
                return panel;
            }
            return panelDic[panelName] as T;
        }
        else
        {
            T panel =  transform.Find(panelName).GetComponent<T>();
            panel.Show();
            return panel;
        }
    }

    public void HidePanel<T>(bool isFade = true) where T : BasePanel
    {
        string panelName = typeof(T).Name;

        if (panelDic.ContainsKey(panelName))
        {
            if (isFade)
            {
                panelDic[panelName].Hide(() =>
                {
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    panelDic.Remove(panelName);
                });
            }
            else
            {
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
        }
    }

    public T getPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }
        //Debug.Log("δ���ֵ����ҵ���Ӧ�氁E);
        return null;
    }
}
