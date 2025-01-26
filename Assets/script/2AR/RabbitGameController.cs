using UnityEngine;

public class RabbitGameController : MonoBehaviour
{
    public Transform rabbit; // 兔子对象
    public Transform environment; // 环境对象
    public float environmentMoveSpeed = 3.0f; // 环境移动速度
    public float smoothSpeed = 5f; // 平滑移动速度

    private Vector3 moveDirection; // 环境移动方向
    private Gyroscope gyro;
    private bool gyroEnabled;
    private Quaternion initialRotation; // 初始旋转校准值
    private Quaternion unityRotationFix = new Quaternion(0, 0, 1, 0); // 左手到右手坐标转换

    private void Start()
    {
        // 启用陀螺仪
        gyroEnabled = EnableGyro();

        if (gyroEnabled)
        {
            // 保存初始旋转值
            initialRotation = gyro.attitude * unityRotationFix;
            Debug.Log("Gyro enabled and initial rotation calibrated.");
        }

        if (rabbit != null)
        {
            moveDirection = rabbit.forward; // 初始化环境移动方向
        }
    }

    private void Update()
    {
        if (rabbit == null || environment == null)
        {
            Debug.LogError("Rabbit or Environment is not assigned!");
            return;
        }

        if (gyroEnabled)
        {
            RotateRabbitWithGyro(); // 通过陀螺仪旋转兔子
        }

        MoveEnvironment(); // 根据兔子方向移动环境
    }

    private void RotateRabbitWithGyro()
    {
        // 获取当前陀螺仪姿态并校正方向
        Quaternion currentRotation = gyro.attitude * unityRotationFix;

        // 计算相对于初始方向的旋转
        Quaternion deltaRotation = Quaternion.Inverse(initialRotation) * currentRotation;

        // 提取 Y 轴的欧拉角（忽略其他方向旋转）
        Vector3 euler = deltaRotation.eulerAngles;

        // 平滑旋转兔子，仅影响 Y 轴
        Quaternion targetRotation = Quaternion.Euler(0, euler.y, 0);
        rabbit.rotation = Quaternion.Lerp(rabbit.rotation, targetRotation, Time.deltaTime * smoothSpeed);

        // 更新环境移动方向
        moveDirection = rabbit.forward;
    }

    private void MoveEnvironment()
    {
        // 根据兔子的方向平滑移动环境
        environment.position += -moveDirection * environmentMoveSpeed * Time.deltaTime;
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