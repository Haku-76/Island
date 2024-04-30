using System.Collections;
using UnityEngine;
using Yarn.Unity;
using TMPro;
using UnityEngine.SceneManagement;

public class GameActions : MonoBehaviour
{
    private GameObject player;
    public DialogueRunner dialogueRunner;
    public TextMeshProUGUI emotionTip;
    public TextMeshProUGUI acceptanceTip;
    public Value value;
    public float displayTime = 1.0f;
    public float fadeTime = 0.5f;
    public NPC currentNPC;

    void Awake()
    {
        dialogueRunner.AddCommandHandler<string>("adjustEmotion", AdjustEmotion);
        dialogueRunner.AddCommandHandler<string>("adjustAcceptance", AdjustAcceptance);
        dialogueRunner.AddCommandHandler<string>("changeScene", ChangeScene);
        dialogueRunner.AddCommandHandler("lock_player", LockPlayer); 
        dialogueRunner.AddCommandHandler("unlock_player", UnLockPlayer);
        dialogueRunner.AddCommandHandler("onDialogueEnd", OnDialogueEnd);
        dialogueRunner.AddCommandHandler("exitBar", ExitBar);
        
        dialogueRunner.AddCommandHandler("plankSpankerPlayAni", PlankSpanker_PlayRecital);
        dialogueRunner.AddCommandHandler("plankSpankerStopPlayAni", PlankSpanker_StopPlayRecital);
        dialogueRunner.AddCommandHandler("plankSpankerStartWork", PlankSpanker_StartWork);
        dialogueRunner.AddCommandHandler("npcOver", NPCOver);

        player = GameObject.FindWithTag("Player");
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

    private void ChangeScene(string sceneName)
    {
        Debug.Log($"Changing to scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }

    private void LockPlayer()
    {
        player.GetComponent<PlayerController>().LockPlayer();
    }

    private void OnDialogueEnd()
    {
        UnLockPlayer();
    }

    private void UnLockPlayer()
    {
        player.GetComponent<PlayerController>().UnLockPlayer();
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

#region NPC_Actions
    private void ExitBar()
    {
        currentNPC.exitBar();
    }

    private void NPCOver()
    {
        NPCManager.Instance.SetNPCCourseOver(currentNPC.npc_Course);
    }

    private void PlankSpanker_PlayRecital()
    {
        var currentChar = currentNPC as Guitarist;
        currentChar.PlayAni();
        
    }

    private void PlankSpanker_StopPlayRecital()
    {
        var currentChar = currentNPC as Guitarist;
        currentChar.StopPlayAni();
    }

    private void PlankSpanker_StartWork()
    {
        var currentChar = currentNPC as Guitarist;
        currentChar.StartWork();
    }
#endregion
}
