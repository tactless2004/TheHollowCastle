/************************************************************
* COPYRIGHT:  Year
* PROJECT: Name of Project or Assignment
* FILE NAME: SoundEvent.cs
* DESCRIPTION: Short Description of script.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2000/01/01 | Your Name | Created class
*
*
************************************************************/
 
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Audio/Sound Event")]
public class SoundEvent : ScriptableObject
{
    [Header("Clips")]
    public AudioClip[] clips;

    [Header("Volume")]
    [Range(0f, 1f)] public float volumeMin = 0.9f;
    [Range(0f, 1f)] public float volumeMax = 1.0f;

    [Header("Pitch")]
    [Range(-3f, 3f)] public float pitchMin = 0.95f;
    [Range(-3f, 3f)] public float pitchMax = 1.05f;

    [Header("Playback")]
    public bool spatial = true;
    public float minDistance = 1f;
    public float maxDistance = 15f;

    [Header("Mixing")]
    public AudioMixerGroup mixerGroup;

    [Header("Limits")]
    public float cooldown = 0f; // seconds

    // Only Interface, everything else should be accessed directly
    public AudioClip GetRandomClip()
    {
        if (clips == null || clips.Length == 0) return null;
        return clips[Random.Range(0, clips.Length)];
    }
}
