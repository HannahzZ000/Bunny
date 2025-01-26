using UnityEngine;
using UnityEngine.SceneManagement;

public class FireController : MonoBehaviour
{
    public Transform rabbit; // 兔子对象
    public float minScale = 0.5f; // 最小缩放比例
    public float maxScale = 3.0f; // 最大缩放比例
    public float sensitivity = 100.0f; // 音量敏感度
    public float soundThreshold = 0.2f; // 声音阈值
    public float maxEffectDistance = 20.0f; // 声音作用的最大距离

    private float randomOffset; // 随机偏移，控制火焰独立缩放

    private void Start()
    {
        randomOffset = Random.Range(0f, 100f); // 初始化随机偏移
        Debug.Log($"FireController initialized at position: {transform.position}");
    }

    private void Update()
    {
        if (rabbit == null)
        {
            Debug.LogError("Rabbit is not assigned to FireController!");
            return;
        }

        HandleFireLogic();
    }

    private void HandleFireLogic()
    {
        // 获取火焰和兔子之间的距离
        float distanceToRabbit = Vector3.Distance(transform.position, rabbit.position);
        Debug.Log($"Fire Position: {transform.position}, Distance to Rabbit: {distanceToRabbit}");

        // 如果超出作用范围，火焰保持随机缩放动画
        if (distanceToRabbit > maxEffectDistance)
        {
            RandomScale();
            return;
        }

        // 获取音量
        if (MicrophoneManager.Instance == null)
        {
            Debug.LogError("MicrophoneManager is not initialized!");
            return;
        }
        float volume = MicrophoneManager.Instance.CurrentVolume;

        // 缩放和消失逻辑
        if (volume > soundThreshold)
        {
            float effectFactor = 1 - (distanceToRabbit / maxEffectDistance); // 距离越近效果越强
            Debug.Log($"Volume: {volume}, Effect Factor: {effectFactor}");

            if (volume * effectFactor * sensitivity > soundThreshold)
            {
                Debug.Log($"Destroying fire at position: {transform.position}");
                Destroy(gameObject); // 火焰消失
                return;
            }
        }

        // 随机缩放火焰
        RandomScale();
    }

    private void RandomScale()
    {
        // 使用 PerlinNoise 动态调整缩放
        float scale = Mathf.Lerp(minScale, maxScale, Mathf.PerlinNoise(Time.time + randomOffset, 0));
        transform.localScale = new Vector3(scale, scale, scale);
        Debug.Log($"Fire Scale Updated: {transform.localScale}");
    }

    private void OnTriggerEnter(Collider other)
    {
        // 处理兔子碰撞逻辑
        if (other.transform == rabbit)
        {
            Debug.Log("Rabbit collided with fire! Switching to Settlement scene.");
            GameStateManager.IsSuccess = false; // 设置为失败
            SceneManager.LoadScene("Settlement"); // 切换到 Settlement 场景
        }
    }
}