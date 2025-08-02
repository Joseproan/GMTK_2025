using UnityEngine;
using DG.Tweening;

public class BreathingAnimation : MonoBehaviour
{
    [Header("Breathing Settings")]
    public float scaleAmount = 0.1f; // how exaggerated the stretch is
    public float duration = 0.5f;    // time per inhale/exhale phase
    public Ease easeType = Ease.InOutSine;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
        PlayBreathing();
    }

    public void PlayBreathing()
    {
        Sequence breathSeq = DOTween.Sequence();

        Vector3 tallThin = new Vector3(
            originalScale.x - scaleAmount,
            originalScale.y + scaleAmount,
            originalScale.z
        );

        Vector3 wideShort = new Vector3(
            originalScale.x + scaleAmount,
            originalScale.y - scaleAmount,
            originalScale.z
        );

        breathSeq.Append(transform.DOScale(tallThin, duration).SetEase(easeType))
            .Append(transform.DOScale(originalScale, duration).SetEase(easeType))
            .Append(transform.DOScale(wideShort, duration).SetEase(easeType))
            .Append(transform.DOScale(originalScale, duration).SetEase(easeType))
            .SetLoops(-1); // loop forever
    }
}