using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject savedUI; // "Saved" UI 面板
    public GameObject lostUI;  // "Lost" UI 面板
    public GameObject homeUI;  // "Home" UI 面板

    public void ShowHomeUI()
    {
        Debug.Log("Navigating to Home UI...");
        // 显示 Home UI，隐藏其他 UI
        homeUI.SetActive(true);
        savedUI.SetActive(false);
        lostUI.SetActive(false);
    }

    public void ShowMuseumScene()
    {
        Debug.Log("Navigating to Museum Scene...");
        // 这里加载名为 "Museum" 的场景
        UnityEngine.SceneManagement.SceneManager.LoadScene("Museum");
    }

    public void ShowARScene()
    {
        Debug.Log("Navigating to AR Scene...");
        // 这里加载名为 "ARScene" 的场景
        UnityEngine.SceneManagement.SceneManager.LoadScene("ARScene");
    }

    public void OpenDonationPage()
    {
        Debug.Log("Opening donation page...");
        // 打开外部链接
        Application.OpenURL("https://www.gofundme.com/f/rebuild-the-bunny-museum-help-steve-candace");
    }
}
