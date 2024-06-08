using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreventClickThrough : MonoBehaviour
{
    public string targetSceneName = "Scene2";
    private Button button;
    private Image childImage; 
    private Color originalColor;

    void Start()
    {
        button = GetComponent<Button>();

        if (button == null)
        {
            Debug.LogError("PreventClickThrough script is not attached to a Button.");
            return;
        }

        childImage = transform.GetChild(0).GetComponent<Image>();

        if (childImage == null)
        {
            Debug.LogError("The child does not have an Image component.");
            return;
        }

        originalColor = childImage.color;

        Scene targetScene = SceneManager.GetSceneByName(targetSceneName);
        if (targetScene.isLoaded)
        {
            childImage.color = originalColor;
            button.interactable = true;
        }
        else
        {
            childImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, originalColor.a * 0.5f);
            button.interactable = false;
        }
    }

    private void Update()
    {
        Scene targetScene = SceneManager.GetSceneByName(targetSceneName);

        if (childImage == null)
        {
            return;
        }

        if (targetScene.isLoaded)
        {
            // 恢复子物体的颜色和按钮的交互状态
            childImage.color = originalColor;
            button.interactable = true;
        }
        else
        {
            // 设置子物体的颜色和按钮的交互状态
            childImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, originalColor.a * 0.5f);
            button.interactable = false;
        }
    }
}
