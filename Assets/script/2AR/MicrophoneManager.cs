using UnityEngine;

public class MicrophoneManager : MonoBehaviour
{
    public static MicrophoneManager Instance; // 单例模式

    private string microphoneDevice;
    private AudioClip micClip;
    private bool micInitialized = false;

    public float CurrentVolume { get; private set; } // 当前音量值

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 保持唯一实例
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 初始化麦克风
        if (Microphone.devices.Length > 0)
        {
            microphoneDevice = Microphone.devices[0];
            micClip = Microphone.Start(microphoneDevice, true, 1, 44100);
            micInitialized = true;
            Debug.Log($"Microphone initialized: {microphoneDevice}");
        }
        else
        {
            Debug.LogError("No microphone detected!");
        }
    }

    void Update()
    {
        if (micInitialized)
        {
            CurrentVolume = GetMicrophoneVolume();
        }
    }

    private float GetMicrophoneVolume()
    {
        if (micClip == null) return 0;

        int micPosition = Microphone.GetPosition(microphoneDevice) - 256;
        if (micPosition < 0) return 0;

        float[] waveData = new float[256];
        micClip.GetData(waveData, micPosition);

        float maxVolume = 0;
        for (int i = 0; i < waveData.Length; i++)
        {
            float volume = Mathf.Abs(waveData[i]);
            if (volume > maxVolume)
            {
                maxVolume = volume;
            }
        }
        return maxVolume;
    }
}
