using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeaker", menuName = "Dialogue/Speaker")]
public class Speaker : ScriptableObject
{
    public string speakerID; // e.g., "OldMan"
    public string displayName; // e.g., "Old Man"
    public Sprite portrait;
}