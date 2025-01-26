using UnityEngine;

public class CameraPermissionRequest : MonoBehaviour
{
    void Start()
    {
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            // 请求摄像头权限
            Application.RequestUserAuthorization(UserAuthorization.WebCam);
        }
        else
        {
            Debug.Log("Camera access already granted.");
        }
    }
}