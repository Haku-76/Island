using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class GuideTextController : MonoBehaviour
{
    public TextMeshProUGUI guideText;
    public Button button;
    
    public void HideButton()
    {
       button.gameObject.SetActive(false);
    }
    public void ShowText()
    {
        guideText.gameObject.SetActive(true);
    }
}
