using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "TitleScene")
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SceneManager.LoadScene("PlayScene");
            }
        }
        else if (currentScene == "PlayScene")
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
        else if (currentScene == "ResultScene")
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