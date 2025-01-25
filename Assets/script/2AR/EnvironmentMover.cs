using UnityEngine;

public class EnvironmentMover : MonoBehaviour
{
    public Transform rabbit; // 兔子的 Transform
    public float moveSpeed = 1.0f; // 环境移动的速度

    void Update()
    {
        if (rabbit == null)
        {
            Debug.LogError("Rabbit Transform 未设置，请在 Inspector 中设置！");
            return;
        }

        // 获取兔子绕 Y 轴的旋转角度
        float rabbitRotationY = rabbit.rotation.eulerAngles.y;

        // 将角度限制在 -180 到 180 度之间（防止跳变）
        if (rabbitRotationY > 180)
        {
            rabbitRotationY -= 360;
        }

        // 默认方向为 -Z（即 Vector3.back）
        Vector3 baseDirection = Vector3.back;

        // 根据兔子的 Y 轴旋转，调整移动方向
        Quaternion rotationAdjustment = Quaternion.Euler(0, rabbitRotationY, 0);
        Vector3 adjustedDirection = rotationAdjustment * baseDirection;

        // 环境沿着调整后的方向移动
        transform.position += adjustedDirection.normalized * moveSpeed * Time.deltaTime;
    }
}
