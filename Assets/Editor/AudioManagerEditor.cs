#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;

[CustomEditor(typeof(AudioManager))]
public class AudioManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AudioManager manager = (AudioManager)target;

        if (manager.musicSounds == null || manager.musicSounds.Length == 0)
            return;

        string[] musicNames = new string[manager.musicSounds.Length];
        for (int i = 0; i < musicNames.Length; i++)
        {
            musicNames[i] = manager.musicSounds[i].name;
        }

        int currentIndex = Array.IndexOf(musicNames, manager.startMusicName);
        if (currentIndex < 0) currentIndex = 0;

        int selectedIndex = EditorGUILayout.Popup("Start Music", currentIndex, musicNames);
        manager.startMusicName = musicNames[selectedIndex];

        if (GUI.changed)
        {
            EditorUtility.SetDirty(manager);
        }
    }
}
#endif