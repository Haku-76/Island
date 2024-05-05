using MainScene;
using UnityEngine;

public class OnClickEvent : MonoBehaviour
{
    void OnMouseDown()
    {
        // 当点击 NPC 时触发 WantAWine 事件
        FindObjectOfType<GOmovement>().WantAWine();
    }
}

