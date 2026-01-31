using UnityEngine;
using System.Collections; // コルーチンを使うために必要

public class MaskDis : MonoBehaviour
{
    [Header("UICanvas")]
    public CanvasGroup targetCanvasGroup;

    [Header("フェード時間")]
    public float fadeDuration = 1.0f;

    public void FadeObj()
    {
        if (targetCanvasGroup != null)
        {
            StartCoroutine(FadeInSequence());
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator FadeInSequence()
    {
        if (GetComponent<Renderer>() != null) GetComponent<Renderer>().enabled = false;
        if (GetComponent<Collider>() != null) GetComponent<Collider>().enabled = false;

        targetCanvasGroup.gameObject.SetActive(true);
        targetCanvasGroup.alpha = 0f;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            targetCanvasGroup.alpha = timer / fadeDuration;
            yield return null; 
        }
        targetCanvasGroup.alpha = 1f;
        Destroy(this.gameObject);
    }
}