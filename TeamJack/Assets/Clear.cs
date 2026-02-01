using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SimpleClearTrigger : MonoBehaviour
{
    [Header("タイトルシーンの名前")]
    [SerializeField] private string titleSceneName = "TitleScene";

    [Header("回収対象リスト（0個になればクリア）")]
    [SerializeField] private List<GameObject> targetObjects = new List<GameObject>();

    private bool isTransitioning = false;

    void Update()
    {
        // 遷移が始まったらUpdateを止める
        if (isTransitioning) return;

        // リスト内の「消滅したオブジェクト」を整理
        targetObjects.RemoveAll(obj => obj == null);

        // リストが空になったか判定
        if (targetObjects.Count == 0)
        {
            isTransitioning = true;
            Debug.Log("全アイテム回収完了！シーンを切り替えます。");
            SceneManager.LoadScene(titleSceneName);
        }
    }

    // デバッグ用：Gキーを押すと強制的にタイトルへ
    private void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene(titleSceneName);
        }
    }
}