using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitController : MonoBehaviour
{
    void Update()
    {
        Vector3 forwardDirection = Camera.main.transform.forward;
        forwardDirection.y = 0; // 保持水平旋转
        transform.forward = forwardDirection;
    }
}
