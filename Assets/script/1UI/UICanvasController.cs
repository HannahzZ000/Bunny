using UnityEngine;
using UnityEngine.SceneManagement;

public class UICanvasController : MonoBehaviour
{
    public CanvasGroup ui1; // UI1 的 CanvasGroup
    public CanvasGroup ui2; // UI2 的 CanvasGroup
    public string arSceneName = "ARScene"; // 目标 AR 场景的名称

    /// <summary>
    /// 显示 UI2，并在 1 秒后隐藏 UI1
    /// </summary>
    public void ShowUI2AndHideUI1()
    {
        // 显示 UI2
        ui2.alpha = 1;
        ui2.interactable = true;
        ui2.blocksRaycasts = true;
        ui2.gameObject.SetActive(true);

        // 延迟 1 秒隐藏 UI1
        Invoke(nameof(HideUI1), 0.01f);
    }

    /// <summary>
    /// 隐藏 UI1
    /// </summary>
    private void HideUI1()
    {
        ui1.alpha = 0;
        ui1.interactable = false;
        ui1.blocksRaycasts = false;
        ui1.gameObject.SetActive(false);
        Debug.Log("UI1 is now hidden.");
    }

    /// <summary>
    /// 切换到 AR 场景
    /// </summary>
    public void SwitchToARScene()
    {
        Debug.Log("SwitchToARScene method called!"); // 调试日志
        if (!string.IsNullOrEmpty(arSceneName))
        {
            Debug.Log($"Loading scene: {arSceneName}");
            SceneManager.LoadScene(arSceneName);
        }
        else
        {
            Debug.LogError("AR Scene Name is not set in the UICanvasController.");
        }
    }
}
