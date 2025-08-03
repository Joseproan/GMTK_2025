using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BusStop : MonoBehaviour
{
    [Header("Bus")]
    public GameObject bus;

    [Header("Scale Settings")]
    public float duration = 0.5f;
    public Ease easeIn = Ease.OutBack;
    public Ease easeOut = Ease.InBack;
    public float delay = 0f;
    public float waitAtFullScale = 1f;

    [Header("Scene")]
    public string nextSceneName;

    [Header("Fade")]
    public Image fadeImage;
    public float fadeDuration = 0.5f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        other.transform.parent.parent.gameObject.SetActive(false);
        bus.SetActive(true);
        bus.transform.localScale = Vector3.zero;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(bus.transform.DOScale(Vector3.one, duration)
                .SetEase(easeIn)
                .SetDelay(delay))
            .AppendInterval(waitAtFullScale)
            .Append(bus.transform.DOScale(Vector3.zero, duration)
                .SetEase(easeOut))
            .AppendCallback(() =>
            {
                fadeImage.gameObject.SetActive(true);
                fadeImage.color = new Color(0, 0, 0, 0);
                fadeImage.DOFade(1f, fadeDuration).OnComplete(() =>
                {
                    SceneManager.LoadScene(nextSceneName);
                });
            });
    }
}