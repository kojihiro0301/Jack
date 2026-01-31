using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환을 위해 필수적인 네임스페이스

public class SceneController : MonoBehaviour
{
    void Update()
    {
        // 현재 활성화된 씬의 이름을 가져옵니다.
        string currentScene = SceneManager.GetActiveScene().name;

        // 1. 현재 씬이 TitleScene 일 때
        if (currentScene == "TitleScene")
        {
            // 숫자 키 1을 누르면 PlayScene으로 이동
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SceneManager.LoadScene("PlayScene");
            }
        }
        // 2. 현재 씬이 PlayScene 일 때
        else if (currentScene == "PlayScene")
        {
            // 숫자 키 1을 누르면 ResultScene으로 이동
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SceneManager.LoadScene("ResultScene");
            }
            // 숫자 키 2를 누르면 TitleScene으로 이동
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SceneManager.LoadScene("TitleScene");
            }
        }
        // 3. 현재 씬이 ResultScene 일 때
        else if (currentScene == "ResultScene")
        {
            // 숫자 키 1을 누르면 TitleScene으로 이동
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SceneManager.LoadScene("TitleScene");
            }
            // 숫자 키 2를 누르면 PlayScene으로 이동
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SceneManager.LoadScene("PlayScene");
            }
        }
    }
}