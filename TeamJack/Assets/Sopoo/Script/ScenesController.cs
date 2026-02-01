using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    void Update()
    {
        string scene = SceneManager.GetActiveScene().name;

        if (scene == "TitleScene")
        {
            // タイトルでは動画再生付き遷移を呼ぶだけ
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SceneChangeManager.Instance.PlayWithOpening();
            }
        }
        else if (scene == "PlayScene")
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SceneManager.LoadScene("ResultScene");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SceneManager.LoadScene("TitleScene");
            }
        }
        else if (scene == "ResultScene")
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SceneManager.LoadScene("TitleScene");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SceneManager.LoadScene("PlayScene");
            }
        }
    }
}
