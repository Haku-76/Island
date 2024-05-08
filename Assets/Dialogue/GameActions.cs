using System.Collections;
using UnityEngine;
using Yarn.Unity;
using TMPro;
using UnityEngine.SceneManagement;
using Spine.Unity;
using System;

public class GameActions : Singleton<GameActions>
{
    private GameObject player;
    public DialogueRunner dialogueRunner;
    public TextMeshProUGUI emotionTip;
    public TextMeshProUGUI acceptanceTip;
    public Value value;
    public Item novelistLetter;
    public static int letterCount;
    public float displayTime = 1.0f;
    public float fadeTime = 0.5f;
    public NPC currentNPC;

    void Awake()
    {
        base.Awake();
        dialogueRunner.AddCommandHandler<string>("adjustEmotion", AdjustEmotion);
        dialogueRunner.AddCommandHandler<string>("adjustAcceptance", AdjustAcceptance);
        dialogueRunner.AddCommandHandler<string>("changeScene", ChangeScene);
        dialogueRunner.AddCommandHandler("lock_player", LockPlayer); 
        dialogueRunner.AddCommandHandler("unlock_player", UnLockPlayer);
        dialogueRunner.AddCommandHandler("onDialogueEnd", OnDialogueEnd);
        dialogueRunner.AddCommandHandler("startBartten", StartBartten);

        dialogueRunner.AddCommandHandler<int, string>("sendLetter", SendLetter);
        dialogueRunner.AddCommandHandler<string>("leftLetter", LeftLetter);
        dialogueRunner.AddCommandHandler<string>("getLetter", GetLetter);

        dialogueRunner.AddCommandHandler<string, int, int>("NPCEnter", NPCEnter);
        dialogueRunner.AddCommandHandler("moveToBartten", MoveToBartten);

        dialogueRunner.AddCommandHandler("exitBar", ExitBar);
        dialogueRunner.AddCommandHandler<string>("npcExitBar", NPCExitBar);
        dialogueRunner.AddCommandHandler("npcOver", NPCOver);
        
        dialogueRunner.AddCommandHandler("plankSpankerPlayAni", PlankSpanker_PlayRecital);
        dialogueRunner.AddCommandHandler("plankSpankerStopPlayAni", PlankSpanker_StopPlayRecital);
        dialogueRunner.AddCommandHandler("plankSpankerStartWork", PlankSpanker_StartWork);

        novelistLetter = Resources.Load<Item>("Novelist_1");
        dialogueRunner.AddCommandHandler("getNovelistLetter", () => GetNovelistLetter());

        player = GameObject.FindWithTag("Player");
    }

    void OnEnable()
    {
        GameRoot.FinishGameEvent += OnFinishGameEvent;
    }

    void OnDisable()
    {
        GameRoot.FinishGameEvent -= OnFinishGameEvent;
    }

    void Update()
    {
        letterCount = novelistLetter.itemHeld;
    }

    [YarnFunction("getNovelistLetter")]
    public static int GetNovelistLetter()
    {
        print(letterCount);
        return letterCount;
    }

    private void OnFinishGameEvent(MixedWine_Data wine_data)
    {
        currentNPC.OnFinishGameEvent(wine_data);
    }

    private void SendLetter(int afterDay, string letter_name)
    {
        MailManager.Instance.SendLetter(afterDay, letter_name);
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

    private void NPCEnter(string name, int flag1, int flag2)
    {
        NPCManager.Instance.NPCEnter(name, flag1, flag2);
    }

    private void StartBartten()
    {
        GameRoot.Instance.CallEnterGameEvent();
        MoveToBartten();
    }

    private void MoveToBartten()
    {
        player.transform.position = Position_Data.Bartten_Pos;
        EnterBartten();
    }

    private void EnterBartten()
    {
        var renderer = player.GetComponent<Renderer>();
        renderer.sortingLayerName = "Item";
        renderer.sortingOrder = 1;
    }

    private void LockPlayer()
    {
        player.GetComponent<PlayerController>().LockPlayer();
    }

    private void OnDialogueEnd()
    {
        UnLockPlayer();
        DialogueManager.Instance.isDialoguing = false;
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

    private void GetLetter(string letter_name)
    {
        InventoryManager.AddLetterToBag(letter_name);
    }
    private void LeftLetter(string letter_name)
    {
        MailManager.Instance.LeftLetter(letter_name);
    }
    private void ExitBar()
    {
        currentNPC.exitBar();
    }

    private void NPCExitBar(string name)
    {
        NPCManager.Instance.NPCExit(name);
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
