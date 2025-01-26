using UnityEngine;
using UnityEngine.SceneManagement;

public class RoadController : MonoBehaviour
{
    private bool isRabbitOnRoad = false; // 兔子是否在马路上
    public GameObject rabbit; // 手动将兔子对象拖入

    private void OnTriggerEnter(Collider other)
    {
        // 检测兔子是否进入马路区域
        if (other.gameObject == rabbit)
        {
            isRabbitOnRoad = true;
            Debug.Log("Rabbit is on the road.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 检测兔子是否离开马路区域
        if (other.gameObject == rabbit)
        {
            isRabbitOnRoad = false;
            Debug.Log("Rabbit left the road!");

            // 切换到失败场景
            GameStateManager.IsSuccess = false;
            SceneManager.LoadScene("Settlement");
        }
    }
}