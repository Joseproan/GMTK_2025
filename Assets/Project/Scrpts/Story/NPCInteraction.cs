using System;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public TextAsset inkJSON;
    public GameObject interactionUI;
    public float interactionRange = 3f;
    public string npcName;
    public Transform player;
    private bool isPlayerNear = false;

    public virtual void Start()
    {
        interactionUI.SetActive(false);
    }

    public virtual void Update()
    {
        if (DialogueManager.Instance.DialogueIsPlaying)
        {
            interactionUI.SetActive(false);
            return;
        }

        if (player == null) return;
        
        float dist = Vector3.Distance(player.position, transform.position);
        isPlayerNear = dist <= interactionRange;

        interactionUI.SetActive(isPlayerNear);

        if (isPlayerNear && InputManager.InteractWasPressed)
        {
            DialogueManager.Instance.EnterDialogueMode(inkJSON,npcName);
        }
    }
}