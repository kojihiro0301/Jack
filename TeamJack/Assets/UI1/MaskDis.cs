using UnityEngine;
using System.Collections;

public class MaskDis : MonoBehaviour
{
    [Header("■ 1つ目のマスク設定")]
    [Tooltip("これが最初のマスクならチェック（色を戻す）")]
    public bool isFirstMask = false;

    [Header("■ 3つ目のマスク設定（ヒント出現）")]
    [Tooltip("このマスクを取った時に出現させたいオブジェクト（数字など）")]
    public GameObject[] hiddenHints;

    [Header("■ 共通設定")]
    public CanvasGroup targetUI_CanvasGroup;
    public float uiFadeDuration = 1.0f;
    public ColorController colorController; // 色戻し用

    public void FadeObj()
    {
        // 1. 色を戻す（最初のマスクのみ）
        if (isFirstMask && colorController != null)
        {
            colorController.StartRestoreColor();
        }

        // 2. 隠された数字を表示する（3つ目のマスク用）
        if (hiddenHints != null && hiddenHints.Length > 0)
        {
            foreach (var obj in hiddenHints)
            {
                if (obj != null) obj.SetActive(true);
            }
        }

        // 3. UIフェードと自身の消滅
        StartCoroutine(ProcessDisappearAndUI());
    }

    private IEnumerator ProcessDisappearAndUI()
    {
        if (GetComponent<Renderer>() != null) GetComponent<Renderer>().enabled = false;
        if (GetComponent<Collider>() != null) GetComponent<Collider>().enabled = false;

        if (targetUI_CanvasGroup != null)
        {
            targetUI_CanvasGroup.gameObject.SetActive(true);
            targetUI_CanvasGroup.alpha = 0f;
            float timer = 0f;
            while (timer < uiFadeDuration)
            {
                timer += Time.deltaTime;
                targetUI_CanvasGroup.alpha = Mathf.Clamp01(timer / uiFadeDuration);
                yield return null;
            }
            targetUI_CanvasGroup.alpha = 1f;
        }

        Destroy(this.gameObject);
    }
}