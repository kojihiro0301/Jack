using UnityEngine;
using System.Collections;

public class MaskDis : MonoBehaviour
{
    [Header("GameManager連携")]
    public GameManager.Progresses progressType;

    [Tooltip("これが最初のマスクならチェック")]
    public bool isFirstMask = false;

    [Tooltip("このマスクを取った時に出現させたいオブジェクト")]
    public GameObject[] hiddenHints;

    [Header("共通設定")]
    public CanvasGroup targetUI_CanvasGroup;
    public float uiFadeDuration = 1.0f;
    public ColorController colorController;

    public void FadeObj()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ProgressAchievement(progressType);
        }
 
        if (isFirstMask && colorController != null)
        {
            colorController.StartRestoreColor();
        }

        if (hiddenHints != null && hiddenHints.Length > 0)
        {
            foreach (var obj in hiddenHints)
            {
                if (obj != null) obj.SetActive(true);
            }
        }

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