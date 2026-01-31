using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    // Game Start 버튼에 연결할 함수
    public void LoadPlayScene()
    {
        SceneManager.LoadScene("PlayScene"); 
    }

    // Play Again 버튼에 연결할 함수
    public void LoadTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}