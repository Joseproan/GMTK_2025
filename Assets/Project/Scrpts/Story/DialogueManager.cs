using System;
using Ink.Runtime;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("Player Dialogue UI")]
    public PlayerDialogueUI playerDialogueUI;
    
    [HideInInspector] public GameObject npcDialogueBox;
    [HideInInspector] public TextMeshProUGUI npcDialogueText;
    
    private Story currentStory;
    public bool DialogueIsPlaying { get; private set; }
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void EnterDialogueMode(TextAsset inkJSON, string npcName)
    {
        currentStory = new Story(inkJSON.text);
        DialogueIsPlaying = true;
        ContinueStory();
    }

    public void ContinueStory()
    {
        if (IsPlayerTalking())
        {
            
        }
        else
        {
            
        }
        
    }

    private bool IsPlayerTalking()
    {
        if (currentStory.currentChoices.Count > 0) return true;

        var savedState = currentStory.state.ToJson();
        // Peek at the next line
        string nextLine = currentStory.Continue().Trim();
        // Restore story state (undo the peek)
        currentStory.state.LoadJson(savedState);
        
        return nextLine.Length == 0;
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
