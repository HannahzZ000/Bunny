using UnityEngine;

public class FireController : MonoBehaviour
{
    public Transform rabbit; // 兔子的Transform，用于计算距离
    public float maxDistance = 5.0f; // 火苗与兔子的最大消失距离
    public float minVolumeThreshold = 0.2f; // 声音的最低触发阈值

    private MicrophoneManager microphoneManager; // 引用麦克风管理器

    void Start()
    {
        // 动态查找场景中的 MicrophoneManager
        microphoneManager = FindObjectOfType<MicrophoneManager>();

        if (microphoneManager == null)
        {
            Debug.LogError("未找到 MicrophoneManager，请检查场景设置！");
        }
    }

    void Update()
    {
        if (microphoneManager == null || rabbit == null) return;

        // 获取当前麦克风音量
        float volume = microphoneManager.GetMicrophoneVolume();

        // 计算火苗与兔子的距离
        float distance = Vector3.Distance(transform.position, rabbit.position);

        // 当音量超过阈值并且距离满足条件时销毁火苗
        if (volume > minVolumeThreshold && distance < maxDistance)
        {
            Destroy(gameObject); // 销毁火苗
        }
    }
}
