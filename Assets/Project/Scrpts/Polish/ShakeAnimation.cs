using UnityEngine;
using DG.Tweening;

public class ShakeAnimation : MonoBehaviour
{
    [Header("Shake Settings")]
    public float duration = 0.1f; // Duration of one shake cycle
    public float strength = 0.05f; // How strong the shake is
    public int vibrato = 10;      // How many times it shakes in one cycle
    public float randomness = 90f;
    public bool fadeOut = false;
    public bool shakePosition = true;

    private Tween shakeTween;

    void Start()
    {
        StartShaking();
    }

    public void StartShaking()
    {
        StopShaking(); // Prevent multiple shakes at once

        if (shakePosition)
        {
            shakeTween = transform.DOShakePosition(duration, strength, vibrato, randomness, fadeOut)
                .SetLoops(-1, LoopType.Restart);
        }
        else
        {
            shakeTween = transform.DOShakeRotation(duration, strength, vibrato, randomness, fadeOut)
                .SetLoops(-1, LoopType.Restart);
        }
    }

    public void StopShaking()
    {
        if (shakeTween != null && shakeTween.IsActive())
        {
            shakeTween.Kill();
        }
    }

    void OnDestroy()
    {
        StopShaking();
    }
}