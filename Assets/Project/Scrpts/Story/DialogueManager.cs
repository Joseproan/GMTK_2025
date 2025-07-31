using System;
using Ink.Runtime;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("Ink")]
    [SerializeField] private TextAsset inkJSONAsset;
    private Story currentStory;
    public bool DialogueIsPlaying { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        DialogueIsPlaying = true;
        DialogueUI.Instance.ShowUI(true);
        ContinueStory();
    }

    public void ContinueStory()
    {
        if (CanContinueStory())
        {
            string text = currentStory.Continue().Trim();
            // Look for speaker variable
            string speakerID = currentStory.variablesState["speaker"]?.ToString() ?? "";
            DialogueUI.Instance.SetSpeaker(speakerID);

            DialogueUI.Instance.DisplayLine(text);
        }
        else
        {
            ExitDialogueMode();
        }
    }


    public void ExitDialogueMode()
    {
        DialogueIsPlaying = false;
        DialogueUI.Instance.ShowUI(false);
    }

    public bool CanContinueStory() => currentStory != null && currentStory.canContinue;

    private void Update()
    {
        if (DialogueIsPlaying && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)))
        {
            ContinueStory();
        }

    }
}