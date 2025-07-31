using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private GameObject interactionUI;
    [SerializeField] private float interactionRange = 3f;

    private Transform player;
    private bool isPlayerNear = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        interactionUI.SetActive(false);
    }

    private void Update()
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
            DialogueManager.Instance.EnterDialogueMode(inkJSON);
        }
    }
}