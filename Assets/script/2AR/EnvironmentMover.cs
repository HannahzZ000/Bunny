using UnityEngine;

public class EnvironmentMover : MonoBehaviour
{
    public float moveSpeed = 5f; // 环境移动速度
    private Vector3 moveDirection = Vector3.back; // 默认向-Z方向移动

    private void Update()
    {
        // 持续移动环境
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // 检测按键调整方向
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RotateEnvironment(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RotateEnvironment(Vector3.right);
        }
    }

    private void RotateEnvironment(Vector3 inputDirection)
    {
        float angle = inputDirection == Vector3.left ? -10f : 10f; // 每次调整10度
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        moveDirection = rotation * moveDirection; // 更新移动方向
        Debug.Log("Updated move direction: " + moveDirection);
    }
}
