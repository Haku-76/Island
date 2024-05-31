using UnityEngine;
using UnityEngine.UI;

public class ProgressUI : MonoBehaviour
{
    private Image unit_1;
    private Image unit_2;

    void Awake()
    {
        unit_1 = GetComponentsInChildren<Image>()[1];
        unit_2 = GetComponentsInChildren<Image>()[2];
    }

    void Start()
    {
        Init();
    }

    void Init()
    {
       unit_1.enabled = false;
       unit_2.enabled = false;
    }

    public void AddWine()
    {
        unit_1.enabled = true;
        Bar_GameManager.Instance.isAddWine = true;
    }

    public void AddWater()
    {
        Debug.Log(2);
        unit_2.enabled = true;
        Bar_GameManager.Instance.isAddWater = true;
    }
}
