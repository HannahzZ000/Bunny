using UnityEngine;

public class SettlementController : MonoBehaviour
{
    public GameObject successUI; // 成功的 UI 对象
    public GameObject failUI;    // 失败的 UI 对象

    private void Start()
    {
        // 确保所有 UI 初始状态为隐藏
        successUI.SetActive(false);
        failUI.SetActive(false);

        // 根据游戏状态显示对应的 UI
        if (GameStateManager.IsSuccess)
        {
            successUI.SetActive(true); // 显示成功 UI
        }
        else
        {
            failUI.SetActive(true); // 显示失败 UI
        }
    }
}
