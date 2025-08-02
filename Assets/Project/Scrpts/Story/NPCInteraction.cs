using System;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public TextAsset inkJSON;
    private GameObject interactionUI;
    public float interactionRange = 3f;
    public string npcName;
    private Transform player;
    private bool isPlayerNear = false;

    private void Awake()
    {
        interactionUI = FindFirstObjectByType<PlayerDialogueUI>().interactionCanvas;
    }

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        interactionUI.SetActive(false);
    }

    public virtual void Update()
    {
        if (DialogueManager.Instance.DialogueIsPlaying)
        {
            interactionUI.SetActive(false);
            return;
        }

        float dist = Vector3.Distance(player.position, transform.position);
        isPlayerNear = dist <= interactionRange;

        interactionUI.SetActive(isPlayerNear);

        if (isPlayerNear && InputManager.InteractWasPressed)
        {
            DialogueManager.Instance.EnterDialogueMode(inkJSON,npcName);
        }
    }
}