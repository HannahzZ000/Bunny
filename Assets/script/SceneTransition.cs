using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    // 设置要跳转的场景名称
    public string targetSceneName = "ARScene";

    void Update()
    {
        // 检测屏幕点击事件
        if (Input.GetMouseButtonDown(0)) // 左键鼠标点击或手机屏幕点击
        {
            // 加载目标场景
            LoadARScene();
        }
    }

    // 加载场景的方法
    void LoadARScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
