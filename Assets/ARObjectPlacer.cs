using System.Collections.Generic; // 必须添加
using UnityEngine;
using UnityEngine.XR.ARFoundation; // 必须添加
using UnityEngine.XR.ARSubsystems; // 必须添加

public class ARObjectPlacer : MonoBehaviour
{
    public GameObject objectToPlace; // 要放置的物体
    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinBounds))
                {
                    Pose hitPose = hits[0].pose;
                    Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
                }
            }
        }
    }
}