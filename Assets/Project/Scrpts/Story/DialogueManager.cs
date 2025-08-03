using System;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("Ink")]
    [SerializeField] private TextAsset inkJSONAsset;
    private Story currentStory;
    private string currentNPC;
    public bool DialogueIsPlaying { get; private set; }

    public IntroDialogue introDialogue;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void EnterDialogueMode(TextAsset inkJSON, string npcName)
    {
        currentStory = new Story(inkJSON.text);
        currentNPC = npcName;
        DialogueIsPlaying = true;
        DialogueUI.Instance.ShowUI(true);
        ContinueStory();
    }

    public void ContinueStory()
    {
        if (currentStory.currentChoices.Count > 0)
        {
            DialogueUI.Instance.SetSpeaker("Pebble");
            DialogueUI.Instance.HideNPCLine();
            DialogueUI.Instance.DisplayChoices(currentStory.currentChoices);
        }
        
        else if (CanContinueStory())
        {
            string text = currentStory.Continue().Trim();
            DialogueUI.Instance.SetSpeaker(currentNPC);
            DialogueUI.Instance.DisplayNPCLine(text);
            DialogueUI.Instance.ClearChoices();
            if (text.Length == 0)
            {
                if (currentStory.currentChoices.Count > 0)
                {
                    DialogueUI.Instance.SetSpeaker("Pebble");
                    DialogueUI.Instance.HideNPCLine();
                    DialogueUI.Instance.DisplayChoices(currentStory.currentChoices);
                }
            }
        }
        
        else if (!CanContinueStory())
        {
            ExitDialogueMode();
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
        ContinueStory();
    }

    public void ExitDialogueMode()
    {
        DialogueIsPlaying = false;
        DialogueUI.Instance.ShowUI(false);

        if (introDialogue != null)
        {
            introDialogue.IntroDone();
            introDialogue = null;
        }
    }

    public bool CanContinueStory() => currentStory != null && currentStory.canContinue;

    private void Update()
    {
        if (!DialogueIsPlaying) return;

        if (DialogueUI.Instance.IsShowingChoices())
        {
            if (InputManager.UpArrowWasPressed)
                DialogueUI.Instance.NavigateChoices(-1);
            else if (InputManager.DownArrowWasPressed)
                DialogueUI.Instance.NavigateChoices(1);
            else if (InputManager.ContinueStoryWasPressed)
                DialogueUI.Instance.ConfirmChoice();
        }
        else if (InputManager.ContinueStoryWasPressed)
        {
            ContinueStory();
        }
    }
}
