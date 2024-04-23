﻿using System.Collections;
using UnityEngine;
using Yarn.Unity;
using TMPro;

public class GameActions : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public TextMeshProUGUI emotionTip;
    public TextMeshProUGUI acceptanceTip;
    public Value value;
    public float displayTime = 1.0f;
    public float fadeTime = 0.5f;

    void Awake()
    {
        dialogueRunner.AddCommandHandler<string>("adjustEmotion", AdjustEmotion);
        dialogueRunner.AddCommandHandler<string>("adjustAcceptance", AdjustAcceptance);
    }

    private void AdjustEmotion(string changeValue)
    {
        value.emotion += int.Parse(changeValue);
        emotionTip.text = "情绪价值 " + changeValue;
        Debug.Log($"New emotion: {value.emotion}");
        StartCoroutine(FadeText(emotionTip, true));
    }

    private void AdjustAcceptance(string changeValue)
    {
        value.acceptance += int.Parse(changeValue);
        acceptanceTip.text = "认同感 " + changeValue;
        Debug.Log($"New acceptance: {value.acceptance}");
        StartCoroutine(FadeText(acceptanceTip, true));
    }

    private IEnumerator FadeText(TextMeshProUGUI textMesh, bool fadeIn)
    {
        Color originalColor = textMesh.color;
        float startAlpha = fadeIn ? 0.0f : 1.0f;
        float endAlpha = fadeIn ? 1.0f : 0.0f;
        float time = 0;

        while (time < fadeTime)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / fadeTime);
            textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }
        textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, endAlpha);

        if (fadeIn)
        {
            yield return new WaitForSeconds(displayTime);
            StartCoroutine(FadeText(textMesh, false));
        }
    }
}