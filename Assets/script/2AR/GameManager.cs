using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform rabbit; // 兔子
    public Transform flag; // 红旗
    public string settlementSceneName = "Settlement";

    void Update()
    {
        // 检测与火苗的碰撞
        foreach (Transform fire in GameObject.Find("Environment/Fires").transform)
        {
            if (Vector3.Distance(rabbit.position, fire.position) < 1f)
            {
                SceneManager.LoadScene(settlementSceneName);
                PlayerPrefs.SetString("Result", "lost");
                return;
            }
        }

        // 检测与红旗的碰撞
        if (Vector3.Distance(rabbit.position, flag.position) < 1f)
        {
            SceneManager.LoadScene(settlementSceneName);
            PlayerPrefs.SetString("Result", "saved");
        }
    }
}
