using UnityEngine;
using DG.Tweening;

public class RotateAnimation : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Vector3 rotationAmount = new Vector3(0, 360, 0); // how much to rotate
    public float duration = 2f; // how long one full rotation takes
    public RotateMode rotateMode = RotateMode.FastBeyond360;
    public Ease easeType = Ease.Linear;

    private void Start()
    {
        Rotate();
    }

    public void Rotate()
    {
        transform.DORotate(rotationAmount, duration, rotateMode)
            .SetEase(easeType)
            .SetLoops(-1, LoopType.Restart); // loop forever
    }
}