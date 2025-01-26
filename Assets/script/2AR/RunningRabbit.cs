using UnityEngine;
using UnityEngine.SceneManagement;

public class RunningRabbit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire")) // 碰撞火苗
        {
            Debug.Log("Game Over: Hit Fire!");
            SceneManager.LoadScene("Settlement"); // 切换到结算场景
        }
        else if (other.CompareTag("Flag")) // 碰撞红旗
        {
            Debug.Log("Game Success: Reached Flag!");
            SceneManager.LoadScene("Settlement"); // 切换到结算场景
        }
    }
}
