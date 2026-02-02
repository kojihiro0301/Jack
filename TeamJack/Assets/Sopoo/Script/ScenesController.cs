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
            if (Input.anyKeyDown)
            {
                SceneChangeManager.Instance.PlayWithOpening();
            }
        }
        //else if (scene == "PlayScene 1")
        //{
        //    if (GameManager.Instance.ProgressesBool[(int)GameManager.Progresses.End])
        //    {
        //        //SceneChangeManager.Instance.PlayWithEnding();
        //    }
        //    else if (Input.GetKeyDown(KeyCode.Alpha2))
        //    {
        //        SceneManager.LoadScene("TitleScene");
        //    }
        //}
        //else if (scene == "ResultScene")
        //{
        //    if (Input.GetKeyDown(KeyCode.Alpha1))
        //    {
        //        SceneManager.LoadScene("TitleScene");
        //    }
        //    else if (Input.GetKeyDown(KeyCode.Alpha2))
        //    {
        //        SceneManager.LoadScene("PlayScene 1");
        //    }
        //}
    }
}
