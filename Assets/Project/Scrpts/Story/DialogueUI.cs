using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance;

    [Header("UI References")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private Image speakerPortrait;

    [Header("Speakers")]
    [SerializeField] private Speaker[] speakers;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        ShowUI(false);
    }

    public void ShowUI(bool show)
    {
        dialoguePanel.SetActive(show);
    }

    public void DisplayLine(string line)
    {
        dialogueText.text = line;
    }

    public void SetSpeaker(string speakerID)
    {
        foreach (var sp in speakers)
        {
            if (sp.speakerID == speakerID)
            {
                speakerNameText.text = sp.displayName;
                speakerPortrait.sprite = sp.portrait;
                speakerPortrait.gameObject.SetActive(sp.portrait != null);
                return;
            }
        }

        // Fallback
        speakerNameText.text = "";
        speakerPortrait.gameObject.SetActive(false);
    }
}