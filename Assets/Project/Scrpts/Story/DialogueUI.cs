using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance;

    [Header("UI References")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private Image speakerPortrait;

    [Header("Choice UI")]
    [SerializeField] private GameObject choiceButtonPrefab; // Just a GameObject with TMP_Text inside
    [SerializeField] private Transform choicesContainer;

    [Header("Speakers")]
    [SerializeField] private Speaker[] speakers;

    private List<TextMeshProUGUI> choiceTexts = new();
    private int currentChoiceIndex = 0;
    private bool showingChoices = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        ShowUI(false);
    }

    public void ShowUI(bool show)
    {
        Debug.Log("Showing UI");
        dialoguePanel.SetActive(show);
        if (!show)
        {
            ClearChoices();
        }
    }

    public void DisplayNPCLine(string line)
    {
        dialogueText.gameObject.SetActive(true);
        dialogueText.text = line;
    }

    public void HideNPCLine()
    {
        dialogueText.gameObject.SetActive(false);
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

    public void DisplayChoices(List<Choice> choices)
    {
        ClearChoices();
        showingChoices = true;
        currentChoiceIndex = 0;

        for (int i = 0; i < choices.Count; i++)
        {
            GameObject choiceGO = Instantiate(choiceButtonPrefab, choicesContainer);
            TextMeshProUGUI choiceText = choiceGO.GetComponentInChildren<TextMeshProUGUI>();
            choiceText.text = choices[i].text.Trim();
            choiceTexts.Add(choiceText);
        }

        HighlightCurrentChoice();
    }

    public void ClearChoices()
    {
        foreach (Transform child in choicesContainer)
            Destroy(child.gameObject);

        choiceTexts.Clear();
        showingChoices = false;
    }

    private void HighlightCurrentChoice()
    {
        for (int i = 0; i < choiceTexts.Count; i++)
        {
            choiceTexts[i].color = (i == currentChoiceIndex) ? Color.blue : Color.black;
        }
    }

    public void NavigateChoices(int direction)
    {
        if (!showingChoices) return;

        currentChoiceIndex += direction;

        if (currentChoiceIndex < 0)
            currentChoiceIndex = choiceTexts.Count - 1;
        if (currentChoiceIndex >= choiceTexts.Count)
            currentChoiceIndex = 0;

        HighlightCurrentChoice();
    }

    public void ConfirmChoice()
    {
        if (!showingChoices) return;

        showingChoices = false;
        DialogueManager.Instance.MakeChoice(currentChoiceIndex);
    }

    public bool IsShowingChoices() => showingChoices;
}
