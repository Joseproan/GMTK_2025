using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
}

[System.Serializable]
public class SFXCategory
{
    public string categoryName;
    public Sound[] sounds;
}