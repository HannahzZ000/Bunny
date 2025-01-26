using UnityEngine;

public class MicrophoneScaler : MonoBehaviour
{
    public GameObject targetObject; // 需要缩放的目标物体
    public float minScale = 0.5f; // 最小缩放比例
    public float maxScale = 3.0f; // 最大缩放比例
    public float sensitivity = 50.0f; // 音量敏感度

    private string microphoneDevice; // 麦克风设备名称
    private AudioClip micClip; // 麦克风录音片段
    private bool micInitialized = false; // 检查麦克风是否正常初始化

    void Start()
    {
        // 初始化麦克风
        if (Microphone.devices.Length > 0)
        {
            microphoneDevice = Microphone.devices[0]; // 使用第一个可用麦克风
            micClip = Microphone.Start(microphoneDevice, true, 1, 44100); // 循环录音，持续1秒，采样率44100Hz
            micInitialized = true;
            Debug.Log($"Microphone initialized: {microphoneDevice}");
        }
        else
        {
            Debug.LogError("No microphone detected on this device!");
        }
    }

    void Update()
    {
        if (micInitialized && targetObject != null)
        {
            // 获取麦克风音量
            float volume = GetMicrophoneVolume();

            // 映射音量到缩放范围
            float scale = Mathf.Lerp(minScale, maxScale, Mathf.Clamp01(volume * sensitivity));

            // 应用缩放到目标物体
            targetObject.transform.localScale = Vector3.Lerp(
                targetObject.transform.localScale, 
                new Vector3(scale, scale, scale), 
                Time.deltaTime * 5 // 平滑缩放
            );

            // 调试日志
            Debug.Log($"Volume: {volume}, Scale: {scale}");
        }
    }

    private float GetMicrophoneVolume()
    {
        if (micClip == null) return 0;

        // 获取当前麦克风数据位置
        int micPosition = Microphone.GetPosition(microphoneDevice) - 256; // 取样大小为256
        if (micPosition < 0) return 0;

        float[] waveData = new float[256]; // 用于存储音频数据
        micClip.GetData(waveData, micPosition); // 获取音频数据

        // 计算音量的峰值
        float maxVolume = 0;
        for (int i = 0; i < waveData.Length; i++)
        {
            float volume = Mathf.Abs(waveData[i]);
            if (volume > maxVolume)
            {
                maxVolume = volume;
            }
        }
        return maxVolume; // 返回峰值音量
    }
}