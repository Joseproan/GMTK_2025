using DG.Tweening;
using UnityEngine;

public class OpeningBusStop : MonoBehaviour
{
    [Header("Scale Settings")]
    public float duration = 0.5f;
    public Ease easeIn = Ease.OutBack;
    public Ease easeOut = Ease.InBack;
    public float delay = 0f;
    public float waitAtFullScale = 1f;
    
    [Header("Player")]
    public GameObject player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player.SetActive(false);
        transform.localScale = Vector3.zero;

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(waitAtFullScale)
                .Append(transform.DOScale(Vector3.one, duration)
                .SetEase(easeIn)
                .SetDelay(delay))
                .AppendInterval(waitAtFullScale)
                .Append(transform.DOScale(Vector3.zero, duration)
                .SetEase(easeOut)
                .OnComplete(() => player.SetActive(true))
                );
    }


}
