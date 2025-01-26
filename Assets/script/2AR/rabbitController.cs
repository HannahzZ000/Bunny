using UnityEngine;

public class GyroControl : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;
    private Quaternion initialRotation; // 初始旋转校准值
    private Quaternion unityRotationFix = new Quaternion(0, 0, -1, 0); // 修正方向调整

    [Range(0.1f, 10f)] public float smoothSpeed = 5f; // 平滑旋转速度
    [Range(0.1f, 5f)] public float rotationMultiplier = 1f; // 旋转比例因子

    void Start()
    {
        // 启用陀螺仪
        gyroEnabled = EnableGyro();

        if (gyroEnabled)
        {
            // 保存初始旋转值
            initialRotation = gyro.attitude * unityRotationFix;
            Debug.Log("Gyro enabled and initial rotation calibrated.");
        }
    }

    void Update()
    {
        if (gyroEnabled)
        {
            // 获取当前陀螺仪姿态并校正方向
            Quaternion currentRotation = gyro.attitude * unityRotationFix;

            // 计算相对于初始方向的旋转
            Quaternion deltaRotation = Quaternion.Inverse(initialRotation) * currentRotation;

            // 提取 Y 轴的欧拉角（避免其他方向旋转干扰）
            Vector3 euler = deltaRotation.eulerAngles;

            // 调整旋转幅度比例并反转方向
            euler.y *= -rotationMultiplier;

            // 平滑旋转到目标角度，仅影响 Y 轴
            Quaternion targetRotation = Quaternion.Euler(0, euler.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
        }
    }

    private bool EnableGyro()
    {
        // 检测设备是否支持陀螺仪
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            Debug.Log("Gyroscope enabled successfully.");
            return true;
        }
        else
        {
            Debug.LogWarning("Device does not support Gyroscope.");
            return false;
        }
    }
}