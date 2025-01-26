using UnityEngine;
using UnityEngine.SceneManagement;

public class RabbitHoleController : MonoBehaviour
{
    public GameObject rabbit; // 手动将兔子对象拖入这里

    private void OnTriggerEnter(Collider other)
    {
        // 检测是否与指定的兔子对象碰撞
        if (other.gameObject == rabbit)
        {
            Debug.Log("Rabbit entered the rabbit hole!");

            // 设置游戏状态为成功
            GameStateManager.IsSuccess = true;

            // 切换到 Settlement 场景
            SceneManager.LoadScene("Settlement");
        }
    }
}
