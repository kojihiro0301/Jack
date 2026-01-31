using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    public void LoadPlayScene()
    {
        SceneManager.LoadScene("PlayScene"); 
    }
    public void LoadTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}