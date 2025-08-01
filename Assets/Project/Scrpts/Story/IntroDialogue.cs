using UnityEngine;

public class IntroDialogue : NPCInteraction
{
    public override void Start()
    {
        base.Start();
        DialogueManager.Instance.EnterDialogueMode(inkJSON,npcName);
    }

    public override void Update()
    {
        
    }
}
