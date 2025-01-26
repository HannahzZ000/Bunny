using UnityEngine;

public class RabbitController : MonoBehaviour
{
    public float rotationSpeed = 50f; // 兔子的旋转速度
    public float fallSpeed = 2f; // 下落速度
    private bool isFalling = false; // 是否正在下落
    public UICanvasController uiCanvasController; // UI 控制器
    public float offScreenY = -10f; // 掉出屏幕的Y轴阈值

    void Update()
    {
        if (!isFalling)
        {
            // 绕 Y 轴旋转
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

            // 检测点击事件
            if (Input.GetMouseButtonDown(0)) // 点击屏幕任意位置
            {
                isFalling = true; // 开始下落
            }
        }
        else
        {
            // 下落逻辑，保持旋转
            transform.position -= new Vector3(0, fallSpeed * Time.deltaTime, 0);

            // 检测是否完全掉出屏幕
            if (transform.position.y < offScreenY)
            {
                isFalling = false; // 停止下落
                uiCanvasController.ShowUI2AndHideUI1(); // 显示 UI2 并隐藏 UI1
                gameObject.SetActive(false); // 隐藏兔子
            }
        }
    }
}
