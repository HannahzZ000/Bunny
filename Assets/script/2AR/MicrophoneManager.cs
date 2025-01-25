using UnityEngine;

public class MicrophoneManager : MonoBehaviour
{
    private AudioSource audioSource; // 音频源

    void Start()
    {
        // 添加AudioSource组件
        audioSource = gameObject.AddComponent<AudioSource>();

        // 设置麦克风录制
        audioSource.clip = Microphone.Start(null, true, 10, 44100); // 启用麦克风
        audioSource.loop = true; // 循环录制
        audioSource.mute = true; // 静音（避免录制音频时播放）
        while (!(Microphone.GetPosition(null) > 0)) { } // 等待麦克风准备
        audioSource.Play(); // 开始录制
    }

    void Update()
    {
        // 获取麦克风音量
        float volume = GetMicrophoneVolume();
        Debug.Log("当前麦克风音量：" + volume); // 打印音量到控制台
    }

    public float GetMicrophoneVolume()
    {
        float[] audioSamples = new float[256]; // 存储音频数据
        audioSource.GetOutputData(audioSamples, 0); // 获取音频输出数据
        float maxVolume = Mathf.Max(audioSamples); // 获取音量的最大值
        return Mathf.Clamp01(maxVolume * 10); // 返回音量（范围 0~1）
    }
}
