using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class ColorController : MonoBehaviour
{
    [Tooltip("ÉVÅ[Éìè„ÇÃGlobal Volume")]
    public Volume globalVolume;

    [Tooltip("êFÇ™ñﬂÇÈÇ‹Ç≈ÇÃéûä‘")]
    public float colorRestoreDuration = 2.0f;

    private ColorAdjustments colorAdjustments;

    void Start()
    {
        if (globalVolume == null)
            globalVolume = FindFirstObjectByType<Volume>();

        if (globalVolume != null)
        {
            if (globalVolume.profile.TryGet(out colorAdjustments))
            {
                colorAdjustments.saturation.value = -100f;
            }
        }
    }
    public void StartRestoreColor()
    {
        if (colorAdjustments != null)
        {
            StartCoroutine(RestoreRoutine());
        }
    }

    private IEnumerator RestoreRoutine()
    {
        float timer = 0f;
        float startSat = -100f;
        float targetSat = 100f;

        while (timer < colorRestoreDuration)
        {
            timer += Time.deltaTime;
            float t = timer / colorRestoreDuration;

            colorAdjustments.saturation.value = Mathf.Lerp(startSat, targetSat, t);

            yield return null;
        }

        colorAdjustments.saturation.value = targetSat;
    }
}