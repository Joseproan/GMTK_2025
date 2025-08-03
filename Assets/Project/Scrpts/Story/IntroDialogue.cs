using UnityEngine;

public class IntroDialogue : NPCInteraction
{
    public GameObject camera;
    public GameObject vCam;
    public GameObject playerObj;
    public GameObject introCamera;

    public string musicName;
    public override void Start()
    {
        base.Start();
        DialogueManager.Instance.EnterDialogueMode(inkJSON,npcName);
        camera.SetActive(false);
        vCam.SetActive(false);
        playerObj.SetActive(false);

        introCamera.SetActive(true);
    }

    public void IntroDone()
    {
        camera.SetActive(true);
        vCam.SetActive(true);
        playerObj.SetActive(true);
        
        introCamera.SetActive(false);
        AudioManager.Instance.PlayMusic(musicName);
    }
}
