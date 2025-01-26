using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public GameObject environment; // 环境（例如马路）
    public float environmentSpeed = 2f; // 环境移动的恒定速度
    public Transform rabbit; // 兔子的 Transform，用于获取其方向

    void Update()
    {
        if (rabbit != null && environment != null)
        {
            // 获取兔子的 Y 轴旋转方向
            float yRotation = rabbit.eulerAngles.y;

            // 根据兔子的方向计算移动矢量（反方向）
            Vector3 forwardDirection = Quaternion.Euler(0, yRotation, 0) * Vector3.forward;

            // 环境以恒定速度朝反方向移动
            environment.transform.position += -forwardDirection * environmentSpeed * Time.deltaTime;
        }
    }
}