using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARAnchorExample : MonoBehaviour
{
    public GameObject objectToAnchor;
    private ARAnchorManager anchorManager;

    void Start()
    {
        anchorManager = FindObjectOfType<ARAnchorManager>();
        PlaceAnchor();
    }

    void PlaceAnchor()
    {
        // 将物体的位置转换为锚点
        ARAnchor anchor = anchorManager.AddAnchor(new Pose(objectToAnchor.transform.position, objectToAnchor.transform.rotation));
        if (anchor != null)
        {
            objectToAnchor.transform.parent = anchor.transform; // 将物体设置为锚点的子对象
        }
        else
        {
            Debug.LogError("Failed to create anchor.");
        }
    }
}