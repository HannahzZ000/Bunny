using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine;

public class ARCameraConfig : MonoBehaviour
{
    private ARCameraManager cameraManager;

    void Start()
    {
        cameraManager = FindObjectOfType<ARCameraManager>();
        if (cameraManager != null)
        {
            cameraManager.frameReceived += OnFrameReceived;
        }
    }

    private void OnFrameReceived(ARCameraFrameEventArgs args)
    {
        Debug.Log("AR Camera Frame Received");
    }

    void OnDestroy()
    {
        if (cameraManager != null)
        {
            cameraManager.frameReceived -= OnFrameReceived;
        }
    }
}