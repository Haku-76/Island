using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace MainScene
{
    public class GOmovement : MonoBehaviour
    {
        private bool _isWineDone;
    
        public float moveSpeed = 5f;
        public CameraController cameraController;
        public GameObject NPC;
        public float jumpDuration = 3f;
        [FormerlySerializedAs("InBarDuration")] public float inBarDuration = 5f;
        public float inSceneDuration = 6f;
        public Vector3 targetPosition0;
        public Vector3 targetPosition2;
        public GameRoot gameRootInstance;
        public GuideTextController guideTextController;
    
    

        void Start()
        {
            _isWineDone = false;
            gameRootInstance = GameRoot.Instance;
            if (gameObject.name == "NPC")
            {
                GameRoot.FinishGameEvent += HandleFinishGameEvent;
            }
        }
        IEnumerator MoveCoroutine()
        {
        
            float elapsedTime = 0f;
        
            Vector3 initialPosition = NPC.transform.position;
            Vector3 targetPosition1 = new Vector3(27.32f, 1.65f, 0f);

            while (elapsedTime < inSceneDuration)
            {
                gameObject.transform.position = Vector3.Lerp(initialPosition, targetPosition0, elapsedTime / inSceneDuration);
                elapsedTime += Time.deltaTime; // 更新经过的时间
                yield return null;
            }
        
            elapsedTime = 0f;
        
            while (elapsedTime < jumpDuration)
            {
                NPC.transform.position = Vector3.Lerp(targetPosition0, targetPosition1, elapsedTime / jumpDuration);
                elapsedTime += Time.deltaTime; 
                yield return null;
            }

            yield return new WaitForSeconds(1f);

            elapsedTime = 0f;

            if (cameraController != null)
            {
                cameraController.ZoomIn();
            }
            else
            {
                Debug.LogError("CameraController not found!");
            }
        
            while (elapsedTime < inBarDuration)
            {
                NPC.transform.position = Vector3.Lerp(targetPosition1, targetPosition2, elapsedTime / inBarDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            if (NPC.GetComponent<OnClickEvent>() == null)
            {
                NPC.AddComponent<OnClickEvent>();
            }
            else
            {
                Debug.LogWarning("OnClickEvent component already exists on NPC.");
            }

            if (gameObject.name == "Boat")
            {
                gameObject.transform.position = new Vector3(48.11f, -3.36f, 0f);
            }

            yield return new WaitForSeconds(2f);
        
            guideTextController.ShowText();

            guideTextController.guideText.text = "Here Comes A Guide";

            yield return new WaitForSeconds(2f);
        
            guideTextController.guideText.text = "This Yellow Man Wants A Wine mixed with Soda & 0.2 Alcohol. ";
        
            yield return new WaitForSeconds(2f);
        
            guideTextController.guideText.text = "Press Him to Make Him A Drink. ";

            if (_isWineDone)
            {
                yield return new WaitForSeconds(1f);
                guideTextController.guideText.text = "Good Now U Can Have A Sleep Now";
            }
        }
        void HandleFinishGameEvent(MixedWine_Data mixedWineData)
        {
            // 在这里处理传递过来的 MixedWine_Data 对象
            Debug.Log("Received mixed wine data: Alcohol - " + mixedWineData.alcohol + ", Taste - " + mixedWineData.taste);
            if (Math.Abs(mixedWineData.alcohol - 0.2f) < 0.001 && mixedWineData.taste == WaterTag.soda)
            {
                Debug.Log("YES!");
                guideTextController.guideText.text = "Good!Now His xxx Has Improved.";
                _isWineDone = true;
            }
            else
            {
                Debug.LogError("NO!");
                guideTextController.guideText.text = "Press Him Again Until U Make Him A Wanted Drink.";
            }
        }
    
        public void WantAWine()
        {
            if (gameRootInstance != null)
            {
                // 调用进入游戏的方法
                gameRootInstance.CallEnterGameEvent();
                guideTextController.guideText.text = "Drag to Add Things to The Drink. ";
            }
            else
            {
                Debug.LogError("GameRoot instance is null. Make sure GameRoot object exists in the scene.");
            }
        }

        public void StartMove()
        {
            StartCoroutine(MoveCoroutine());
        }
    }
}

