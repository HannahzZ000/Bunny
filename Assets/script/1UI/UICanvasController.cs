using UnityEngine;
using UnityEngine.SceneManagement;

public class UICanvasController : MonoBehaviour
{
    public CanvasGroup ui2; // UI2 的 CanvasGroup
    public float fadeSpeed = 1f; // UI2 的渐显速度
    public string arSceneName = "ARScene"; // AR 场景的名称

    private bool isFading = false; // 防止重复触发

    // 渐显 UI2
    public void SwitchToUI2()
    {
        if (!isFading)
        {
            StartCoroutine(FadeInUI2());
        }
    }

    private System.Collections.IEnumerator FadeInUI2()
    {
        isFading = true;

        // 确保 UI2 可见
        ui2.gameObject.SetActive(true);

        // 渐显 UI2 的透明度
        while (ui2.alpha < 1)
        {
            ui2.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }

        ui2.alpha = 1;
        isFading = false;
    }

    // 切换到 AR 场景
    public void SwitchToARScene()
    {
        if (!string.IsNullOrEmpty(arSceneName))
        {
            SceneManager.LoadScene(arSceneName);
        }
        else
        {
            Debug.LogError("AR Scene Name is not set in the UICanvasController.");
        }
    }
}
