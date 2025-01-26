using UnityEngine;

public class FireController : MonoBehaviour
{
    public Transform rabbit; // 兔子位置
    public float disappearRate = 1f; // 消失速率
    public float maxDistance = 5f; // 火苗作用范围

    private bool isDisappearing = false; // 是否正在消失

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) // 按下空格
        {
            isDisappearing = true;
            float distance = Vector3.Distance(transform.position, rabbit.position);

            // 越靠近兔子的火苗优先消失
            if (distance <= maxDistance)
            {
                float disappearSpeed = disappearRate / distance; // 距离越近消失越快
                transform.localScale -= Vector3.one * disappearSpeed * Time.deltaTime;

                // 火苗完全消失
                if (transform.localScale.x <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
